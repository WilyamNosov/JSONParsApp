using JSONParsApp.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParsApp.Models
{
    public class Student : Person
    {
        public int Course { get; set; }
        [JsonRenameField("Degree")]
        public string DegreeType { get; set; }
    }
}
