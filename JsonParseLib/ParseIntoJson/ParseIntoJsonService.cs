using JsonParseLib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JsonParseLib.ParseIntoJson
{
    public class ParseIntoJsonService<T>
    {
        public string ConvertToJSONByModel(T model)
        {
            return BuildJSONData(model);
        }

        private string BuildJSONData(T model)
        {
            var builder = new StringBuilder("{\n");

            foreach (var prop in model.GetType().GetProperties())
            {
                AddToBuilder(builder, prop, model);
            }

            builder.Append("}");
            return builder.ToString();
        }

        private void AddToBuilder(StringBuilder builder, PropertyInfo prop, T model)
        {
            var type = prop.PropertyType;
            var valueName = GetPropAttributeName(prop);
            var value = prop.GetValue(model);

            AddValue(builder, type, valueName, value);
        }

        private void AddValue(StringBuilder builder, Type type, string name, object value)
        {
            if (type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64))
            {
                builder.AppendFormat("\t{0}:{1}, \n", name, value);
            }
            else
            {
                builder.AppendFormat("\t{0}:\"{1}\", \n", name, value);
            }
        }

        private string GetPropAttributeName(PropertyInfo prop)
        {
            return prop.GetCustomAttributes<JsonRenameFieldAttribute>().FirstOrDefault()?.Field ?? prop.Name;
        }
    }
}
