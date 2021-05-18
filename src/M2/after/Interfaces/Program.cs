using System;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // create a bunch of students
            var students = new Student[10];
            students[0] = new Student("Steve", "Smith");
            students[1] = new Student("Chad", "Smith");
            students[2] = new Student("Ben", "Smith");
            students[3] = new Student("Eric", "Smith");
            students[4] = new Student("Julie", "Lerman");
            students[5] = new Student("David", "Starr");
            students[6] = new Student("Aaron", "Skonnard");
            students[7] = new Student("Aaron", "Stewart");
            students[8] = new Student("Aaron", "Powell");
            students[9] = new Student("Aaron", "Frost");

            // sort
            Array.Sort(students);

            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine(students[i]); // runtime error if no IComparable
            }
        }
    }

    public class Student : IComparable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public int CompareTo(object obj)
        {
            if (obj is null) return 1;

            if(obj is Student otherStudent)
            {
                if(otherStudent.LastName == this.LastName)
                {
                    return this.FirstName.CompareTo(otherStudent.FirstName);
                }
                return this.LastName.CompareTo(otherStudent.LastName);
            }
            throw new ArgumentException("Not a Student", nameof(obj));
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
