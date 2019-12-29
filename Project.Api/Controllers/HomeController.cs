using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Project.Helper;
using Project.Helper.Ioc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Project.Api.Controllers
{
    public class HomeController : Controller
    {
        [Autowired]
        private ILogger<HomeController> _logger;
        [Autowired]
        private IWebHostEnvironment env;
        public HomeController(AutowiredService autowiredService)
        {
            autowiredService.Autowired(this);
        }

        public IActionResult Index()
        {
            return Content("首页无内容，swagger请访问<a href='/swagger'>/swagger</a>", "text/html", Encoding.UTF8);
        }
    }
}
