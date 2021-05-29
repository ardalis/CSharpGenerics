using System.Collections.Generic;

namespace Complexity
{
    class Category {}
    class Author {}
    class Course {}

    class Sample
    {
        static void Run()
        {
            var catalog = new Dictionary<Category,Dictionary<Author,List<Course>>>();
            var courseCatalog = new CourseCatalog();
        }
    }

    class CourseCatalog : Dictionary<Category,Dictionary<Author,List<Course>>>
    {
        // custom business logic here
    }
}