using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassesMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generic Events and Delegates");

            LoadData(new[] { typeof(Author), typeof(Course) });

            var authorRepo = new Repository<Author>();
            var courseRepo = new Repository<Course>();
            var dataloader = new DataLoader<Author>(authorRepo);
            dataloader.Load(Authors());

            //repo.EntityAdded += Repo_EntityAdded;
            //repo.Add(author);

            Console.WriteLine($"{dataloader.Counter} record(s) added.");

            var authors = authorRepo.List();
            var courses = courseRepo.List();

            foreach (var author in authors)
            {
                Console.WriteLine(author);
            }
            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }
        }

        private static void LoadData(Type[] types)
        {
            var loaderType = typeof(DataLoader<>);
            var repoType = typeof(Repository<>); // a container would use an interface here
            foreach (Type type in types)
            {
                var repoConstructed = repoType.MakeGenericType(new[] { type });
                var repoInstance = Activator.CreateInstance(repoConstructed);

                var loaderConstructed = loaderType.MakeGenericType(new[] { type });
                dynamic loaderInstance = Activator.CreateInstance(loaderConstructed, repoInstance);

                //var loadMethod = loaderConstructed.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).FirstOrDefault(method => method.Name == "Load");

                // could also use reflection for this
                if (type.Name == "Author")
                {
                    loaderInstance.Load(Authors());
                }
                else
                {
                    loaderInstance.Load(Courses());
                }

                Console.WriteLine($"Loaded {loaderInstance.Counter} {type.Name} records.");
            }
        }

        private static void Repo_EntityAdded(object sender, Author e)
        {
            Console.WriteLine($"Author added: {e} (via Program.cs)");
        }

        public static IEnumerable<Author> Authors()
        {
            yield return new Author("Steve", "Smith");
        }
        public static IEnumerable<Course> Courses()
        {
            yield return new Course("Domain-Driven Design Fundamentals");
            yield return new Course("Kanban: Getting Started");
            yield return new Course("C# Design Patterns: Rules Engine Pattern");
            yield return new Course("C# Design patterns: Memento");
            yield return new Course("C# Design Patterns: Template Method");
            yield return new Course("Design Patterns Overview");
            yield return new Course("C# Design Patterns: Singleton");
            yield return new Course("C# Design Patterns: Proxy");
            yield return new Course("C# Design Patterns: Adapter");
            yield return new Course("Refactoring for C# Developers");
            yield return new Course("SOLID Principles for C# Developers");
        }
    }

    public record Author(string FirstName, string LastName);
    public record Course(string CourseName);
}
