using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApi.Auth;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;

        public AuthController(IOptions<AppAuthenticationSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult Auth(string username, string password)
        {
            //var response = ResponseModelFactory.CreateInstance;
            //DncUser user;
            //using (_dbContext)
            //{
            //    user = _dbContext.DncUser.FirstOrDefault(x => x.LoginName == username.Trim());
            //    if (user == null || user.IsDeleted == IsDeleted.Yes)
            //    {
            //        response.SetFailed("用户不存在");
            //        return Ok(response);
            //    }
            //    if (user.Password != password.Trim())
            //    {
            //        response.SetFailed("密码不正确");
            //        return Ok(response);
            //    }
            //    if (user.IsLocked == IsLocked.Locked)
            //    {
            //        response.SetFailed("账号已被锁定");
            //        return Ok(response);
            //    }
            //    if (user.Status == UserStatus.Forbidden)
            //    {
            //        response.SetFailed("账号已被禁用");
            //        return Ok(response);
            //    }
            //}
            string userId = Guid.NewGuid().ToString();
            var claimsIdentity = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("guid",userId),
                    new Claim("avatar",""),
                    new Claim("displayName","Admin"),
                    new Claim("loginName","Admin"),
                    new Claim("emailAddress",""),
                    new Claim("userType",(1).ToString())
                });
            var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);

            return Ok(token);
        }
    }
}