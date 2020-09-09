using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JSONParsApp.Services.ParseIntoJSON
{
    public class AnyIntoJSONService<T> : IParseIntoJSONService<T>
    {
        public JObject ConvertToJSONByModel(T model)
        {
            var buildedString = BuildJSONData(model);
            return JObject.Parse(buildedString);
        }

        public string BuildJSONData(T model)
        {
            var builder = new StringBuilder("{\n");

            foreach (var prop in model.GetType().GetProperties())
            {
                AddToBuilder(builder, prop, model);
            }

            builder.Append("}");
            return builder.ToString();
        }

        public virtual void AddToBuilder(StringBuilder builder, PropertyInfo prop, T model)
        {
            var type = prop.PropertyType.Name;
            var valueName = prop.Name;
            var value = prop.GetValue(model);

            AddValue(builder, type, valueName, value);
        }

        protected void AddValue(StringBuilder builder, string type, string name, object value)
        {
            if (type == "Int32")
            {
                builder.AppendFormat("\t{0}:{1}, \n", name, value);
            }
            else
            {
                builder.AppendFormat("\t{0}:\"{1}\", \n", name, value);
            }
        }
    }
}
