using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParsApp.Attributes
{
    public class JsonRenameFieldAttribute : Attribute
    {
        public string Field { get; set; }

        public JsonRenameFieldAttribute(string name)
        {
            Field = name;
        }
    }
}
