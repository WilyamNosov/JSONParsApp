using JSONParsApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JSONParsApp.Services.ParseIntoJSON.StudentIntoJSON
{
    public class StudentIntoJSONService : IStudentIntoJSONService
    {
        public JObject ConvertToJSONByModel(Student model)
        {
            var buildedString = BuildJSONData(model);
            return JObject.Parse(buildedString);
        }

        public string BuildJSONData(Student model)
        {
            var builder = new StringBuilder("{\n");

            foreach(var prop in model.GetType().GetProperties())
            {
                AddToBuilder(builder, prop, model);
            }

            builder.Append("}");
            return builder.ToString();
        }

        private void AddToBuilder(StringBuilder builder, PropertyInfo prop, Student model)
        {
            var propName = GetPropAttributeName(prop);

            if (prop.PropertyType.Name == "String")
            {
                builder.AppendFormat("\t{0}:\"{1}\", \n", propName, prop.GetValue(model));
            } 
            else if (prop.PropertyType.Name == "Int32")
            {
                builder.AppendFormat("\t{0}:{1}, \n", propName, prop.GetValue(model));
            }
            else
            {
                builder.AppendFormat("\t{0}:\"{1}\", \n", propName, prop.GetValue(model));
            }

        }

        private string GetPropAttributeName(PropertyInfo prop)
        {
            if (prop.GetCustomAttributes(true).Length > 0)
            {
                var attrs = prop.GetCustomAttributes(true);
                var renameAttr = attrs.Select(attr => attr.GetType().GetProperties().Where(p => p.Name == "Field").FirstOrDefault()).FirstOrDefault();
                var result = attrs.Where(attr => !(renameAttr.GetValue(attr) is Exception)).Select(attr => renameAttr.GetValue(attr)).FirstOrDefault().ToString();
                return result;
            }
            else
            {
                return prop.Name;
            }
        }
    }
}
