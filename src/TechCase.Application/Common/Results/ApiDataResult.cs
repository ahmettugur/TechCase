using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TechCase.Application.Common.Results
{
    public class ApiDataResult<T> : IDataResult<T> where T : new()
    {
        public ApiDataResult(int code, T data, List<string> errors = null)
        {
            Code = code;
            Data = data;
            Errors = errors;
        }

        public ApiDataResult()
        {
        }

        public string Version => "1.0";
        public int Code { get; private set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Errors { get; set; }
    }
}
