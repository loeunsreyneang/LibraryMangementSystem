using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public partial class Library
    {
        private readonly List<Book> _books = new();
        private readonly List<Author> _authors = new();
        private readonly List<Member> _members = new();
        private readonly List<Borrow> _borrows = new();

        private int _nextBorrowId = 1;

        // ==================================================
        // AUTHORS
        // ==================================================

        public bool AddAuthor(Author author)
        {
            if (author == null)
            {
                return false;
            }

            if (FindAuthorById(author.AuthorId) != null)
            {
                return false;
            }

            _authors.Add(author);
            return true;
        }

        public Author? FindAuthorById(int id)
        {
            return _authors.FirstOrDefault(author =>
                author.AuthorId == id);
        }

        public List<Author> GetAllAuthors()
        {
            return _authors.ToList();
        }

        public bool UpdateAuthor(
            int id,
            string name,
            string biography)
        {
            Author? author = FindAuthorById(id);

            if (author == null)
            {
                return false;
            }

            author.Name = name;
            author.Biography = biography;

            return true;
        }

        public bool DeleteAuthor(int id)
        {
            Author? author = FindAuthorById(id);

            if (author == null)
            {
                return false;
            }

            bool hasBooks = _books.Any(book =>
                book.AuthorId == id);

            if (hasBooks)
            {
                return false;
            }

            _authors.Remove(author);
            return true;
        }

        public List<Author> SearchAuthors(string keyword)
        {
            return _authors
                .Where(author =>
                    author.Name.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase) ||
                    author.Biography.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void DisplayAllAuthors()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("                  ALL AUTHORS");
            Console.WriteLine("==================================================");

            if (_authors.Count == 0)
            {
                Console.WriteLine("No authors available.");
                return;
            }

            foreach (Author author in _authors)
            {
                author.DisplayInfo();
            }
        }

        // ==================================================
        // BOOKS
        // ==================================================

        public bool AddBook(Book book)
        {
            if (book == null)
            {
                return false;
            }

            if (FindBookById(book.Id) != null)
            {
                return false;
            }

            if (FindAuthorById(book.AuthorId) == null)
            {
                return false;
            }

            _books.Add(book);
            return true;
        }

        public Book? FindBookById(int id)
        {
            return _books.FirstOrDefault(book =>
                book.Id == id);
        }

        public List<Book> GetAllBooks()
        {
            return _books.ToList();
        }

        public bool UpdateBook(
            int id,
            string title,
            Author author,
            int year)
        {
            Book? book = FindBookById(id);

            if (book == null)
            {
                return false;
            }

            if (FindAuthorById(author.AuthorId) == null)
            {
                return false;
            }

            book.Update(title, author, year);

            return true;
        }

        public bool DeleteBook(int id)
        {
            Book? book = FindBookById(id);

            if (book == null)
            {
                return false;
            }

            bool isBorrowed = _borrows.Any(borrow =>
                borrow.BookId == id &&
                !borrow.IsReturned);

            if (isBorrowed)
            {
                return false;
            }

            _books.Remove(book);
            return true;
        }

        public List<Book> SearchBooks(string keyword)
        {
            return _books
                .Where(book =>
                    book.Title.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase) ||
                    book.Author.Name.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void DisplayAllBooks()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("                   ALL BOOKS");
            Console.WriteLine("==================================================");

            if (_books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            foreach (Book book in _books)
            {
                book.DisplayInfo();
            }
        }

        // ==================================================
        // MEMBERS
        // ==================================================

        public bool AddMember(Member member)
        {
            if (member == null)
            {
                return false;
            }

            if (FindMemberById(member.Id) != null)
            {
                return false;
            }

            _members.Add(member);
            return true;
        }

        public Member? FindMemberById(int id)
        {
            return _members.FirstOrDefault(member =>
                member.Id == id);
        }

        public List<Member> GetAllMembers()
        {
            return _members.ToList();
        }

        public bool UpdateMember(
            int id,
            string name,
            string phone)
        {
            Member? member = FindMemberById(id);

            if (member == null)
            {
                return false;
            }

            member.UpdateContact(name, phone);

            return true;
        }

        public bool DeleteMember(int id)
        {
            Member? member = FindMemberById(id);

            if (member == null)
            {
                return false;
            }

            bool hasActiveBorrowing = _borrows.Any(borrow =>
                borrow.MemberId == id &&
                !borrow.IsReturned);

            if (hasActiveBorrowing)
            {
                return false;
            }

            _members.Remove(member);
            return true;
        }

        public List<Member> SearchMembers(string keyword)
        {
            return _members
                .Where(member =>
                    member.Name.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase) ||
                    member.Phone.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void DisplayAllMembers()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("                  ALL MEMBERS");
            Console.WriteLine("==================================================");

            if (_members.Count == 0)
            {
                Console.WriteLine("No members available.");
                return;
            }

            foreach (Member member in _members)
            {
                member.DisplayInfo();
            }
        }
    }
}