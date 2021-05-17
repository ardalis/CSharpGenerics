using System;
using System.Collections;

namespace CourseRegistrar
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Course Registration System");
            Console.WriteLine("--------------------------");

            // add student email to waitlist
            var waitlist = new Waitlist();

            Console.WriteLine("Adding students to waitlist...");

            waitlist.Add("student1@test.test");
            waitlist.Add("student2@test.test");
            waitlist.Add("student3@test.test");

            // course has open seats
            bool courseIsFull = false;

            // process waitlist
            while (waitlist.Length > 0 && !courseIsFull)
            {
                string? studentEmail = (string?)waitlist.GetNextItem();
                Console.WriteLine($"Emailing {studentEmail} to let them know about opening.");
            }

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }
    }

    public class Waitlist
    {
        private Queue _queue = new Queue();
        public int Length => _queue.Count;

        public void Add(object item)
        {
            _queue.Enqueue(item);
        }

        public object? GetNextItem()
        {
            return _queue.Dequeue();
        }
    }

    // later to gain type safety
    public class EmailWaitlist
    {
        private Queue _queue = new Queue();
        public int Length => _queue.Count;

        public void Add(string item)
        {
            _queue.Enqueue(item);
        }

        public string? GetNextItem()
        {
            return (string?)_queue.Dequeue();
        }
    }
}
