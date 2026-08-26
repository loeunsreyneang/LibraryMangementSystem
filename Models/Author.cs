using System;

namespace LibraryManagementSystem.Models
{
    public class Author : Person
    {
        private string _biography;

        public Author(int authorId, string name, string biography)
            : base(name)
        {
            if (authorId <= 0)
            {
                throw new ArgumentException("Author ID must be positive.");
            }

            AuthorId = authorId;
            Biography = biography;
        }

        public int AuthorId { get; }

        public string Biography
        {
            get => _biography;

            set
            {
                _biography = value?.Trim() ?? string.Empty;
            }
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Author ID : {AuthorId}");
            Console.WriteLine($"Name      : {Name}");
            Console.WriteLine($"Biography : {Biography}");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}