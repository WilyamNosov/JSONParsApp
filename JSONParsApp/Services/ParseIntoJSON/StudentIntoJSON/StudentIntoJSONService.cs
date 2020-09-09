using JSONParsApp.Attributes;
using JSONParsApp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JSONParsApp.Services.ParseIntoJSON.StudentIntoJSON
{
    public class StudentIntoJSONService : AnyIntoJSONService<Student>
    {
        public override void AddToBuilder(StringBuilder builder, PropertyInfo prop, Student model)
        {
            var type = prop.PropertyType.Name;
            var propName = GetPropAttributeName(prop);
            var value = prop.GetValue(model);

            AddValue(builder, type, propName, value);
        }

        private string GetPropAttributeName(PropertyInfo prop)
        {
            return prop.GetCustomAttributes<JsonRenameFieldAttribute>().FirstOrDefault()?.Field ?? prop.Name;
        }
    }
}
