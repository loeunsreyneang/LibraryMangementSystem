using System;

namespace LibraryManagementSystem.Models
{
    public class Member : Person
    {
        private string _phone;

        public Member(int id, string name, string phone)
            : base(name)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Member ID must be positive.");
            }

            Id = id;
            Phone = phone;
        }

        public int Id { get; }

        public string Phone
        {
            get => _phone;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Phone cannot be empty.");
                }

                _phone = value.Trim();
            }
        }

        public void UpdateContact(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("MEMBER");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Member ID   : {Id}");
            Console.WriteLine($"Name        : {Name}");
            Console.WriteLine($"Phone       : {Phone}");
            Console.WriteLine("Member Type : General Member");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}