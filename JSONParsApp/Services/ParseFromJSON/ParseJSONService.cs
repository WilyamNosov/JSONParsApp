using JSONParsApp.Models;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.ComponentModel;

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
            var regexSelector = @"{((\S\w*\S:\S\w*-\w*-\w*\S,)|(\S\w*\S:\S?\w*\S?,?)){1,10}}";
            var items = ParseJSON(regexSelector, jsonData);

            foreach (Match item in items)
            {
                result.Add(GenerateNewItem(item));
            }

            return result;
        }

        //private List<Match> RemoveDifferentValues(MatchCollection values)
        //{
        //    var model = (T)Activator.CreateInstance(typeof(T));
        //    var properties = model.GetType().GetProperties().Select(prop => prop.Name);

        //    return values.Where(val => properties.Contains(val.Value.Split(':')[0].Replace("\"", ""))).Select(val => val).ToList();
        //}

        public virtual MatchCollection ParseJSON(string regexSelector, string jsonData)
        {
            jsonData = prepareJsonData(jsonData);
            var regex = new Regex(regexSelector);

            return regex.Matches(jsonData);
        }

        private static string prepareJsonData(string jsonData)
        {
            return jsonData.Replace("  ", "").Replace("\r\n", "").Trim(' ');
        }

        public T GenerateNewItem(Match item)
        {
            var result = (T)Activator.CreateInstance(typeof(T));
            var regexSelector = @"(\S\w*\S:\S\w*-\w*-\w*\S)|(\S\w*\S:\S?\w*\S?)";
            var values = ParseJSON(regexSelector, item.Value);

            foreach (Match value in values)
            {
                var field = value.Value.Split(':')[0].Replace("\"", "");
                var data = value.Value.Split(':')[1].Replace("\"", "").Trim(',');
                AddValue(result, field, data);

            }

            return result;
        }

        private static void AddValue(T model, string field, object value)
        {
            if (model.GetType().GetProperty(field) == null)
            {
                return;
            }

            var saveType = model.GetType().GetProperty(field).PropertyType.FullName;
            var saveValue = Convert.ChangeType(value, Type.GetType(saveType));

            model.GetType().GetProperty(field).SetValue(model, saveValue);
        }
    }
}
