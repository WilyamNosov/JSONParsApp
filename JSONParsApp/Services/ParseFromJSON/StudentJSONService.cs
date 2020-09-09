using JSONParsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParsApp.Services.ParseFromJSON
{
    public class StudentJSONService : ParseJSONService<Student>
    {
        public StudentJSONService()
        {
            _defaultJSONData = new StudentJSONData();
        }
    }
}
