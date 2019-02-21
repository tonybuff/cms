using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Core.Common.LinqExtensions;
using Core.Models;
using Core.Service.Models;
using Core.Service.Adapter;
using Core.Repository;
using Core.Service.Models.RoleView;
using Core.Common.Enums;

namespace Core.Service.Imp
{
    public class RoleService : IRoleService
    {
        private readonly IRolesRepository _rolesRepository;

        public RoleService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public ResultDataModel GetAllRoles()
        {
            var result = new ResultDataModel();
            result.SetData(_rolesRepository.Entities.ProjectedAsCollection<RoleSelectView, Guid>());
            return result;
        }

        public ResultDataModel GetRoleDetailById(Guid id)
        {
            var result = new ResultDataModel();
            result.SetData(_rolesRepository.GetByKey(id).ProjectedAs<RoleEditView>());
            return result;
        }

        public ResultDataModel GetRoleList(RoleRequestModel model)
        {
            Expression<Func<Roles, bool>> fliter = null;
            if (!string.IsNullOrWhiteSpace(model.Kw))
            {
                fliter = f => f.Name.Contains(model.Kw);
            }
            if (model.IsDeleted > IsDeleted.All)
            {
                if (fliter == null)
                {
                    fliter = f => f.IsDeleted == model.IsDeleted;
                }
                else
                {
                    fliter = fliter.And(f => f.IsDeleted == model.IsDeleted);
                }
            }
            if (model.Status > Status.All)
            {
                if (fliter == null)
                {
                    fliter = f => f.Status == model.Status;
                }
                else
                {
                    fliter = fliter.And(f => f.Status == model.Status);
                }
            }
            var result = new ResultDataModel();
            int total = 0;
            var list = _rolesRepository.GetPages(fliter, model.CurrentPage, model.PageSize, out total).ProjectedAsCollection<RolePageView, Guid>();
            result.SetData(list, total);
            return result;
        }

        public ResultDataModel SaveEdit(RoleEditView model)
        {
            var result = new ResultDataModel();
            var hasExsisteRoleName = _rolesRepository.Entities.Any(f => f.Name==model.Name && f.Id != model.Id);
            if (hasExsisteRoleName)
            {
                result.SetFailed("角色名称已经被使用");
            }
            else
            {
                var entity = _rolesRepository.GetByKey(model.Id);
                if (entity != null)
                {
                    if (!model.IsSuperAdministrator)
                    {
                        result.SetFailed("权限不足！");
                    }
                    else
                    {
                        entity.Name = model.Name;
                        entity.IsDeleted = model.IsDeleted;
                        entity.ModifiedByUserGuid = model.ModifyBy;
                        entity.UpdateDate = DateTime.Now;
                        entity.Status = model.Status;
                        entity.Description = model.Description;
                        _rolesRepository.Update(entity);
                        result.SetSuccess();
                    }
                }
                else
                {
                    entity = model.ProjectedAs<Roles>();
                    entity.CreatedByUserGuid = model.CreateBy;
                    _rolesRepository.Insert(entity);
                    result.SetSuccess();
                }
            }
            return result;
        }

    }
}
