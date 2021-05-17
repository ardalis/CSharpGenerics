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
            waitlist.Add(Guid.NewGuid());
            waitlist.Add("student2@test.test");
            waitlist.Add(Guid.NewGuid());
            waitlist.Add("student3@test.test");

            // course has open seats
            bool courseIsFull = false;

            // process waitlist
            while (waitlist.Length > 0 && !courseIsFull)
            {
                object? item = waitlist.GetNextItem();
                if(item is string studentEmail)
                {
                    Console.WriteLine($"Emailing {studentEmail} to let them know about opening.");
                }
                if(item is Guid registrationCode)
                {
                    Console.WriteLine($"Registering student with code {registrationCode}.");
                }
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

    // later to support Guid reservation codes
    public class ReservationWaitlist
    {
        private Queue _queue = new Queue();
        public int Length => _queue.Count;

        public void Add(Guid item)
        {
            _queue.Enqueue(item);
        }

        public Guid? GetNextItem()
        {
            return (Guid?)_queue.Dequeue();
        }
    }

    // but now the original program needs 2 queues one for emails, one for reservations
    // but there is only *one* waitlist
    // new logic: given an opening, process next waitlist item. If it's a notification, wait 15 minutes before processing the next waitlist item for this opening.

    public class WaitlistItem
    {
        public string StudentEmail { get; set; }
    }

    public class ReservationWaitlistItem : WaitlistItem
    {
        public Guid ReservationCode { get; set; }
    }

}
