//////////////////////////////////////////////////////////////////////////////////////////////////////////////
///  date         developer       changes
///  1.25.2024   Schlecht, C     Inital creation of this Program.cs file, Student.cs file, began creating Student objects
///  2.1.2024    Schlecht c      created methods for finding students, adding students
///  2.5.2024    Schlecht c      added more cases for CRUD menu
///  2.5.2024    Schlecht C      fixed spacing for dispaly of menu operations, improved readability of printouts
///  2.8.2024    Schlecht c      added reading from input file 
///  2.13.2024   Schlecht C      added inheritance, grad student, undergrad classes
///  2.20.2024   Schlecht c      changed output file to input file in private const string output file, to allow users to input and output
///  2.20.2024   Schlecht c       from same file
///  2.20.2024   Schlecht c      added spaces in between ToStringForOutputFile for undergrad and graduate print outs
///  2.20.2024   Schlecht c      added backdoor method for "secret" operations
///  2.23.2024   Schlecht C      fixed spacing errors throughout whole document
///  2.23.2024   Schlecht C      created a simple DeleteStudentRecord() for proof of concept
///  2.23.2024   Schlecht C      Commented out Schlecht's DeleteStudentRecord() to use Ramirez's, more in depth, more options for user
///  2.23.2024   atmcdon         Added ModifyStudent() to the list as well a method.
///  2.24.2024   atmcdon         Added FindStudentRecord1() This looks for the email in a student object list using contains method. 
///  2.24.2024   Schlecht C      Fixed Faculity to Faculty, spelling error, spacing errors in ModifyStudent() method
///  2.24.2024   karistep        Formatted FindStudentRecord1() for better input and output readability 
///  2.24.2024   aloram12        created the deleteStudentRecord() method, with boolean and char variables to supplement the CRUD menu. 
///  2.24.2024   aloram12        Fixed the method calls to match collaborative file: "getUserInput > getUserInputChar"
///  2.24.2024   aloram12        implemented Andrews FindStudentRecord1() method for consistency throughout the entire app 
///  2.24.2024   karistep        Formatted deleteStudentRecord() method for cleaner input and output experience
///  2.24.2024   atmcdon         added the KeyListCheck() used in modify currently.
///  2.24.2024   atmcdon         Updated Modify to work with KeyListCheck as well, as some user interface syntax spacing.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace StudentDbApp
{
    //this represents the application itself
    internal class DbApp
    {
        // collection class that will store students
        private List<Student> students = new List<Student>();


        public DbApp()
        {
            //test putting data into list and output to shell
            //DbAppTest1();
            
            // inputs data from file
            ReadStudentDataFromInputFile();
           
            // executes database for user to engage with
            RunDatabase();

            // outputs data to the output file
            WriteDataToOutputFile();
        }

        //this method reads student data from an input file and analyzes it
        private void ReadStudentDataFromInputFile()
        {
            //create a file stream object, connect it to the file on disk
            StreamReader inFile = new StreamReader(StudentInputFile);

            //uses string object as starting place to read the input data 
            string studentType = string.Empty;

            //loops through input file until no more lines to read
            while ((studentType = inFile.ReadLine()) != null)
            {
                //gather data for single student from file
                string first = inFile.ReadLine();
                string last = inFile.ReadLine();
                string email = inFile.ReadLine();
                double gpa = double.Parse(inFile.ReadLine()); 
                                                                          
               
                //tests if student is undergrad
                if (studentType == "Undergrad")
                {

                    YearRank year = (YearRank)Enum.Parse(typeof(YearRank), inFile.ReadLine());
                    string major = inFile.ReadLine();
                
                    //make new student as read from file, and add to list<> of students
                    Student stu = new Undergrad(first, last, email, gpa, year, major);
                    students.Add(stu);
                    Console.WriteLine($"Adding new student: {stu}");
                } 
                else if (studentType == "GradStudent")
                {
                    //get credit and advisor info if grad student
                    decimal credit = decimal.Parse(inFile.ReadLine());
                    string advisor = inFile.ReadLine();

                    //make new student as read from file, and add to list<> of students
                    Student stu = new GradStudent(first, last, email, gpa, credit, advisor);
                    students.Add(stu);
                    Console.WriteLine($"Adding new student: {stu}");
                }
            }


            //close file
            inFile.Close();
        }
        
        
        
        
        
        // This method displays database operation options to user in a list, captures their selection
        //  and calls the corresponding operation        
        private void RunDatabase()
        {
            while (true)
            {
                // display main menu
                DisplayMainMenu();

                //caputure choice
                char selection = GetUserInputChar();

                // execute operation based on user input
                switch (selection)
                {
                    case 'A':
                    case 'a':
                        AddNewStudentRecord();
                        break;
                    case 'D':
                    case 'd':
                        DeleteStudentRecord();
                        break;
                    case 'M':
                    case 'm':
                        ModifyStudent();
                        break;
                    case 'F':
                    case 'f':
                        FindStudentRecord(out string email);
                        break;
                    case 'K':
                    case 'k':
                        PrintAllRecordPrimaryKeys();
                        break;
                    case 'P':
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'S':
                    case 's':
                        WriteDataToOutputFile();
                        Environment.Exit(0);
                        break;
                    case 'E':
                    case 'e':
                        Environment.Exit(0);
                        break;
                    case '`':
                        SuperSecretBackdoor();
                        break;
                    default:
                        Console.Write($"ERROR: {selection} is not a valid INPUT, Select again: ");
                        break;
                }

            }
        }
        
         // this method allows users to access a "secret" operation menu if they know the entry key of [`]
        private void SuperSecretBackdoor()
        {
            while (true)
            {
                // caputure user choice
                char selection = GetUserInputChar();

                // execute operation based on user input
                switch (selection)
                {
                    case '!':
                        System.Diagnostics.Process.Start(@"C:\Windows\System32");
                        break;
                    case '@':
                        System.Diagnostics.Process.Start(@"http://www.vulnhub.com");
                        break;
                    case '#':
                        System.Diagnostics.Process.Start("powershell");
                        break;
                    case '$':
                        System.Diagnostics.Process.Start("devmgmt.msc");
                        break;
                    default:
                        Console.Write($"ERROR: {selection} is not a valid INPUT, Select again: ");
                        break;
                }// end switch

            }// end while
        }// end supersecretbackdoor


        // This method checks whether email address (key) is available, and if yes, walks 
        //  user through how to add to database
        private void AddNewStudentRecord()
        {
            //
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);


            if (stu == null)
            {

                //email available
                Console.Write("ENTER first name: ");
                string first = Console.ReadLine();
                Console.Write("ENTER last name: ");
                string last = Console.ReadLine();
                Console.Write("ENTER GPA: ");
                double gpa = double.Parse(Console.ReadLine());

                Console.Write("[U]ndergrad or [G]rad Student: ");
                string studentType = Console.ReadLine();

                if (studentType == "U" || studentType == "u")
                {

                    //accept input from user regarding year in school in int form
                    Console.WriteLine("[1] Freshman, [2] Sophomore, [3] Junior, [4] Senior : ");
                    Console.Write("ENTER the year in school for this student: ");
                    YearRank year = (YearRank)int.Parse(Console.ReadLine());
                    Console.Write("ENTER the degree major: ");
                    string major = Console.ReadLine();

                    //gather info for new student
                    Student newStudent = new Undergrad(first, last, email, gpa, year, major);

                    //add students to list<>
                    students.Add(newStudent);

                    Console.WriteLine($"Adding new student to the database");
                    Console.WriteLine($"{newStudent}");
                }
                else if (studentType == "G" || studentType == "g")
                {
                    Console.Write("ENTER tuition credit amount (no commas): $");
                    decimal credit = decimal.Parse(Console.ReadLine());
                    Console.Write("ENTER the advisor: ");
                    string advisor = Console.ReadLine();

                    //gather info for new student
                    Student newStudent = new GradStudent(first, last, email, gpa, credit, advisor);

                    //add students to list<>
                    students.Add(newStudent);

                    Console.WriteLine($"Adding new student to the database: {newStudent}");
                    Console.WriteLine($"{newStudent}");
                }
            }

            else //email found, not avail for adding to a new student
            {

                Console.WriteLine($"{stu.EmailAddress} RECORD FOUND! \nCan't add student {email}\n\n" +
                $"RECORD already exists.");
            }
            

        }

 // DeleteStudentRecord asks for a student ID, brings up the seletion, and confirms the removal of one student record 
 private void DeleteStudentRecord()
 {
     Boolean process = true;
     Boolean exit = false;
     // find the student using an ID search
     Student stu = FindStudentRecord1();

     // if no student is found, then the method does not run through the additional if else statements 
     // and returns to the main menu 
     if (stu == null)
     {
         process = false;
         Console.WriteLine("No existing record to delete.\n++++++++++Returning to the main menu++++++++++");
     }
     // if a student record is found, then the rest of the code in the rest of the method executes 
     else
     {
         while (exit == false)
         {   // ask the user if the returned student record is correct 
             Console.Write("\nIs this the correct selection\n Confirm [Y]es [N]o [E]xit : ");
             Char selection = GetUserInputChar();

             // if the student record returned is correct 
             if (selection == 'y' || selection == 'Y')
             {
                 //process = true;
                 while (process)
                 {   // ask to confirm the removal of the student record
                     Console.Write($"\n{stu}\n++++++++++ CONFIRM to DELETE this student record ++++++++++" +
                                     "\n[Y]es [N]o [E]xit : ");
                     char confirmation = GetUserInputChar();
                     // user confirms deletion
                     if (confirmation == 'y' || confirmation == 'Y')
                     {
                         students.Remove(stu);
                         process = false;
                         exit = true;

                         Console.WriteLine("\n\nStudent Record has been deleted.\n\n++++++++++     UPDATING RECORDS      ++++++++++");
                         PrintAllRecords();
                         Console.WriteLine("\n++++++++++  Returning to the main menu    ++++++++++++");
                         break;

                     }
                     // deny confirmation of removal 
                     if (confirmation == 'n' || confirmation == 'N')
                     {
                         Console.WriteLine("\nCancelling selection...");

                         Console.WriteLine("\n++++++++++  Returning to the main menu    ++++++++++++");
                         process = false;
                         exit = true;
                         break;
                     }
                     // exit the removal method 
                     if (confirmation == 'e' || confirmation == 'E')
                     {
                         Console.WriteLine("++++++++++  Returning to the main menu    ++++++++++++");
                         process = false;
                         exit = true;
                         break;
                     }
                     else
                     {
                         Console.Write(" - Please choose the correct input choice");
                     }

                 }
                 // if the returned student record is not the correct student record
                 if (selection == 'n' || selection == 'N')
                 {
                     Console.WriteLine("\nPlease try again ");
                     exit = true;
                 }
                 // exit the removal method
                 if (selection == 'e' || selection == 'E')
                 {
                     Console.WriteLine("\n++++++++++  Returning to the main menu(1)    ++++++++++++");
                     exit = true;
                 }

             }
         }
     }
 }// end of removal method 


        //Input PARAMS are list of chars and compares them to a keySelection.
        //Will loop and ask the user till they get the correct one that is in charList
        //After 10 trys will return false
        //The return false can be used to say exit menu. 
        //Will out the Key selecion that matches the CharList.
        //EXAMPLE of use:
        /*char[] tryList = { 't', 'T', 'e', 'E' };
        if (KeyListCheck(out char selectKey, tryList) == false)
        {
            //if bad trys are greater then 10.
            exit = true;
            break;
        }
        if (selectKey == 'T' || selectKey == 't')
        */
        private bool KeyListCheck(out char keySelection, char[] charList)
        {
            keySelection = GetUserInputChar();

            int countBadTrys = 0;
            while (true)
            {
                
                //if selectedKey is in CharList[,] then reply true.
                for (int i = 0; i < charList.Length; i++)
                {
                    if (keySelection == charList[i])
                        
                        return true;      
                }
                if (countBadTrys == 9)
                {
                    //if bad trys are greater then 10 then it wll exit modify.
                    break;
                }
                Console.Write("\nPlease enter from the selection: ");
                keySelection = GetUserInputChar();
                countBadTrys++;
            }
            return false;
        }

        // MODIFY a student through a series of prompts 
        // ModifyStudent() will first find the student object that will be stu.
        // Depending on stu's class type either undergrad or grad student there 
        //  are Two paths to take, either update a grad stu or undergrad stu.
        // Both of these make a copy before there is edit.
        // Once the edit is made, there is a confirm that the user must acknowledge.
        // Otherwise stu will be reverted back so there is no change.
        private void ModifyStudent()
        {
            //will keep looping till exit 
            bool exit = false;
            while (exit == false)
            {
                Student stu = null;
                //finds student by email or beginning of email (NETID)
                while (true)
                {
                    stu = FindStudentRecord1();
                    if (stu == null)
                    {
                        //If not found will give option to leave or try again.
                        Console.WriteLine("\nStudent is NOT found.");
                        Console.Write(@"
[T]ry another
[E]xit

Please enter from the selection: ");
                        char[] tryList = { 't', 'T', 'e', 'E' };
                        //takes in selected and returns true or false if keys are in list.
                        if (KeyListCheck(out char select1, tryList) == false)
                        {
                            //if bad trys are greater then 10 then it wll exit modify.
                            exit = true;
                            break;
                        }
                        if (select1 == 'T' || select1 == 't')
                        {
                            //do nothing will go back in the loop
                        }
                        if (select1 == 'e' || select1 == 'E')
                        {
                            exit = true;
                            break;
                        }
                        
                    }
                    else if (stu != null)
                    {
                        Console.WriteLine("\nIs this the student you're looking for? ");
                        Console.Write(@"
[Y]es
[N]o 
[E]xit

Please enter from the selection: ");
                        char[] findRecList = { 'y', 'Y', 'e', 'E', 'N','n' };
                        //takes in selected and returns true or false if keys are in list.
                        if (KeyListCheck(out char select1, findRecList) == false)
                        {
                            //if bad trys are greater then 10 then it wll exit modify.
                            exit = true;
                            break;
                        }
                        if (select1 == 'y' || select1 == 'Y')
                        {
                            break;
                        }
                        if (select1 == 'e' || select1 == 'E')
                        {
                            exit = true;
                            break;
                        }
                    }
                }
                if (exit)
                { break; }
                Console.WriteLine("\n\nDo you want to continue modifying this student?");
                /*Console.WriteLine(stu.ToString());*/


                //if no then it will rinse and repeat.
                Console.Write("[Y]es or [N]o: ");

                char[] charList = { 'y', 'Y', 'n', 'N' };
                //takes in selected and returns true or false if keys are in list.
                KeyListCheck(out char selection, charList);

                    if (selection == 'y' || selection == 'Y')
                {
                    //Gets type of student 
                    if (stu is Undergrad)
                    {

                        Undergrad undergrad = (Undergrad)stu; // Cast stu to UnderGrad


                        // will revert back to the origianl name if user does not confirm.
                        string FNRevertBack = undergrad.FirstName;
                        string LNRevertBack = undergrad.LastName;
                        string EMRevertBack = undergrad.EmailAddress;
                        double GPARevertBack = undergrad.GradePtAvg;
                        YearRank YRRevertBack = undergrad.Rank;
                        string DMRevertBack = undergrad.DegreeMajor;
                        
                        Console.Write($@"
What part of the student record would
you like to modify?
[F]irst Name      {undergrad.FirstName}
[L]ast Name       {undergrad.LastName}
[E]mail address   {undergrad.EmailAddress} 
[G]pa             {undergrad.GradePtAvg}
[Y]ear in school  {undergrad.Rank}
[M]ajor           {undergrad.DegreeMajor}
[D]one

Please Make a selection: ");
                        char [] underGradMenu = { 'f', 'F', 'l', 'L', 'e', 'E', 'g', 'G', 'Y', 'y', 'M', 'm', 'd', 'D' };
                        //takes in selected and returns true or false if keys are in list.
                        KeyListCheck(out char selectMod, underGradMenu);
                        Console.WriteLine("");


                        switch (selectMod)
                        {
                            case 'F':
                            case 'f':

                                Console.Write("\nPlease enter new first name: ");

                                Console.Write("Please enter new first name: ");

                                undergrad.FirstName = Console.ReadLine();
                                break;
                            case 'L':
                            case 'l':
                                Console.Write("\nPlease enter new Last name: ");

                                Console.Write("Please enter new Last name: ");
                                undergrad.LastName = Console.ReadLine();
                                break;
                            case 'E':
                            case 'e':

                                Console.Write("\nPlease enter new Email: ");
                                undergrad.EmailAddress = Console.ReadLine();
                                break;
                            case 'G':
                            case 'g':

                                Console.Write("\nPlease enter new GPA: ");

                                undergrad.GradePtAvg = double.Parse(Console.ReadLine());
                                break;
                            case 'Y':
                            case 'y':

                                Console.Write("\n[1]Freshman, [2]Sophomore, [3]Junior, [4]Senior ");
                                Console.Write("Enter the year in school for this student:");
                                undergrad.Rank = (YearRank)int.Parse(Console.ReadLine());
                                break;
                            case 'M':
                            case 'm':

                                Console.Write("\nPlease enter new Major: ");
                                undergrad.DegreeMajor = Console.ReadLine();
                                break;
                            case 'D':
                            case 'd':
                                exit = true;
                                break;

                        }

                        Console.WriteLine("++++++++++Modifying Student Record+++++++++++");

                        Console.WriteLine($"\n{undergrad}");
                        Console.WriteLine("Are you sure you want to make this change?");
                        Console.Write("Confirm [Y]es or [N]o: ");

                        //are you sure you want to make this change?
                        //yes or no
                        
                        //The list is charList = { 'y', 'Y', 'n', 'N' };
                        //takes in selected and returns true or false if keys are in list.
                        KeyListCheck(out char confirmChange, charList);
                        if (confirmChange == 'y' || confirmChange == 'Y')
                        {
                            Console.WriteLine("\nYour change has been made.");
                        }

                        //no will revert back to the original
                        if (confirmChange == 'n' || confirmChange == 'N')
                        {
                            Console.WriteLine("\nNo change has been made.");
                            switch (selectMod)
                            {
                                case 'F':
                                case 'f':
                                    undergrad.FirstName = FNRevertBack;

                                    break;
                                case 'L':
                                case 'l':
                                    undergrad.LastName = LNRevertBack;
                                    break;
                                case 'E':
                                case 'e':
                                    undergrad.EmailAddress = EMRevertBack;
                                    break;
                                case 'G':
                                case 'g':
                                    undergrad.GradePtAvg = GPARevertBack;
                                    break;
                                case 'Y':
                                case 'y':
                                    undergrad.Rank = YRRevertBack;
                                    break;
                                case 'M':
                                case 'm':
                                    undergrad.DegreeMajor = DMRevertBack;

                                    break;
                            }
                            Console.WriteLine($"{undergrad}");
                        }
                    }

                    if (stu is GradStudent)
                    {
                        GradStudent gradStu = (GradStudent)stu; // Cast stu to GradStudent

                        // will revert back to the origianl name if user does not confirm.
                        string FNRevertBack = gradStu.FirstName;
                        string LNRevertBack = gradStu.LastName;
                        string EMRevertBack = gradStu.EmailAddress;
                        double GPARevertBack = gradStu.GradePtAvg;
                        decimal TCRevertBack = gradStu.TuitionCredit;
                        string AVRevertBack = gradStu.FacultyAdvisor;
                        Console.Write($@"

What part of the student account would
you like to modify?
[F]irst Name      {gradStu.FirstName}
[L]ast Name       {gradStu.LastName}
[E]mail address   {gradStu.EmailAddress} 
[G]pa             {gradStu.GradePtAvg}
[T]uition Credit  {gradStu.TuitionCredit}
[A]dvisor         {gradStu.FacultyAdvisor} 
[D]one

Please enter from the selection: ");
                        char[] gradMenu = { 'f', 'F', 'l', 'L', 'e', 'E', 'g', 'G', 'A', 'a', 'T', 't', 'd', 'D' };
                        //takes in selected and returns true or false if keys are in list.
                        KeyListCheck(out char selectMod, gradMenu);
                        Console.WriteLine("");
                        switch (selectMod)
                        {
                            case 'F':
                            case 'f':
                                Console.Write("Please enter new first name: ");
                                gradStu.FirstName = Console.ReadLine();
                                break;
                            case 'L':
                            case 'l':
                                Console.Write("Please enter new Last name: ");
                                gradStu.LastName = Console.ReadLine();
                                break;
                            case 'E':
                            case 'e':
                                Console.Write("Please enter new Email: ");
                                gradStu.EmailAddress = Console.ReadLine();
                                break;
                            case 'G':
                            case 'g':
                                Console.Write("Please enter new GPA: ");
                                gradStu.GradePtAvg = double.Parse(Console.ReadLine());
                                break;
                            case 'T':
                            case 't':
                                Console.Write("Please enter new Tuition Credit(no commas): $");
                                gradStu.TuitionCredit = decimal.Parse(Console.ReadLine());
                                break;
                                
                            case 'A':
                            case 'a':
                                Console.Write("Please enter new Facilty Advisor: ");
                                gradStu.FacultyAdvisor = Console.ReadLine();
                                break;
                            case 'D':
                            case 'd':
                                exit = true;
                                break;

                        }
                        if (exit)
                        { break; }
                        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");

                        Console.WriteLine($"/n{gradStu}");
                        Console.WriteLine("Are you sure you want to make this change?");
                        Console.Write("\nConfirm [Y]es or [N]o: ");
                        //are you sure you want to make this change?
                        //yes or no
                        KeyListCheck(out char confirmChange, charList);
                        if (confirmChange == 'y' || confirmChange == 'Y')
                        {
                            Console.WriteLine("\nYour change has been made.");
                        }
                        //no will revert back to the original
                        if (confirmChange == 'n' || confirmChange == 'N')
                        {
                            Console.WriteLine("\nNo change has been made.");
                            switch (selectMod)
                            {
                                case 'F':
                                case 'f':
                                    gradStu.FirstName = FNRevertBack;

                                    break;
                                case 'L':
                                case 'l':
                                    gradStu.LastName = LNRevertBack;
                                    break;
                                case 'E':
                                case 'e':
                                    gradStu.EmailAddress = EMRevertBack;
                                    break;
                                case 'G':
                                case 'g':
                                    gradStu.GradePtAvg = GPARevertBack;
                                    break;
                                case 'T':
                                case 't':
                                    gradStu.TuitionCredit = TCRevertBack;
                                    break;
                                case 'A':
                                case 'a':
                                    gradStu.FacultyAdvisor = AVRevertBack;

                                    break;
                            }
                        }
                        //print new version if stu with reverted or updated
                        Console.WriteLine($"{gradStu}");
                    }
                }
                //exits modify
                exit = true;
            }
        }//end MODIFY method




        //this is another way to find a student by email using the Contains() method. 
             //MODIFY method uses this.

        private Student FindStudentRecord1()
        {

            Console.Write("\nEnter the beginning of Email address ( NET ID ) to search for: ");
            string email = Console.ReadLine();

            /*Console.WriteLine(students.Exists(y => y.EmailAddress == email));*/

            Console.WriteLine(students.Find(x => x.EmailAddress.Contains(email)));

            Student stu = students.Find(x => x.EmailAddress.Contains(email));
            return stu;

        }
             
        // This method checks and displays confirmation status of if a student record exists already, using email address as a key
        private Student FindStudentRecord(out string email)
        {
            Console.Write("\nEnter the primary key (email address) to search for: ");
            email = Console.ReadLine();

            //iterate through students, looks for email
            foreach (Student stu in students)
            {
                if (email == stu.EmailAddress)
                {
                    Console.WriteLine($"\nFOUND record for: {stu.EmailAddress}\n");
                    return stu;
                }
            }

            //when at this point, did not find email, print this
            Console.WriteLine($"{email} NOT FOUND.");
            return null;
        }

        /*private void DeleteStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            //if key is valid, will ask for confirmation and delete or not
            if (stu != null)
            {
                //key is found, ask for confirmation
                Console.WriteLine($"\nAre you sure you want to DELETE this record?:\n [Y]es [N]o");
                if (GetUserInputChar() == 'y')
                {
                    students.Remove(stu);
                    Console.WriteLine($"\nRecord deleted. Returning to Main Menu.");
                }
                else
                {
                    Console.WriteLine("\nDeletion cancelled. Returning to Main Menu.");
                }

            }
            else { 
            //if here, student record was not found
            Console.WriteLine("\nStudent record not found. Returning to Main Menu.");
            }
        } //end of DELETE RECORD method
        */

        // This method prints all email addresses for students that exist in the database, to the console.
        private void PrintAllRecordPrimaryKeys()
            {
                Console.WriteLine("\n\n++++++++++Listing All Student Emails++++++++++++");
                //for each loop to iterate through student objects and print their data
                foreach (Student stu in students)
                {
                    Console.WriteLine(stu.EmailAddress);
                }
                Console.WriteLine("++++++++++Done Listing All Student Emails++++++++++++");
            }

        
             // This method prints all student records in a labeled and formatted list, with line seperations between students
            private void PrintAllRecords()
            {

                Console.WriteLine("\n++++++++++Listing All Student Records++++++++++++");
                //for each loop to iterate through student objects and print their data
                foreach (Student stu in students)
                {
                    Console.WriteLine(stu);
                }
                Console.WriteLine("\n++++++++++Done Listing All Student Records++++++++++++");
            }


            // This method allows us to get input as char from key press without having user press enter
            private char GetUserInputChar()
            {
                ConsoleKeyInfo key = Console.ReadKey();
                return key.KeyChar;
            }

            // This method displays standard database operation options to user
            private void DisplayMainMenu()
            {
                Console.Write(@"
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%% Student Database App %%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

[A]dd student record
[F]ind student record
[M]odify student record
[D]elete student record
[P]rint all records 
Print [K]eys (email addresses) only
[S]ave data to file and exit
[E]xit without saving

Choose selection: ");
            }

        //constants for reading and writing student data to files
        private const string StudentOutputFile = "STUDENT_INPUT_FILE.txt";
        private const string StudentInputFile = "STUDENT_INPUT_FILE.txt";

        // this method prints all student data to the specified output file, each token on its own line
        public void WriteDataToOutputFile() 
        {
            //direct output of the data to the file
            StreamWriter outFile = new StreamWriter(StudentOutputFile);

            // notify user that writing is taking place
            Console.WriteLine("Saving student data to output file...");
            
            //iterate through student objects and print their data
            foreach (Student stu in students)
            {
                Console.WriteLine(stu.ToString());
                outFile.WriteLine(stu.ToStringForOutputFile());
                
            }

            //close the file to release the resource
            outFile.Close();
            Console.WriteLine("Done writing data to output file. File has been closed.");

        }

    }
}
