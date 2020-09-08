using JSONParsApp.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace JSONParsApp.Services.ParseFromJSON
{
    public class ParseJSONService<T>
        where T : Person
    {
        private readonly IDefaultJSONData _defaultJSONData;

        public ParseJSONService()
        {
            if (typeof(T) == typeof(Student))
            {
                _defaultJSONData = new StudentJSONData();
            }
            else if (typeof(T) == typeof(Lecturer))
            {
                _defaultJSONData = new LecturerJSONData();
            }
            else
            {
                _defaultJSONData = new DefaultJSONData();
            }
        }

        public List<T> ParseFile()
        {
            var jsonData = _defaultJSONData.GetJSONFile();
            var persons = JsonConvert.DeserializeObject<List<T>>(jsonData);

            return persons;
        }
    }
}
