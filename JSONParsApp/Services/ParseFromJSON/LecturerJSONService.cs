using JSONParsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParsApp.Services.ParseFromJSON
{
    public class LecturerJSONService : ParseJSONService<Lecturer>
    {
        public LecturerJSONService()
        {
            _defaultJSONData = new LecturerJSONData();
        }
    }
}
