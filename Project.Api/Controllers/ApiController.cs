using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Project.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ApiController : ControllerBase
    {
        protected string CurrentUserId {
            get
            {
               var user =  this.HttpContext.User;
               string userId = user?.FindFirst("userid")?.Value;
                
                return userId;
            }
        }
    }
}
