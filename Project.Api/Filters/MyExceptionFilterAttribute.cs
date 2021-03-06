﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Project.Helper.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.Filters
{
    [AppService]
    public class MyExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILogger<MyExceptionFilterAttribute> _logger;
        public MyExceptionFilterAttribute(ILogger<MyExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            _logger.LogError(context.Exception, context.Exception.Message);
            return base.OnExceptionAsync(context);
        }
    }
}
