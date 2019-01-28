using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Core.Common.LinqExtensions;
using Core.Common.Enums;
using Core.Service.Models.MenuView;
using Core.Repository;
using Core.Models;
using Core.Service.Models;
using Core.Service.Adapter;
using System.Linq;
using System.Data.SqlClient;

namespace Core.Service.Imp
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        public ResultDataModel SaveEdit(MenuCreateViewModel model,AuthContextUser currentUser)
        {
            var entity = model.ProjectedAs<Menus>(); 
            if(entity.Id==Guid.Empty)
            {
                entity.CreateDate = DateTime.Now;
                entity.Id = Guid.NewGuid();
                entity.CreatedByUserGuid = currentUser.UserId;
                entity.CreatedByUserName = currentUser.DisplayName;
                _menuRepository.Insert(entity);
            }
            else
            {
                entity = _menuRepository.GetByKey(model.Id);
                entity.Name = model.Name;
                entity.Icon = model.Icon;
                entity.Level = 1;
                entity.ParentGuid = model.ParentGuid;
                entity.Sort = model.Sort;
                entity.Url = model.Url;
                entity.ModifiedByUserGuid = currentUser.UserId;
                entity.ModifiedByUserName =currentUser.DisplayName;
                entity.UpdateDate = DateTime.Now;
                entity.Description = model.Description;
                entity.ParentName = model.ParentName;
                _menuRepository.Update(entity);
            }

            var resultData = new ResultDataModel();
            resultData.SetSuccess();
            return resultData;
        }

        public ResultDataModel Delete(IsDeleted isDeleted,string ids)
        {
            List<string> idList = ids.Split(',').ToList();
            var entityList = _menuRepository.Entities.Where(f => idList.Any(s=>s==f.Id.ToString()));
            if(entityList!=null)
            {
                foreach (var item in entityList)
                {
                    item.IsDeleted = (int)isDeleted;
                }
            }
            var result = _menuRepository.Update(entityList);
            var resultData = new ResultDataModel();
            resultData.SetSuccess();
            return resultData;
        }

        public ResultDataModel GetMenuUseToEdit(Guid id)
        {
            var entity = _menuRepository.GetByKey(id).ProjectedAs<MenuCreateViewModel>();
            var menuTree = LoadMenuTreeData(entity.ParentGuid);
            var response = new ResultDataModel();
            response.SetData(new { entity, menuTree.Data });
            return response;
        }

        public ResultDataModel GetPage(MenuRequestModel model)
        {
            Expression<Func<Menus, bool>> fliter = null;
            if (!string.IsNullOrWhiteSpace(model.Kw))
            {
                model.Kw = model.Kw.Trim();
                fliter = f => f.Name.Contains(model.Kw) || f.Url.Contains(model.Kw);
            }
            if (model.IsDeleted > IsDeleted.All)
            {
                int isDeleted = (int)model.IsDeleted;
                if (fliter == null)
                    fliter = f => f.IsDeleted == isDeleted;
                else
                    fliter = fliter.And(f => f.IsDeleted == isDeleted);
            }
            if (model.Status > Status.All)
            {
                int status = (int)model.Status;
                if (fliter == null)
                    fliter = f => f.Status == status;
                else
                    fliter = fliter.And(f => f.Status == status);
            }
            if (model.ParentGuid.HasValue)
            {
                if (fliter == null)
                    fliter = f => f.ParentGuid == model.ParentGuid;
                else
                    fliter = fliter.And(f => f.ParentGuid == model.ParentGuid);
            }
            int totalCount = 0;
            var list = _menuRepository.GetPages(fliter,model.CurrentPage, model.PageSize, out totalCount).ProjectedAsCollection<MenuViewModel,Guid>();
            var resultData = new ResultDataModel();
            resultData.SetData(list, totalCount);
            return resultData;
        }

        public ResultDataModel LoadMenuTreeData(Guid? selectedGuid)
        {
            var temp = _menuRepository.Entities.Where(x => x.IsDeleted ==(int)IsDeleted.No && x.Status == (int)Status.Normal).ToList().Select(x => new MenuTree
            {
                Id = x.Id.ToString(),
                ParentGuid = x.ParentGuid,
                Title = x.Name
            }).ToList();
            var root = new MenuTree
            {
                Title = "顶级菜单", 
                Id = Guid.Empty.ToString(),
                ParentGuid = null
            };
            temp.Insert(0, root);
            var tree = temp.BuildTree(selectedGuid?.ToString());
            return new ResultDataModel() { Data = tree };
        }

        public ResultDataModel UpdateStatus(UserStateEnums status, string ids)
        {
            List<string> idList = ids.Split(',').ToList();
            var entityList = _menuRepository.Entities.Where(f => idList.Any(s => s == f.Id.ToString()));
            if (entityList != null)
            {
                foreach (var item in entityList)
                {
                    item.Status = (int)status;
                }
            }
            var result = _menuRepository.Update(entityList);
            var resultData = new ResultDataModel();
            resultData.SetSuccess();
            return resultData;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class MenuTreeHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="selectedGuid"></param>
        /// <returns></returns>
        public static List<MenuTree> BuildTree(this List<MenuTree> menus, string selectedGuid = null)
        {
            var lookup = menus.ToLookup(x => x.ParentGuid);
            Func<Guid?, List<MenuTree>> build = null;
            build = pid =>
            {
                return lookup[pid]
                 .Select(x => new MenuTree()
                 {
                     Id = x.Id,
                     ParentGuid = x.ParentGuid,
                     Title = x.Title,
                     Expand = (x.ParentGuid == null || x.ParentGuid == Guid.Empty),
                     Selected = selectedGuid == x.Id,
                     Children = build(new Guid(x.Id)),
                 })
                 .ToList();
            };
            var result = build(null);
            return result;
        }
    }
}
