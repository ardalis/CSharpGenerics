using System;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var studentService = new StudentPrinterService(new StudentRepository());
            studentService.PrintStudents();

            Console.WriteLine();

            var authorService = new AuthorPrinterService(new AuthorRepository());
            authorService.PrintAuthors();
        }
    }

    public class StudentPrinterService
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentPrinterService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void PrintStudents()
        {
            var students = _studentRepository.List();

            // sort
            Array.Sort(students);

            Console.WriteLine("Students:");
            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine(students[i]);
            }
        }
    }




    // public interface IStudentRepository
    // {
    //     Student[] List();
    // }
    // public interface IAuthorRepository
    // {
    //     Author[] List();
    // }

    public interface IRepository<T>
    {
        T[] List();
    }





    public class StudentRepository : IRepository<Student>
    {
        public Student[] List()
        {
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
            return students;
        }
    }

    public class Student : IComparable<Student>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
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
            var authors = _authorRepository.List();

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
        public Author[] List()
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
