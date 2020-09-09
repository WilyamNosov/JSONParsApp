using JSONParsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParsApp.Services.ParseFromJSON
{
    public class DefaultJSONService : ParseJSONService<Person>
    {
        public DefaultJSONService()
        {
            _defaultJSONData = new DefaultJSONData<Person>();
        }
    }
}
