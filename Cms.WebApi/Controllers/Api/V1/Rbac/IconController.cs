using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Service.Models.IconsViewModel;
using Core.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cms.WebApi.Controllers.Api.V1.Rbac
{
    [Route("api/v1/rbac/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class IconController : ControllerBase
    {
        private readonly IIConsService _iconsService;

        public IconController(IIConsService iconsService)
        {
            _iconsService = iconsService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(IconsRequestModel model)
        {
            return Ok(_iconsService.GetList(model));
        }

        [HttpGet("/api/v1/rbac/icon/find_list_by_kw/{kw?}")]
        public IActionResult FindByKeyword(string kw)
        {
            return Ok(_iconsService.FindIconByKey(kw));
        }
    }
}
