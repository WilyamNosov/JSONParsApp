using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParsApp.Services.ParseFromJSON
{
    public class AnyJSONService<T> : ParseJSONService<T>
    {
        public AnyJSONService()
        {
            _defaultJSONData = new AnyJSONData<T>();
        }
    }
}
