using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Helper;
using Project.Helper.Ioc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Sys;
using Project.Api.Services;

namespace Project.Api.Controllers
{
    public class LoginRequest {
        public string Account { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// 用户相关服务
    /// </summary>
    public class UserController : ApiController
    {
        [Autowired] UserService userService;
        public UserController(AutowiredService autowiredService)
        {
            autowiredService.Autowired(this);
        }

        /// <summary>
        /// 登录，获取jwt
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public AjaxResult Login([FromBody]LoginRequest request)
        {
            if (request == null)
            {
                return new AjaxResult { Code = AjaxCode.ERROR, Msg = "" };
            }

            string jwt = userService.Login(request.Account, request.Password);

            if (string.IsNullOrEmpty(jwt)) return new AjaxResult { Code = AjaxCode.ERROR, Msg = "用户名或密码错误" };
            else return new AjaxResult { Code = AjaxCode.OK, Msg = "", Data = new { jwt = jwt } };
        }
    }
}