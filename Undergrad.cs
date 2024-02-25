/////////////////////////////////////////////
// date        developer        change history
// 2.13.2024   Schlecht C       initial creation of this Undergrad class and all methods for undergrad

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDbApp
{
    public enum YearRank
    {
        //this YearRank type represents the students academic year ranking
        Freshman = 1,
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }

    //undergrad class is a kind of student, inherits from student
    internal class Undergrad : Student
    {
        public YearRank Rank { get; set; }
        public string DegreeMajor { get; set; }


        //undergrad constructor calls base class constructor of derived class
        //student constructor is generic to all students, specific undergrad student info here
        public Undergrad(string first, string last, string email, 
                        double gpa, YearRank year, string major)
            : base(first, last, email, gpa)
        {
            Rank = year;
            DegreeMajor = major;
        }


        // This method returns the formatted labeled information for Undergrad students
        public override string ToString()
        {
            return base.ToString() + $"   Year: {Rank}\n  Major: {DegreeMajor}\n";
        }
        
        // This methods formats undergrad student info seperating tokens so there is a single token on each line
        // prepares student data for output file reading
        public override string ToStringForOutputFile()
        {
            string str = this.GetType().Name + "\n";
            str += base.ToStringForOutputFile() +"\n";
            str += $"{Rank}\n";
            str += $"{DegreeMajor}";

            return str;

        }


    }
}
