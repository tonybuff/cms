using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Service.Models.RoleView;
using Cms.WebApi.AuthContext;

namespace Cms.WebApi.Controllers.Api.V1.Rbac
{
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        
        [HttpGet("/api/v1/rbac/role/get_all_roles")]
        public IActionResult GetAllRoles()
        {
            return Ok(_roleService.GetAllRoles());
        }

        [HttpPost]
        public IActionResult List(RoleRequestModel model)
        {
            return Ok(_roleService.GetRoleList(model));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid id)
        {
            return Ok(_roleService.GetRoleDetailById(id));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(RoleEditView model)
        {
            model.ModifyBy = AuthContextService.CurrentUser.UserId;
            model.IsSuperAdministrator = AuthContextService.IsSupperAdministator;
            return Ok(_roleService.SaveEdit(model));
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(RoleEditView model)
        {
            model.CreateBy = AuthContextService.CurrentUser.UserId;
            return Ok(_roleService.SaveEdit(model));
        }
    }
}