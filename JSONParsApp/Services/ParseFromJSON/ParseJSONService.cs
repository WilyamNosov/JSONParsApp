using JSONParsApp.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace JSONParsApp.Services.ParseFromJSON
{
    public abstract class ParseJSONService<T>
    {
        protected IDefaultJSONData<T> _defaultJSONData;

        public List<T> ParseFile()
        {
            var jsonData = _defaultJSONData.GetJSONFile();
            var persons = ConverJSONToObjectArray(jsonData);

            return persons;
        }

        public virtual List<T> ConverJSONToObjectArray(string jsonData)
        {
            var result = new List<T>();
            var countOfFields = typeof(T).GetProperties().Length;
            var values = ParseJSON(jsonData);
            var sortedValues = RemoveDifferentValues(values);

            for (int i = 0; i < sortedValues.Count / countOfFields; i++)
            {
                result.Add(GenerateNewItem(sortedValues, i, countOfFields));
            }

            return result;
        }

        private List<Match> RemoveDifferentValues(MatchCollection values)
        {
            var model = (T)Activator.CreateInstance(typeof(T));
            var properties = model.GetType().GetProperties().Select(prop => prop.Name);

            return values.Where(val => properties.Contains(val.Value.Split(':')[0].Replace("\"", ""))).Select(val => val).ToList();
        }

        public virtual MatchCollection ParseJSON(string jsonData)
        {
            var regexSelector = @"(\S\w*\S:\S\w*-\w*-\w*\S)|(\S\w*\S:\S?\w*\S?)";
            var regex = new Regex(regexSelector);

            return regex.Matches(jsonData);
        }

        public T GenerateNewItem(List<Match> values, int beginIndex, int countOfFields)
        {
            var result = (T)Activator.CreateInstance(typeof(T));
            
            for (int i = 0; i < countOfFields; i++)
            {
                var value = values[beginIndex * countOfFields + i].ToString().Split(':')[1].Replace("\"", "").Trim(',');

                AddValue(result, value, values, beginIndex, countOfFields, i);
            }

            return result;
        }

        private static void AddValue(T model, string value, List<Match> values, int beginIndex, int countOfFields, int insertIndex)
        {
            if (model.GetType()
                .GetProperty(values[beginIndex * countOfFields + insertIndex].ToString()
                .Split(':')[0].Replace("\"", "")).PropertyType.Name == "Int32")
            {
                model
                    .GetType().GetProperty(values[beginIndex * countOfFields + insertIndex].ToString().Split(':')[0].Replace("\"", ""))
                    .SetValue(model, Int32.Parse(value));
            }
            else if (model.GetType()
                .GetProperty(values[beginIndex * countOfFields + insertIndex].ToString()
                .Split(':')[0].Replace("\"", "")).PropertyType.Name == "DateTime")
            {
                model
                    .GetType().GetProperty(values[beginIndex * countOfFields + insertIndex].ToString().Split(':')[0].Replace("\"", ""))
                    .SetValue(model, DateTime.Parse(value));
            }
            else
            {
                model
                    .GetType().GetProperty(values[beginIndex * countOfFields + insertIndex].ToString().Split(':')[0].Replace("\"", ""))
                    .SetValue(model, value);
            }
        }
    }
}
