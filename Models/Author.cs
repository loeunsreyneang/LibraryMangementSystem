using System;

namespace LibraryManagementSystem.Models
{
    public class Author
    {
        private string _name;
        private string _biography;

        public Author(int authorId, string name, string biography)
        {
            if (authorId <= 0)
            {
                throw new ArgumentException("Author ID must be positive.");
            }

            AuthorId = authorId;
            Name = name;
            Biography = biography;
        }

        public int AuthorId { get; }

        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Author name cannot be empty.");
                }

                _name = value.Trim();
            }
        }

        public string Biography
        {
            get => _biography;

            set
            {
                _biography = value?.Trim() ?? string.Empty;
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Author ID : {AuthorId}");
            Console.WriteLine($"Name      : {Name}");
            Console.WriteLine($"Biography : {Biography}");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}