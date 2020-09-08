using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParsApp.Services.ParseIntoJSON
{
    public interface IParseIntoJSONService<T>
    {
        string BuildJSONData(T model);
        JObject ConvertToJSONByModel(T model);
    }
}
