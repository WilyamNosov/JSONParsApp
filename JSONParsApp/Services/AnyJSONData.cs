using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JSONParsApp.Services
{
    public class AnyJSONData<T> : IDefaultJSONData<T>
    {
        public string GetJSONFile()
        {
            var result = "";

            using (StreamReader sr = new StreamReader("../../../JSONs/AnyModel.json"))
            {
                result = sr.ReadToEnd();
            }

            return result;
        }
    }
}
