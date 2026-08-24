using System;

namespace LibraryManagementSystem.Models
{
    public class Borrow
    {
        public Borrow(
            int id,
            int bookId,
            int memberId,
            DateTime borrowDate,
            DateTime dueDate)
        {
            Id = id;
            BookId = bookId;
            MemberId = memberId;
            BorrowDate = borrowDate;
            DueDate = dueDate;
        }

        public int Id { get; }

        public int BookId { get; }

        public int MemberId { get; }

        public DateTime BorrowDate { get; }

        public DateTime DueDate { get; }

        public DateTime? ReturnDate { get; private set; }

        public decimal Fine { get; private set; }

        public bool IsReturned => ReturnDate.HasValue;

        public void CompleteReturn(DateTime returnDate, decimal fine)
        {
            ReturnDate = returnDate;
            Fine = fine;
        }
    }
}