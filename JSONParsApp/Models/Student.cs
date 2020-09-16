using JsonParseLib.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParsApp.Models
{
    public class Student : Person
    {
        [JsonRenameField("YearStudies")]
        public int Course { get; set; }
        [JsonRenameField("Degree")]
        public string DegreeType { get; set; }
        public bool TestBool { get; set; }
        public long TestLong { get; set; }
        public double TestDouble { get; set; }
        public float TestFloat { get; set; }
        public DateTime TestDateTime { get; set; }
    }
}
