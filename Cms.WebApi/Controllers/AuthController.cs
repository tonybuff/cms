using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Cms.WebApi.Auth;
using Core.Service;
using Core.Service.Models.UserView;
using System.Security.Claims;

namespace Cms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppAuthenticationSettings _appSettings;
        private readonly IUserService _userService;


        public AuthController(IOptions<AppAuthenticationSettings> appSettings, IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Auth(UserLoginModel loginModel)
        {
            var resultData = _userService.Login(loginModel);
            if (resultData.Code == 200)
            {
                var user = resultData.Data as UserViewModel;
                var claimsIdentity = new ClaimsIdentity(new Claim[]
                                    {
                                        new Claim(ClaimTypes.Name, user.UserName),
                                        new Claim("userid",user.UserId.ToString()),
                                        new Claim("avatar",user.Avatar),
                                        new Claim("displayName",user.UserName),
                                        new Claim("loginName",user.UserName),
                                        new Claim("emailAddress",""),
                                        new Claim("IsSuperAdministrator",user.IsSuperAdministrator.ToString())
                                    });
                var token = JwtBearerAuthenticationExtension.GetJwtAccessToken(_appSettings, claimsIdentity);
                resultData.SetData(token);
            }
            return Ok(resultData);
        }
    }
}