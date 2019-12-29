using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Project.Helper;
using Project.Helper.Ioc;
using Project.Model;
using Project.Model.sys;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Project.Api.Services
{
    [AppService]
    public class UserService
    {
        [Autowired]
        private ILogger<UserService> _logger;

        public UserService(AutowiredService autowiredService)
        {
            autowiredService.Autowired(this);
        }

        /// <summary>
        /// 登录，获取jwt
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string Login(string account, string password)
        {
            using (DbEntities context = new DbEntities())
            {
                // 效验身份
                var user = context.Users.FirstOrDefault();
                if (user == null) 
                {
                   
                }

                // 生成token
                var dict = new Dictionary<string, string>();
                dict.Add(Const.TOKEN_USERID, "0");
                dict.Add(Const.TOKEN_USERROLE, "0");

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Instance.Jwt.SigningKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                
                var token = new JwtSecurityToken(
                    issuer: AppSettings.Instance.Jwt.Issuer,
                    audience: AppSettings.Instance.Jwt.Audience,
                    claims: dict.Select(x => new Claim(x.Key, x.Value)),
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: creds);
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
        }
    }
}
