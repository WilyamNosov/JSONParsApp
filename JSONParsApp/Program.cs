using JSONParsApp.Models;
using System;
using System.Collections.Generic;
using JsonParseLib.ParseFromJson;
using JsonParseLib.ParseIntoJson;
using Newtonsoft.Json.Linq;

namespace JSONParsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var myNewParse = new ParseJsonService<Student>();
            var res = myNewParse.ParseFile("../../../JSONs/Students.json");

            var myNewParseInto = new ParseIntoJsonService<Student>();
            var resInto = myNewParseInto.ConvertToJSONByModel(new Student()
            {
                Age = 12,
                BirthDay = DateTime.Now.Date,
                Course = 1,
                FirstName = "Test",
                SecondName = "Test",
                Gender = "Male",
                DegreeType = "Bachalor",
                TestBool = true,
                TestDateTime = DateTime.Now,
                TestDouble = 12.5,
                TestFloat = 256.35F,
                TestLong = 1203486
            });

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
