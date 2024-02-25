//////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// date         developer       changes
/// 1.25.2024    Schlecht, C     Inital creation of this Program.cs file, Student.cs file, began creating objects
///                              class Student
///  2.1.2024    Schlecht, C     Added methods
///  2.6.2024   Schlecht, C      commented out MainTest1() as it was not necessary for advancing the app

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StudentDbApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // static type on left, dynamic type on right
            DbApp app = new DbApp();
            
        }
            /*static void MainTest1()
            {
                //do some basic development testing as the DB and the student class
                //are developed
                Console.WriteLine("BEGIN STUDENT DB APP\n\n");

            //what do I want to do with this database?
            //we want to make students for the database
            //many student objects, stu1 and stu2 created with default constructor
            //stu3, stu4 created with full-spec constructor
            Student stu1 = new Student();
            Student stu2 = new Student();
            Student stu3 = new Student("Carlos", "Castaneda", "ccastaneda", 2.5, YearRank.Sophomore);
            Student stu4 = new Student("David", "Davis", "ddavis@uw.edu", 1.5,  YearRank.Freshman);

            //first student for testing
            stu1.FirstName = "Alice";
            stu1.LastName = "Anderson";
            stu1.EmailAddress = "aanderson@uw.edu";
            stu1.GradePtAvg = 4.0;
            stu1.Rank = YearRank.Junior;

            //second student for testing
            stu2.FirstName = "Bob";
            stu2.LastName = "Bradshaw";
            stu2.EmailAddress = "bbradshaw@uw.edu";
            stu2.GradePtAvg = 3.5;
            stu2.Rank = YearRank.Senior;

            //test console writing of student info
            //student record stu1
            Console.WriteLine("*********Student Record*************");
            Console.WriteLine($"First: {stu1.FirstName}");
            Console.WriteLine($" Last: {stu1.LastName}");
            Console.WriteLine($"Email: {stu1.EmailAddress}");
            Console.WriteLine($"  GPA: {stu1.GradePtAvg}");
            Console.WriteLine($" Rank: {stu1.Rank}\n");

            //student record stu2
            Console.WriteLine("*********Student Record*************");
            Console.WriteLine($"First: {stu2.FirstName}");
            Console.WriteLine($" Last: {stu2.LastName}");
            Console.WriteLine($"Email: {stu2.EmailAddress}");
            Console.WriteLine($"  GPA: {stu2.GradePtAvg}");
            Console.WriteLine($" Rank: {stu2.Rank} \n");

            //print student info using toString method
            Console.WriteLine(stu3);
            Console.WriteLine(stu4);    

            Console.WriteLine("\n\nEND STUDENT DB APP");
        }*/
    }
}
