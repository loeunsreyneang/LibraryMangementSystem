using System;

namespace LibraryManagementSystem.Models
{
    public class TeacherMember : Member
    {
        public TeacherMember(
            int id,
            string name,
            string phone,
            string teacherId,
            string department)
            : base(id, name, phone)
        {
            if (string.IsNullOrWhiteSpace(teacherId))
            {
                throw new ArgumentException("Teacher ID cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(department))
            {
                throw new ArgumentException("Department cannot be empty.");
            }

            TeacherId = teacherId.Trim();
            Department = department.Trim();
        }

        public string TeacherId { get; }

        public string Department { get; }

        public override void DisplayInfo()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("TEACHER MEMBER");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Member ID   : {Id}");
            Console.WriteLine($"Name        : {Name}");
            Console.WriteLine($"Phone       : {Phone}");
            Console.WriteLine($"Teacher ID  : {TeacherId}");
            Console.WriteLine($"Department  : {Department}");
            Console.WriteLine("Member Type : Teacher");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}