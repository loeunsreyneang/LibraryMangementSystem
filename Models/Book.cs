using System;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        private string _title;

        public Book(int id, string title, Author author, int year)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Book ID must be positive.");
            }

            Id = id;
            Title = title;
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Year = year;
            IsAvailable = true;
        }

        public int Id { get; }

        public string Title
        {
            get => _title;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Book title cannot be empty.");
                }

                _title = value.Trim();
            }
        }

        public Author Author { get; private set; }

        public int AuthorId => Author.AuthorId;

        public int Year { get; private set; }

        public bool IsAvailable { get; private set; }

        public void Update(string title, Author author, int year)
        {
            Title = title;
            Author = author ?? throw new ArgumentNullException(nameof(author));
            Year = year;
        }

        public void Borrow()
        {
            IsAvailable = false;
        }

        public void Return()
        {
            IsAvailable = true;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine($"Book ID     : {Id}");
            Console.WriteLine($"Title       : {Title}");
            Console.WriteLine($"Author ID   : {Author.AuthorId}");
            Console.WriteLine($"Author      : {Author.Name}");
            Console.WriteLine($"Year        : {Year}");
            Console.WriteLine(
                $"Status      : {(IsAvailable ? "Available" : "Borrowed")}");
            Console.WriteLine("--------------------------------------------------");
        }
    }
}