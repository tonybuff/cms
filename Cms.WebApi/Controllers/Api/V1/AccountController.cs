using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Cms.WebApi.AuthContext;

namespace Cms.WebApi.Controllers.Api.V1
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var userId = AuthContextService.CurrentUser.UserId;
            var resultData = _userService.GetCurrentUserProfile(userId);
            return Ok(resultData);
        }
    }
}
