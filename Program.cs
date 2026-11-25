
using System;
using System.Collections.Generic;

// Stores the personal information of each student.
class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public string Gender { get; set; }
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    public Student(string studentNumber, string surname, string firstName,
        string occupation, string gender, string countryCode,
        string areaCode, string phoneNumber)
    {
        StudentNumber = studentNumber;
        Surname = surname;
        FirstName = firstName;
        Occupation = occupation;
        Gender = gender;
        CountryCode = countryCode;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
    }
}

class Phonebook
{
    // List where all student records are stored.
    private List<Student> students = new List<Student>();

    public void AddStudent(Student student)
    {
        students.Add(student);
    }

    // Searches for a student using their student number.
    public Student? FindStudent(string studentNumber)
    {
        int index = 0;

        while (index < students.Count)
        {
            if (students[index].StudentNumber == studentNumber)
            {
                return students[index];
            }

            index++;
        }

        return null;
    }

    // Allows the user to update an existing student record.
    public void EditStudent(Student student)
    {
        string choice = "";

        while (choice != "8")
        {
            string pronoun = student.Gender.ToUpper() == "F" ? "Her" : "His";

            Console.WriteLine("\nHere is the existing information about " + student.StudentNumber + ":");
            Console.WriteLine(student.FirstName + " " + student.Surname + " is a " + student.Occupation + ". "
                + pronoun + " number is "
                + student.CountryCode + "-" + student.AreaCode + "-" + student.PhoneNumber);

            Console.WriteLine("\nWhich of the following information do you wish to change?");
            Console.WriteLine("[1] Student number [2] Surname [3] Gender [4] Occupation");
            Console.WriteLine("[5] Country code [6] Area code [7] Phone number");
            Console.WriteLine("[8] None - Go back to main menu");
            Console.Write("Enter choice: ");
            choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                Console.Write("Enter new student number: ");
                student.StudentNumber = Console.ReadLine() ?? "";
            }
            else if (choice == "2")
            {
                Console.Write("Enter new surname: ");
                student.Surname = Console.ReadLine() ?? "";
            }
            else if (choice == "3")
            {
                Console.Write("Enter new gender (M/F): ");
                student.Gender = Console.ReadLine() ?? "";
            }
            else if (choice == "4")
            {
                Console.Write("Enter new occupation: ");
                student.Occupation = Console.ReadLine() ?? "";
            }
            else if (choice == "5")
            {
                Console.Write("Enter new country code (60, 62, 63, 65, 66): ");
                string newCode = Console.ReadLine() ?? "";

                if (IsValidCountryCode(newCode))
                {
                    student.CountryCode = newCode;
                }
                else
                {
                    Console.WriteLine("Invalid country code.");
                }
            }
            else if (choice == "6")
            {
                Console.Write("Enter new area code: ");
                student.AreaCode = Console.ReadLine() ?? "";
            }
            else if (choice == "7")
            {
                Console.Write("Enter new phone number: ");
                student.PhoneNumber = Console.ReadLine() ?? "";
            }
            else if (choice != "8")
            {
                Console.WriteLine("Invalid choice.");
            }
        }
    }

    // Searches students by selected countries.
    public void SearchByCountry()
    {
        bool[] selectedCountries = new bool[5];
        bool allCountries = false;
        string choice = "";
        int choiceNumber = 1;

        Console.WriteLine("\nFrom which country:");
        Console.WriteLine("[1] Philippines [2] Thailand [3] Singapore [4] Indonesia");
        Console.WriteLine("[5] Malaysia [6] ALL [0] No More");

        while (choice != "0" && allCountries == false)
        {
            Console.Write("Enter choice " + choiceNumber + ": ");
            choice = Console.ReadLine() ?? "";
            choiceNumber++;

            if (choice == "1")
            {
                selectedCountries[0] = true;
            }
            else if (choice == "2")
            {
                selectedCountries[1] = true;
            }
            else if (choice == "3")
            {
                selectedCountries[2] = true;
            }
            else if (choice == "4")
            {
                selectedCountries[3] = true;
            }
            else if (choice == "5")
            {
                selectedCountries[4] = true;
            }
            else if (choice == "6")
            {
                allCountries = true;
            }
            else if (choice != "0")
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        List<Student> matchingStudents = new List<Student>();
        int index = 0;

        while (index < students.Count)
        {
            bool includeStudent = allCountries;

            if (students[index].CountryCode == "63" && selectedCountries[0])
            {
                includeStudent = true;
            }
            else if (students[index].CountryCode == "66" && selectedCountries[1])
            {
                includeStudent = true;
            }
            else if (students[index].CountryCode == "65" && selectedCountries[2])
            {
                includeStudent = true;
            }
            else if (students[index].CountryCode == "62" && selectedCountries[3])
            {
                includeStudent = true;
            }
            else if (students[index].CountryCode == "60" && selectedCountries[4])
            {
                includeStudent = true;
            }

            if (includeStudent)
            {
                matchingStudents.Add(students[index]);
            }

            index++;
        }

        // Basic bubble sort: alphabetically arrange students by surname.
        int outer = 0;

        while (outer < matchingStudents.Count - 1)
        {
            int inner = 0;

            while (inner < matchingStudents.Count - outer - 1)
            {
                if (string.Compare(matchingStudents[inner].Surname,
                    matchingStudents[inner + 1].Surname,
                    StringComparison.OrdinalIgnoreCase) > 0)
                {
                    Student temporary = matchingStudents[inner];
                    matchingStudents[inner] = matchingStudents[inner + 1];
                    matchingStudents[inner + 1] = temporary;
                }

                inner++;
            }

            outer++;
        }

        string[] countryNames =
        {
            "Philippines", "Thailand", "Singapore", "Indonesia", "Malaysia"
        };

        List<string> selectedNames = new List<string>();
        int countryIndex = 0;

        while (countryIndex < countryNames.Length)
        {
            if (selectedCountries[countryIndex])
            {
                selectedNames.Add(countryNames[countryIndex]);
            }

            countryIndex++;
        }

        if (allCountries)
        {
            Console.WriteLine("\nHere are the students from all countries:");
        }
        else if (selectedNames.Count == 1)
        {
            Console.WriteLine("\nHere are the students from " + selectedNames[0] + ":");
        }
        else if (selectedNames.Count > 1)
        {
            string countries = "";
            int nameIndex = 0;

            while (nameIndex < selectedNames.Count)
            {
                if (nameIndex == 0)
                {
                    countries = selectedNames[nameIndex];
                }
                else if (nameIndex == selectedNames.Count - 1)
                {
                    countries += " and " + selectedNames[nameIndex];
                }
                else
                {
                    countries += ", " + selectedNames[nameIndex];
                }

                nameIndex++;
            }

            Console.WriteLine("\nHere are the students from " + countries + ":");
        }
        else
        {
            Console.WriteLine("\nSearch results:");
        }

        if (matchingStudents.Count == 0)
        {
            Console.WriteLine("No students found.");
        }
        else
        {
            int printIndex = 0;

            while (printIndex < matchingStudents.Count)
            {
                Student current = matchingStudents[printIndex];
                string pronoun = current.Gender.ToUpper() == "F" ? "Her" : "His";

                Console.WriteLine(current.Surname + ", " + current.FirstName + ", with student number "
                    + current.StudentNumber + ", is a " + current.Occupation + ". "
                    + pronoun + " phone number is "
                    + current.CountryCode + "-" + current.AreaCode + "-" + current.PhoneNumber);

                printIndex++;
            }
        }
    }

    public bool IsValidCountryCode(string code)
    {
        if (code == "60" || code == "62" || code == "63" || code == "65" || code == "66")
        {
            return true;
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        Phonebook phonebook = new Phonebook();
        string choice = "";

        while (choice != "4")
        {
            Console.WriteLine("\nASEAN PHONEBOOK");
            Console.WriteLine("[1] Store to ASEAN phonebook");
            Console.WriteLine("[2] Edit entry in ASEAN phonebook");
            Console.WriteLine("[3] Search ASEAN phonebook by country");
            Console.WriteLine("[4] Exit");
            Console.Write("Enter choice: ");
            choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                string anotherEntry = "Y";

                while (anotherEntry == "Y" || anotherEntry == "y")
                {
                    Console.Write("Enter student number: ");
                    string studentNumber = Console.ReadLine() ?? "";

                    Console.Write("Enter surname: ");
                    string surname = Console.ReadLine() ?? "";

                    Console.Write("Enter first name: ");
                    string firstName = Console.ReadLine() ?? "";

                    Console.Write("Enter occupation: ");
                    string occupation = Console.ReadLine() ?? "";

                    Console.Write("Enter gender (M for male, F for female): ");
                    string gender = Console.ReadLine() ?? "";

                    Console.Write("Enter country code (60, 62, 63, 65, 66): ");
                    string countryCode = Console.ReadLine() ?? "";

                    while (phonebook.IsValidCountryCode(countryCode) == false)
                    {
                        Console.WriteLine("Invalid country code. Please enter 60, 62, 63, 65, or 66.");
                        Console.Write("Enter country code: ");
                        countryCode = Console.ReadLine() ?? "";
                    }

                    Console.Write("Enter area code: ");
                    string areaCode = Console.ReadLine() ?? "";

                    Console.Write("Enter number: ");
                    string phoneNumber = Console.ReadLine() ?? "";

                    Student student = new Student(studentNumber, surname, firstName,
                        occupation, gender, countryCode, areaCode, phoneNumber);

                    phonebook.AddStudent(student);

                    Console.Write("Do you want to enter another entry [Y/N]? ");
                    anotherEntry = Console.ReadLine() ?? "";
                }
            }
            else if (choice == "2")
            {
                Console.Write("Enter student number: ");
                string studentNumber = Console.ReadLine() ?? "";

                Student? student = phonebook.FindStudent(studentNumber);

                if (student == null)
                {
                    Console.WriteLine("Student number not found.");
                }
                else
                {
                    phonebook.EditStudent(student);
                }
            }
            else if (choice == "3")
            {
                phonebook.SearchByCountry();
            }
            else if (choice != "4")
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        Console.WriteLine("Program ended.");
    }
}
