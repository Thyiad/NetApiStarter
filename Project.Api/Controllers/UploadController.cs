using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Helper;
using Project.Helper.Ioc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Sys;
using Project.Model.sys;
using System.IO;

namespace Project.Api.Controllers
{
    /// <summary>
    /// 上传相关
    /// </summary>
    public class UploadController : ApiController
    {
        public UploadController(AutowiredService autowiredService)
        {
            autowiredService.Autowired(this);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult Uploadfile()
        {
            var formFile = Request.Form.Files.FirstOrDefault();

            var appSetting = AppSettings.Instance;
            if (formFile == null || formFile.Length == 0)
            {
                return new AjaxResult { Code = AjaxCode.OK, Msg = "文件不能为空" };
            }
            if (formFile.Length > appSetting.Upload.LimitSize)
            {
                return new AjaxResult { Code = AjaxCode.OK, Msg = "文件超过了最大限制" };
            }
            var ext = Path.GetExtension(formFile.FileName).ToLower();
            if (!appSetting.Upload.AllowExts.Contains(ext))
            {
                return new AjaxResult { Code = AjaxCode.OK, Msg = "文件类型不允许" };
            }
            //上传逻辑
            var now = DateTime.Now;
            var yy = now.ToString("yyyy");
            var mm = now.ToString("MM");
            var dd = now.ToString("dd");
            var fileName = Guid.NewGuid().ToString("n") + ext;

            var folder = Path.Combine(appSetting.Upload.UploadPath, yy, mm, dd);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var filePath = Path.Combine(folder, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                formFile.CopyTo(fileStream);
                fileStream.Flush(true);
            }

            var fileUrl = $"{appSetting.RootUrl}{appSetting.Upload.RequestPath}/{yy}/{mm}/{dd}/{fileName}";
            return new AjaxResult { Code = AjaxCode.OK, Msg = "文件类型不允许", Data = fileUrl };
        }
    }
}