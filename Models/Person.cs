using System;

namespace LibraryManagementSystem.Models
{
    public class Person
    {
        private string _name;

        public Person(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty.");
                }

                _name = value.Trim();
            }
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}");
        }
    }
}