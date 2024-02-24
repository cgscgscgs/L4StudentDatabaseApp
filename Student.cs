/////////////////////////////////////////////////////////////////////////////////////////////////
///date             developer           changes
///1.25.2024        Schlecht, C         Initial creation of this Student.cs file, Program.cs file, 
///                                     and fully specified constructors for student objects
///
///                                                


using System;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace StudentDbApp
{
  
    //this class reprents a single record in the database for a student in the school
    //we will determine what parameters we want to store in that record
    //the student class is an example of encapsulation, the first feature of any OOP language. 
    //provides a software component to hold all STATE and BEHAVIOR that has to do with object of that type.
    internal class Student
    {
        //states of the student
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //intuitively chosen as the primary key for our record
        public string EmailAddress { get; set; }
        public double GradePtAvg { get; set; }
        public YearRank Rank { get; set; }

        //fully specified constructor for student objects
        public Student (string fName, string lName, string email, double gpa)
        {
            FirstName = fName;
            LastName = lName; 
            EmailAddress = email;  
            GradePtAvg = gpa;   
        }

        //same as default constructor
        public Student()
        {
        }

        //original presentation fo string to user
        //we need a way to override the ToString method, for student to print itself
        //ToString is part of API, but we must explicitly include it in this class for 
        //student info to print
        public override string ToString()
        {
            //declare a temp string
            string str = string.Empty;
            //build up that string with the info from this object

            str += ("********* Student Record **********\n");
            str += $"  First: {FirstName}\n";
            str += $"   Last: {LastName}\n";
            str += $"  Email: {EmailAddress}\n";
            str += $"    GPA: {GradePtAvg:F2}\n";

            //return the string from this method
            return str;

        }

        //new presentatino of stringto user, formatted for output file, no labels only data
        public virtual string ToStringForOutputFile()
        {
            //declare a temp string
            string str = string.Empty;
            //build up that string with the info from this object

            str += $"{FirstName}\n";
            str += $"{LastName}\n";
            str += $"{EmailAddress}\n";
            str += $"{GradePtAvg:f2}";

            //return the string from this method
            return str;

        }
    }
}
