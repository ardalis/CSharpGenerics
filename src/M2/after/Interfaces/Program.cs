using System;
using System.Collections.Generic;
using System.Linq;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var studentService = new StudentPrinterService(new StudentRepository());
            studentService.PrintStudents(5);

            Console.WriteLine();

            // var authorService = new AuthorPrinterService(new AuthorRepository());
            // authorService.PrintAuthors();

            Console.WriteLine($"Total Students Created: {Student.StudentCount}");

        }
    }

    public class StudentPrinterService
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentPrinterService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void PrintStudents(int max = 100)
        {
            var students = _studentRepository.List()
                .Take(max)
            .ToArray();

            // sort
            //Array.Sort(students);

            // Console.WriteLine("Students:");
            // for (int i = 0; i < students.Length; i++)
            // {
            //     Console.WriteLine(students[i]);
            // }
            PrintStudentsToConsole(students);
        }

        private void PrintStudentsToConsole(IEnumerable<Student> students)
        {
            Console.WriteLine("Students:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
    }

    public interface IRepository<T>
    {
        IEnumerable<T> List();
    }

    public record Name(string First, string Last);

    public class StudentRepository : IRepository<Student>
    {
        private Name[] _names = new Name[10];

        public StudentRepository()
        {
            _names[0] = new("Steve", "Smith");
            _names[1] = new("Chad", "Smith");
            _names[2] = new("Ben", "Smith");
            _names[3] = new("Eric", "Smith");
            _names[4] = new("Julie", "Lerman");
            _names[5] = new("David", "Starr");
            _names[6] = new("Aaron", "Skonnard");
            _names[7] = new("Aaron", "Stewart");
            _names[8] = new("Aaron", "Powell");
            _names[9] = new("Aaron", "Frost");
        }

        public IEnumerable<Student> List()
        {
            int index = 0;
            while (index < _names.Length)
            {
                yield return new Student(_names[index].First, _names[index].Last);
                index++;
            }
        }
    }

    public class Student : IComparable<Student>
    {
        public static int StudentCount = 0;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentCount++;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public int CompareTo(Student other)
        {
            if (other is null) return 1;
            if (other.LastName == this.LastName)
            {
                return this.FirstName.CompareTo(other.FirstName);
            }
            return this.LastName.CompareTo(other.LastName);
        }
    }

    public class AuthorPrinterService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorPrinterService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public void PrintAuthors()
        {
            var authors = _authorRepository.List().ToArray();

            // sort
            Array.Sort(authors);

            Console.WriteLine("Authors:");
            for (int i = 0; i < authors.Length; i++)
            {
                Console.WriteLine(authors[i]);
            }
        }
    }

    public class AuthorRepository : IRepository<Author>
    {
        public IEnumerable<Author> List()
        {
            // create a bunch of authors
            var authors = new Author[10];
            authors[0] = new Author("Steve", "Smith");
            authors[1] = new Author("Chad", "Smith");
            authors[2] = new Author("Ben", "Smith");
            authors[3] = new Author("Eric", "Smith");
            authors[4] = new Author("Julie", "Lerman");
            authors[5] = new Author("David", "Starr");
            authors[6] = new Author("Aaron", "Skonnard");
            authors[7] = new Author("Aaron", "Stewart");
            authors[8] = new Author("Aaron", "Powell");
            authors[9] = new Author("Aaron", "Frost");
            return authors;
        }
    }

    public class Author : IComparable<Author>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Author(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public int CompareTo(object obj)
        {
            if (obj is null) return 1;

            if (obj is Author otherAuthor)
            {
                return this.ToString().CompareTo(otherAuthor.ToString());
            }
            throw new ArgumentException("Not an Author", nameof(obj));
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public int CompareTo(Author other)
        {
            if (other is null) return 1;

            return this.ToString().CompareTo(other.ToString());
        }
    }
}
