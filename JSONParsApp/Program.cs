using JSONParsApp.Models;
using JSONParsApp.Services;
using JSONParsApp.Services.ParseFromJSON;
using JSONParsApp.Services.ParseIntoJSON.StudentIntoJSON;
using JSONParsApp.Services.ParseIntoJSON.Lecturer;
using System;
using System.Collections.Generic;
using JSONParsApp.Services.ParseIntoJSON;
using JsonParseLib.ParseFromJson;
using JsonParseLib.ParseIntoJson;

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
                DegreeType = "Bachalor"
            });

            var parseS = new StudentJSONService();
            var S = parseS.ParseFile();

            var parseL = new LecturerJSONService();
            var L = parseL.ParseFile();

            var parseP = new DefaultJSONService();
            var P = parseP.ParseFile();

            var parseIntoS = new StudentIntoJSONService();
            var resultS = parseIntoS.ConvertToJSONByModel(new Student()
            {
                Age = 12,
                BirthDay = DateTime.Now.Date,
                Course = 1,
                FirstName = "Test",
                SecondName = "Test",
                Gender = "Male",
                DegreeType = "Bachalor"
            });
            resultS.ToString();

            var parseIntoL = new LecturerIntoJSONService();
            var resultL = parseIntoL.ConvertToJSONByModel(new Lecturer()
            {
                Age = 12,
                BirthDay = DateTime.Now.Date,
                DegreeLevel = "Doctor",
                FirstName = "Test",
                SecondName = "Test",
                Gender = "Male",
                Subject = "IT"
            });

            //##########ANY##########

            var parseAny = new AnyJSONService<Student>();
            var Any = parseAny.ParseFile(); 
            
            var parseIntoSAny = new AnyIntoJSONService<Student>();
            var resultSAny = parseIntoSAny.ConvertToJSONByModel(new Student()
            {
                Age = 12,
                BirthDay = DateTime.Now.Date,
                Course = 1,
                FirstName = "Test",
                SecondName = "Test",
                Gender = "Male",
                DegreeType = "Bachalor"
            });


            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
