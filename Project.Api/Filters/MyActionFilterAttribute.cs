
using Project.Helper;
using Project.Helper.Ioc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Project.Api.Filters
{
    [AppService]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MyActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 是否验证参数有效性
        /// </summary>
        public bool ValidParams { get; set; } = true;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ////由于Filters是套娃模式，使用以下逻辑保证作用域的覆盖 Action > Controller > Global
            //if (context.Filters.OfType<MyActionFilterAttribute>().Last() != this)
            //{
            //    return;
            //}

            ////默认只有一个参数
            //var firstParam = context.ActionArguments.FirstOrDefault().Value;
            //if (firstParam != null && firstParam.GetType().IsClass)
            //{
            //    //验证参数合法性
            //    if (ValidParams)
            //    {
            //        var validationResults = new List<ValidationResult>();
            //        var validationFlag = Validator.TryValidateObject(firstParam, new ValidationContext(firstParam), validationResults, false);

            //        if (!validationFlag)
            //        {
            //            var ro = new { Msg = validationResults.First().ErrorMessage };
            //            context.Result = new JsonResult(ro);
            //            return;
            //        }
            //    }
            //}

            base.OnActionExecuting(context);
        }
    }
}
