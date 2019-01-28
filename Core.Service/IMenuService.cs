using System;
using System.Collections.Generic;
using System.Text;
using Core.Common.Enums;
using Core.Service.Models;
using Core.Service.Models.MenuView;

namespace Core.Service
{
    public interface IMenuService
    {
        /// <summary>
        /// 分页获取菜单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultDataModel GetPage(MenuRequestModel model);

        /// <summary>
        /// 加载菜单树形结构
        /// </summary>
        /// <param name="selectedGuid"></param>
        /// <returns></returns>
        ResultDataModel LoadMenuTreeData(Guid? selectedGuid);

        /// <summary>
        /// 保存编辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        ResultDataModel SaveEdit(MenuCreateViewModel model, AuthContextUser currentUser);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="isDeleted"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        ResultDataModel Delete(IsDeleted isDeleted, string ids);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">菜单状态</param>
        /// <param name="ids">菜单ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        ResultDataModel UpdateStatus(UserStateEnums status, string ids);

        /// <summary>
        /// 根据Id加载菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultDataModel GetMenuUseToEdit(Guid id);

    }
}
