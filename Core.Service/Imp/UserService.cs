using System;
using System.Collections.Generic;
using Core.Models;
using System.Linq;
using Core.Repository;
using Core.Service.Adapter;
using Core.Service.Models.UserView;
using Core.Common.Enums;
using Core.Service.Models;
using System.Linq.Expressions;
using Core.Common.LinqExtensions;

namespace Core.Service.Imp
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IRolePermissionRepository _rolePermissionRepository;
        private IPermissionRepository _permissionRepository;
        private IMenuRepository _menuRepository;
        public UserService(IUserRepository userRepository, IRolePermissionRepository rolePermissionRepository, IMenuRepository menuRepository, IPermissionRepository permissionRepository)
        {
            _userRepository = userRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
        }

        public ResultDataModel GetCurrentUserProfile(Guid userId)
        {
            var user = _userRepository.GetByKey(userId);
            if (user.IsDeleted == IsDeleted.Yes)
                return null;
            var roles = user.UserRoles;
           
            var menus = _menuRepository.Entities.ToList();
            var permissions = _permissionRepository.Entities.ToList();

            var allowPages = new List<string> { };

            if(roles !=null)
            {
                if (roles.IsDeleted != IsDeleted.Yes)
                {
                    if (roles.IsSuperAdministrator)
                    {
                        allowPages.AddRange(menus.Select(s => s.Alias));
                    }
                    else
                    {
                        if (!roles.RolePermissions.Any())
                            return null;
                        permissions = roles.RolePermissions.Select(s => s.Permission).ToList();
                        allowPages.AddRange(menus.Where(x => x.IsDefaultRouter == (int)YesOrNo.Yes).Select(x => x.Alias));
                        foreach (var permission in permissions.Where(x => x.Type == (int)PermissionType.Menu))
                        {
                            allowPages.AddRange(FindParentMenuAlias(menus, permission.MenuId));
                        }
                    }
                }
            }
            
            var pages = allowPages.Distinct().ToList();
            var permissionDic = permissions.GroupBy(f => f.Menus.Alias).ToDictionary(g => g.Key, g => g.Select(s => s.ActionCode).ToList());
            var resultData = new CurrentUserInfoViewModel { Access = new string[] { }, Avator = user.Avatar, UserId = userId, UserName = user.DisplayName, Pages = allowPages.Distinct().ToList(), Permissions = permissionDic };
            return new ResultDataModel { Data = resultData };
        }

        private List<string> FindParentMenuAlias(List<Menus> menus, Guid? parentGuid)
        {
            var pages = new List<string>();
            var parent = menus.FirstOrDefault(x => x.Id == parentGuid);
            if (parent != null)
            {
                if (!pages.Contains(parent.Alias))
                {
                    pages.Add(parent.Alias);
                }
                else
                {
                    return pages;
                }
                if (parent.ParentGuid != Guid.Empty)
                {
                    pages.AddRange(FindParentMenuAlias(menus, parent.ParentGuid));
                }
            }

            return pages.Distinct().ToList();
        }

        public ResultDataModel GetPageList(out int total, int page = 1, int size = 10)
        {
            var list = _userRepository.GetPages(page, size, out total).ProjectedAsCollection<UserViewModel, Guid>();
            return new ResultDataModel
            {
                Data = list
            };
        }

        public ResultDataModel Login(UserLoginModel loginModel)
        {
            var responseModel = new ResultDataModel();
            var user = _userRepository.Entities.FirstOrDefault(f => f.LoginName == loginModel.UserName).ProjectedAs<UserViewModel, Guid>();
            if (user == null)
            {
                responseModel.SetFailed("账号或者密码错误");
            }
            else
            {
                if (!user.Password.Equals(loginModel.Password))
                {
                    responseModel.SetFailed("账号或者密码错误！");
                }
                else
                {
                    if (user.IsLocked == (int)IsLocked.Locked)
                    {
                        responseModel.SetFailed("账号被锁定！");
                    }
                    else
                    {
                        switch (user.Status)
                        {
                            case (int)UserStateEnums.Forbidden:
                                responseModel.SetFailed("账号被禁用！");
                                break;
                            case (int)UserStateEnums.Normal:
                                responseModel.SetSuccess("登录成功！");
                                responseModel.SetData(user);
                                break;
                        }
                    }
                }
            }
            return responseModel;
        }

        public ResultDataModel GetPageList(UserRequestModel userRequest)
        {
            int total = 0;
            Expression<Func<Users, bool>> fliter = f => f.UserType >= 0;
            if (!string.IsNullOrWhiteSpace(userRequest.Kw))
            {
                fliter = fliter.And(f => f.LoginName.Contains(userRequest.Kw) || f.DisplayName.Contains(userRequest.Kw));
            }
            if (userRequest.IsDeleted > IsDeleted.All)
            {
                fliter = fliter.And(f => f.IsDeleted == userRequest.IsDeleted);
            }
            if (userRequest.Status > UserStateEnums.All)
            {
                fliter = fliter.And(f => f.Status == userRequest.Status);
            }
            var resultData = _userRepository.GetPages(fliter, userRequest.CurrentPage, userRequest.PageSize, out total).ProjectedAsCollection<UserViewPageModel, Guid>();
            return new ResultDataModel
            {
                Data = resultData,
                TotalCount = total
            };
        }

        public ResultDataModel CreateUser(UserEditViewModel model)
        {
            var resultData = new ResultDataModel();
            var loginNameHasUsed = _userRepository.Entities.Any(f => f.LoginName == model.LoginName);
            if (loginNameHasUsed)
            {
                resultData.SetFailed("登录名已经被使用");
                return resultData;
            }
            var user = model.ProjectedAs<Users>();
            user.Id = Guid.NewGuid();
            _userRepository.Insert(user);
            resultData.SetSuccess();
            return resultData;
        }

        public ResultDataModel GetUserById(Guid id)
        {
            var result = _userRepository.GetByKey(id).ProjectedAs<UserEditViewModel>();
            return new ResultDataModel { Data = result };
        }

        public ResultDataModel UpdateUser(UserEditViewModel model)
        {
            var resultData = new ResultDataModel();
            var entity = _userRepository.GetByKey(model.Guid);
            if (entity != null)
            {
                entity.DisplayName = model.DisplayName;
                entity.IsDeleted = model.IsDeleted;
                entity.IsLocked = model.IsLocked;
                entity.ModifiedByUserGuid = model.ModifyBy;
                entity.UpdateDate = DateTime.Now;
                entity.Password = model.Password;
                entity.Status = model.Status;
                entity.UserType = model.UserType;
                entity.Description = model.Description;
                entity.RoleId = model.RoleId;
                _userRepository.Update(entity);
                resultData.SetSuccess();
            }
            else
            {
                resultData.SetFailed("用户信息不存在");
            }
            return resultData;
        }

        public ResultDataModel UpdateIsDelete(IsDeleted isDeleted, string ids)
        {
            var idsArray = ids.Split(',').ToList();
            var idsGuidList = new List<Guid>();
            idsArray.ForEach((item) =>
            {
                idsGuidList.Add(Guid.Parse(item));
            });
            var users = _userRepository.Entities.Where(f => idsGuidList.Contains(f.Id)).ToList();
            users.ForEach((item) =>
            {
                item.IsDeleted = isDeleted;
            });
            _userRepository.Update(users);
            var result = new ResultDataModel();
            result.SetSuccess();
            return result;
        }

        public ResultDataModel UpdateStatus(UserStateEnums userStateEnums, string ids)
        {
            var idsArray = ids.Split(',').ToList();
            var idsGuidList = new List<Guid>();
            idsArray.ForEach((item) =>
            {
                idsGuidList.Add(Guid.Parse(item));
            });
            var users = _userRepository.Entities.Where(f => idsGuidList.Contains(f.Id)).ToList();
            users.ForEach((item) =>
            {
                item.Status = userStateEnums;
            });
            _userRepository.Update(users);
            var result = new ResultDataModel();
            result.SetSuccess();
            return result;
        }
    }
}
