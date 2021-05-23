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
        }
    }

    public interface IRepository<T> where T : IComparable<T>
    {
        IEnumerable<T> List();
        IEnumerable<T> SortedList();

    }

    public interface IPersonRepository<T> : IRepository<T> where T : Person, IComparable<T>, new()
    {
        IEnumerable<T> Search(string name);
        T Create(Name name);
        T CreateDefault();
    }

    public interface IPersonRepository
    {
        IEnumerable<T> Search<T>(string name) where T : Person;
        T Create<T>(Name name) where T : Person;
        T CreateDefault<T>() where T : Person, new();
    }

    public class StudentPrinterService
    {
        private readonly IPersonRepository<Student> _studentRepository;
        public StudentPrinterService(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void PrintStudents(int max = 100)
        {
            var students = _studentRepository.SortedList()
                .Take(max)
            .ToArray();

            PrintStudentsToConsole(students);

            var smiths = _studentRepository.Search("Smith");
            PrintSmithsToConsole(smiths);
        }

        private void PrintStudentsToConsole(IEnumerable<Student> students)
        {
            Console.WriteLine("Students:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
        private void PrintSmithsToConsole(IEnumerable<Student> students)
        {
            Console.WriteLine("Smiths:");
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }
    }

    public record Name(string First, string Last);

    public class StudentRepository : IPersonRepository<Student>
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

        public Student Create(Name name)
        {
            return new Student(name.First, name.Last);
        }

        public Student CreateDefault()
        {
            return new Student();
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

        public IEnumerable<Student> Search(string name)
        {
            return List().Where(student => student.FirstName.Contains(name) ||
            student.LastName.Contains(name));
        }

        public IEnumerable<Student> SortedList()
        {
            var students = List().ToList();
            students.Sort();
            return students;
        }
    }

    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class Student : Person, IComparable<Student>
    {
        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Student() : this("FirstName", "LastName")
        {
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

        public IEnumerable<Author> SortedList()
        {
            throw new NotImplementedException();
        }
    }

    public class Author : Person, IComparable<Author>
    {
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
