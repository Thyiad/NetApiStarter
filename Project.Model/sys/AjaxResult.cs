using Newtonsoft.Json;
using System;

namespace Project.Model.Sys
{
    public enum AjaxCode
    {
        OK = 2000,
        ERROR = 3000,
        TOKEN_ERROR = 4000,
        TOKEN_EXPERIED = 4001,
    }

    public class AjaxResult
    {
        [JsonProperty("code")]
        public AjaxCode Code { get; set; } = AjaxCode.OK;
        [JsonProperty("msg")]
        public string Msg { get; set; }
        [JsonProperty("data")]
        public object Data { get; set; }
    }
}