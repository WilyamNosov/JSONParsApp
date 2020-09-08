using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JSONParsApp.Services
{
    public class StudentJSONData : DefaultJSONData
    {
        public override string GetJSONFile()
        {
            var result = "";

            using (StreamReader sr = new StreamReader("../../../JSONs/Students.json"))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }
    }
}
