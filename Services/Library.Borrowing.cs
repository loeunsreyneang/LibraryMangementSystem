using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public partial class Library
    {
        // ==================================================
        // BORROW BOOK
        // ==================================================

        public bool BorrowBook(int bookId, int memberId)
        {
            Book? book = FindBookById(bookId);
            Member? member = FindMemberById(memberId);

            if (book == null || member == null)
            {
                return false;
            }

            if (!book.IsAvailable)
            {
                return false;
            }

            bool alreadyBorrowed = _borrows.Any(borrow =>
                borrow.BookId == bookId &&
                !borrow.IsReturned);

            if (alreadyBorrowed)
            {
                return false;
            }

            DateTime borrowDate = DateTime.Now;
            DateTime dueDate = borrowDate.AddDays(7);

            Borrow borrow = new Borrow(
                _nextBorrowId++,
                bookId,
                memberId,
                borrowDate,
                dueDate);

            _borrows.Add(borrow);
            book.Borrow();

            return true;
        }

        // ==================================================
        // RETURN BOOK
        // ==================================================

        public bool ReturnBook(
            int bookId,
            out decimal fine)
        {
            fine = 0;

            Book? book = FindBookById(bookId);

            if (book == null)
            {
                return false;
            }

            Borrow? borrowing = _borrows.FirstOrDefault(borrow =>
                borrow.BookId == bookId &&
                !borrow.IsReturned);

            if (borrowing == null)
            {
                return false;
            }

            DateTime returnDate = DateTime.Now;

            fine = CalculateFine(
                borrowing.DueDate,
                returnDate);

            borrowing.CompleteReturn(
                returnDate,
                fine);

            book.Return();

            return true;
        }

        // ==================================================
        // CALCULATE FINE
        // ==================================================

        private static decimal CalculateFine(
            DateTime dueDate,
            DateTime returnDate)
        {
            if (returnDate.Date <= dueDate.Date)
            {
                return 0;
            }

            int lateDays =
                (returnDate.Date - dueDate.Date).Days;

            const decimal finePerDay = 1.00m;

            return lateDays * finePerDay;
        }

        // ==================================================
        // ACTIVE BORROWINGS
        // ==================================================

        public void DisplayActiveBorrows()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("                 ACTIVE BORROWINGS");
            Console.WriteLine("==================================================");

            List<Borrow> activeBorrows = _borrows
                .Where(borrow => !borrow.IsReturned)
                .ToList();

            if (activeBorrows.Count == 0)
            {
                Console.WriteLine("No active borrowings.");
                return;
            }

            foreach (Borrow borrow in activeBorrows)
            {
                Book? book = FindBookById(borrow.BookId);
                Member? member = FindMemberById(borrow.MemberId);

                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Borrow ID  : {borrow.Id}");
                Console.WriteLine(
                    $"Book       : {book?.Title ?? "Unknown"}");
                Console.WriteLine(
                    $"Member     : {member?.Name ?? "Unknown"}");
                Console.WriteLine(
                    $"Borrow Date: {borrow.BorrowDate:yyyy-MM-dd}");
                Console.WriteLine(
                    $"Due Date   : {borrow.DueDate:yyyy-MM-dd}");
                Console.WriteLine("--------------------------------------------------");
            }
        }

        // ==================================================
        // BORROW HISTORY
        // ==================================================

        public void DisplayBorrowHistory()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("                  BORROW HISTORY");
            Console.WriteLine("==================================================");

            if (_borrows.Count == 0)
            {
                Console.WriteLine("No borrowing history.");
                return;
            }

            foreach (Borrow borrow in _borrows)
            {
                Book? book = FindBookById(borrow.BookId);
                Member? member = FindMemberById(borrow.MemberId);

                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Borrow ID  : {borrow.Id}");
                Console.WriteLine(
                    $"Book       : {book?.Title ?? "Unknown"}");
                Console.WriteLine(
                    $"Member     : {member?.Name ?? "Unknown"}");
                Console.WriteLine(
                    $"Borrow Date: {borrow.BorrowDate:yyyy-MM-dd}");
                Console.WriteLine(
                    $"Due Date   : {borrow.DueDate:yyyy-MM-dd}");

                if (borrow.IsReturned)
                {
                    Console.WriteLine(
                        $"Return Date: {borrow.ReturnDate:yyyy-MM-dd}");

                    Console.WriteLine(
                        $"Fine       : ${borrow.Fine:0.00}");
                }
                else
                {
                    Console.WriteLine("Status     : Active");
                }

                Console.WriteLine("--------------------------------------------------");
            }
        }
    }
}