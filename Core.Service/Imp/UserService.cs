using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;
using System.Linq;
using Core.Repository;
using Core.Service.Adapter;
using Core.Service.Models.UserView;
using Core.Common.Enums;
using System.Data.SqlClient;
using Core.Service.Models;
using System.Security.Claims;

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
            if (user.IsDeleted == (int)IsDeleted.Yes)
                return null;
            var roles = user.UserRoles;
            if (roles.IsDeleted == (int)IsDeleted.Yes)
                return null;
   
            var menus = _menuRepository.Entities.ToList();
            var permissions = _permissionRepository.Entities.ToList();

            var allowPages = new List<string> { };
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
            var pages = allowPages.Distinct().ToList();
            var permissionDic = permissions.GroupBy(f => f.Menus.Alias).ToDictionary(g => g.Key, g => g.Select(s => s.ActionCode).ToList());
            var resultData = new CurrentUserInfoViewModel { Access = new string[] { },Avator = user.Avatar, UserId = userId, UserName = user.DisplayName, Pages = allowPages.Distinct().ToList(), Permissions = permissionDic };
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

        

    }
}
