using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Service.Models.MenuView;
using Core.Service;
using Core.Common.Enums;
using Core.Service.Models;

namespace Cms.WebApi.Controllers.Api.V1.Rbac
{
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {

        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(MenuRequestModel model)
        {
            var resultData = _menuService.GetPage(model);
            return Ok(resultData);
        }

        /// <summary>
        /// 加载被选中的树形菜单
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        [HttpGet("{selected?}")]
        public IActionResult Tree(Guid? selected)
        {
            var resultData = _menuService.LoadMenuTreeData(selected);
            return Ok(resultData);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="model">菜单视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(MenuCreateViewModel model)
        {
            return Ok(_menuService.SaveEdit(model, AuthContext.AuthContextService.CurrentUser));
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="guid">菜单ID</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            return Ok(_menuService.GetMenuUseToEdit(guid));
        }

        /// <summary>
        /// 保存编辑后的菜单信息
        /// </summary>
        /// <param name="model">菜单视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(MenuCreateViewModel model)
        {
            return Ok(_menuService.SaveEdit(model, AuthContext.AuthContextService.CurrentUser));
        }


        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="ids">菜单ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            return Ok(_menuService.Delete(IsDeleted.Yes, ids));
        }

        /// <summary>
        /// 恢复菜单
        /// </summary>
        /// <param name="ids">菜单ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Recover(string ids)
        {
            var response = _menuService.Delete(IsDeleted.No, ids);
            return Ok(response);
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">菜单ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            var result = new ResultDataModel();
            switch (command)
            {
                case "delete":
                    result=_menuService.Delete(IsDeleted.Yes, ids);
                    break;
                case "recover":
                    result=_menuService.Delete(IsDeleted.No, ids);
                    break;
                case "forbidden":
                    result = _menuService.UpdateStatus(UserStateEnums.Forbidden, ids);
                    break;
                case "normal":
                    result = _menuService.UpdateStatus(UserStateEnums.Normal, ids);
                    break;
                default:
                    break;
            }
            return Ok(result);
        }
    }
}
