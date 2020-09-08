using JSONParsApp.Models;
using JSONParsApp.Services;
using JSONParsApp.Services.ParseFromJSON;
using JSONParsApp.Services.ParseIntoJSON.StudentIntoJSON;
using JSONParsApp.Services.ParseIntoJSON.Lecturer;
using System;
using System.Collections.Generic;

namespace JSONParsApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var parseS = new ParseJSONService<Student>();
            var S = parseS.ParseFile();

            var parseL = new ParseJSONService<Lecturer>();
            var L = parseL.ParseFile();

            var parseD = new ParseJSONService<Person>();
            var D = parseD.ParseFile();

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
            resultL.ToString();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
