using System;

namespace LibraryManagementSystem.Models
{
    public class StudentMember : Member
    {
        public StudentMember(
            int id,
            string name,
            string phone,
            string studentId,
            int year)
            : base(id, name, phone)
        {
            if (string.IsNullOrWhiteSpace(studentId))
            {
                throw new ArgumentException("Student ID cannot be empty.");
            }

            if (year <= 0)
            {
                throw new ArgumentException("Year must be positive.");
            }

            StudentId = studentId.Trim();
            Year = year;
        }

        public string StudentId { get; }

        public int Year { get; }

        public override void DisplayInfo()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("STUDENT MEMBER");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Member ID   : {Id}");
            Console.WriteLine($"Name        : {Name}");
            Console.WriteLine($"Phone       : {Phone}");
            Console.WriteLine($"Student ID  : {StudentId}");
            Console.WriteLine($"Year        : {Year}");
            Console.WriteLine("Member Type : Student");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}