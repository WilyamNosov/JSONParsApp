using System;
using System.Collections.Generic;
using System.Text;

namespace JSONParsApp.Models
{
    public class Lecturer : Person
    {
        public string Subject { get; set; }
        public string DegreeLevel { get; set; }
    }
}
