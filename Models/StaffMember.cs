using System;

namespace LibraryManagementSystem.Models
{
    public class StaffMember : Member
    {
        public StaffMember(
            int id,
            string name,
            string phone,
            string staffId,
            string position)
            : base(id, name, phone)
        {
            if (string.IsNullOrWhiteSpace(staffId))
            {
                throw new ArgumentException("Staff ID cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(position))
            {
                throw new ArgumentException("Position cannot be empty.");
            }

            StaffId = staffId.Trim();
            Position = position.Trim();
        }

        public string StaffId { get; }

        public string Position { get; }

        public override void DisplayInfo()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("STAFF MEMBER");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Member ID   : {Id}");
            Console.WriteLine($"Name        : {Name}");
            Console.WriteLine($"Phone       : {Phone}");
            Console.WriteLine($"Staff ID    : {StaffId}");
            Console.WriteLine($"Position    : {Position}");
            Console.WriteLine("Member Type : Staff");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}