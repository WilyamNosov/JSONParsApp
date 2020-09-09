using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace JSONParsApp.Services
{
    public class DefaultJSONData<T> : IDefaultJSONData<T>
    {
        public virtual string GetJSONFile()
        {
            var result = "";

            using (StreamReader sr = new StreamReader("../../../JSONs/Lecturers.json"))
            {
                result = sr.ReadToEnd();
            }

            using (StreamReader sr = new StreamReader("../../../JSONs/Students.json"))
            {
                result += sr.ReadToEnd();
            }

            result = ConnectData(result);
            return result;
        }

        private static string ConnectData(string inputData)
        {
            var regex = new Regex(@"\[");
            inputData = regex.Replace(inputData, "");
            regex = new Regex(@"\]");
            inputData = regex.Replace(inputData, ",");
            inputData = inputData.Insert(0, "[");
            inputData += "]";
            return inputData;
        }
    }
}
