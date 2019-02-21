using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.WebApi.AuthContext;
using Core.Common.Enums;
using Core.Service;
using Core.Service.Models;
using Core.Service.Models.UserView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.WebApi.Controllers.Api.V1.Rbac
{
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult List(UserRequestModel userRequest)
        {
            return Ok(_userService.GetPageList(userRequest));
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="model">用户视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(UserEditViewModel model)
        {
            string errorMsg = "";
            ResultDataModel resultDataModel = new ResultDataModel();
            if (!ModelState.IsValid)
            {
                foreach (var item in ModelState.Values.Select(s=>s.Errors))
                {
                    var itemError = item.FirstOrDefault();
                    errorMsg += itemError.ErrorMessage+"<br/>";
                } 
            }

            if(!string.IsNullOrWhiteSpace(errorMsg))
            {
                resultDataModel.SetFailed(errorMsg);
                return Ok(resultDataModel);
            }

            model.CreateBy = AuthContextService.CurrentUser.UserId;

            return Ok(_userService.CreateUser(model));
        }

        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            return Ok(_userService.GetUserById(guid));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(UserEditViewModel model)
        {
            model.ModifyBy = AuthContextService.CurrentUser.UserId;
            return Ok(_userService.UpdateUser(model));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids">用户GUID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            return Ok(_userService.UpdateIsDelete(IsDeleted.Yes, ids));
        }

        /// <summary>
        /// 恢复已删除的用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Recover(string ids)
        {
           return Ok(_userService.UpdateIsDelete(IsDeleted.No, ids));
        }

        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param name="command"></param>
        /// <param name="ids">用户ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            var response = new ResultDataModel();
            switch (command)
            {
                case "delete":
                    response = _userService.UpdateIsDelete(IsDeleted.Yes, ids);
                    break;
                case "recover":
                    response = _userService.UpdateIsDelete(IsDeleted.No, ids);
                    break;
                case "forbidden":
                    response = _userService.UpdateStatus(UserStateEnums.Forbidden, ids);
                    break;
                case "normal":
                    response = _userService.UpdateStatus(UserStateEnums.Normal, ids);
                    break;
                default:
                    break;
            }
            return Ok(response);
        }
    }
}