using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechCase.Application.Common.Results
{
    public class ApiResult : IResult
    {
        public ApiResult(int code, string message = "")
        {
            Code = code;
            Message = message;
        }

        public ApiResult()
        {
        }

        public string Version => "1.0";
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }
        public int Code { get; set; }
    }
}
