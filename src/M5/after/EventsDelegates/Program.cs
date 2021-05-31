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

            //Delegates.DelegateRunner.Execute();
            BuiltinDelegates.DelegateRunner.Execute();
            return;

            // int count = 0;
            // var authorRepo = new Repository<Author>();
            // authorRepo.EntityAdded += Repo_EntityAdded;
            // authorRepo.EntityAdded += (o,e) => count++;
            // authorRepo.Add(new Author("Steve", "Smith"));
            // Console.WriteLine($"{count} authors added.");

            // use data loaders
            var authorRepo = new Repository<Author>();
            var courseRepo = new Repository<Course>();

            var authorLoader = new DataLoader<Author>(authorRepo);
            var courseLoader = new DataLoader<Course>(courseRepo);

            authorLoader.Load(Authors());
            Console.WriteLine($"Loaded {authorLoader.Counter} authors.");
            courseLoader.Load(Courses());
            Console.WriteLine($"Loaded {courseLoader.Counter} courses.");

            // print them all
            Console.WriteLine("Listing all authors:");
            var authors = authorRepo.List();
            foreach (var author in authors)
            {
                Console.WriteLine(author);
            }
            Console.WriteLine("Listing all courses:");
            var courses = courseRepo.List();
            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }

        }
        private static void Repo_EntityAdded(object sender, EntityAddedEventArgs<Author> args)
        {
            Console.WriteLine($"Author added: {args.EntityAdded} (via Program.cs) by {sender}");
        }












        static void Main2(string[] args)
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
