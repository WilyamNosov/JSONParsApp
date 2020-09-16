using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using JsonParseLib.Data;

namespace JsonParseLib.ParseFromJson
{
    public class ParseJsonService<T>
    {
        private static readonly JsonData<T> _jsonData = new JsonData<T>();

        public List<T> ParseFile(string jsonUrl)
        {
            var jsonData = _jsonData.GetJsonFile(jsonUrl);
            var items = ConverJSONToObjectArray(jsonData);

            return items;
        }

        public virtual List<T> ConverJSONToObjectArray(string jsonData)
        {
            var result = new List<T>();
            var regexSelector = @"{((\S\w*\S:\S\w*-\w*-\w*\S,)|(\S\w*\S:\S?\w*\S?,?)){1,100}}";
            var items = ParseJSON(regexSelector, jsonData);

            foreach (Match item in items)
            {
                var generatedItem = GenerateNewItem(item);
                result.Add(generatedItem);
            }

            
            return result;
        }

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

        private void AddValue(T model, string field, object value)
        {
            var property = model.GetType().GetProperty(field);

            if (property == null)
            {
                return;
            }

            var saveType = property.PropertyType;
            var saveValue = Convert.ChangeType(value, saveType);

            property.SetValue(model, saveValue);
        }
    }
}
