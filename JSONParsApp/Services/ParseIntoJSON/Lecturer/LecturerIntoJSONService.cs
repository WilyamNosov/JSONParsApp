using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JSONParsApp.Services.ParseIntoJSON.Lecturer
{
    public class LecturerIntoJSONService : ILecturerIntoJSONService
    {
        public string BuildJSONData(Models.Lecturer model)
        {
            var builder = new StringBuilder("{\n");

            foreach (var prop in model.GetType().GetProperties())
            {
                AddToBuilder(builder, prop, model);
            }

            builder.Append("}");
            return builder.ToString();
        }

        public JObject ConvertToJSONByModel(Models.Lecturer model)
        {
            var buildedString = BuildJSONData(model);
            return JObject.Parse(buildedString);
        }

        private void AddToBuilder(StringBuilder builder, PropertyInfo prop, Models.Lecturer model)
        {
            if (prop.PropertyType.Name == "String")
            {
                builder.AppendFormat("\t{0}:\"{1}\", \n", prop.Name, prop.GetValue(model));
            }
            else if (prop.PropertyType.Name == "Int32")
            {
                builder.AppendFormat("\t{0}:{1}, \n", prop.Name, prop.GetValue(model));
            }
            else
            {
                builder.AppendFormat("\t{0}:\"{1}\", \n", prop.Name, prop.GetValue(model));
            }

        }
    }
}
