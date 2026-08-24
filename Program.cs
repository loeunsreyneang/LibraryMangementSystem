using System;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services;
using LibraryManagementSystem.Utilities;

namespace LibraryManagementSystem
{
    internal static class Program
    {
        private static readonly Library library = new();

        private static void Main()
        {
            Console.Title = "Library Management System";

            SeedData();
            RunMainMenu();
        }

        // ==================================================
        // MAIN MENU
        // ==================================================

        private static void RunMainMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("==================================================");
                Console.WriteLine("           LIBRARY MANAGEMENT SYSTEM");
                Console.WriteLine("==================================================");
                Console.WriteLine();
                Console.WriteLine("1. Author Management");
                Console.WriteLine("2. Book Management");
                Console.WriteLine("3. Member Management");
                Console.WriteLine("4. Borrow Book");
                Console.WriteLine("5. Return Book");
                Console.WriteLine("6. Search Book");
                Console.WriteLine("7. Borrow History");
                Console.WriteLine("8. Active Borrowings");
                Console.WriteLine("9. Display All Books");
                Console.WriteLine("10. Display All Members");
                Console.WriteLine("11. Display All Authors");
                Console.WriteLine("12. Exit");
                Console.WriteLine("==================================================");

                int choice =
                    InputHelper.GetInt("Enter your choice: ");

                if (choice == 1)
                {
                    AuthorManagementMenu();
                }
                else if (choice == 2)
                {
                    BookManagementMenu();
                }
                else if (choice == 3)
                {
                    MemberManagementMenu();
                }
                else if (choice == 4)
                {
                    BorrowBook();
                }
                else if (choice == 5)
                {
                    ReturnBook();
                }
                else if (choice == 6)
                {
                    SearchBook();
                }
                else if (choice == 7)
                {
                    Console.Clear();
                    library.DisplayBorrowHistory();
                    Pause();
                }
                else if (choice == 8)
                {
                    Console.Clear();
                    library.DisplayActiveBorrows();
                    Pause();
                }
                else if (choice == 9)
                {
                    Console.Clear();
                    library.DisplayAllBooks();
                    Pause();
                }
                else if (choice == 10)
                {
                    Console.Clear();
                    library.DisplayAllMembers();
                    Pause();
                }
                else if (choice == 11)
                {
                    Console.Clear();
                    library.DisplayAllAuthors();
                    Pause();
                }
                else if (choice == 12)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Thank you for using the Library Management System.");

                    return;
                }
                else
                {
                    Console.WriteLine("[WARNING] Invalid choice.");
                    Pause();
                }
            }
        }

        // ==================================================
        // AUTHOR MANAGEMENT
        // ==================================================

        private static void AuthorManagementMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("==================================================");
                Console.WriteLine("                AUTHOR MANAGEMENT");
                Console.WriteLine("==================================================");
                Console.WriteLine();
                Console.WriteLine("1. Add Author");
                Console.WriteLine("2. Update Author");
                Console.WriteLine("3. Delete Author");
                Console.WriteLine("4. Search Author");
                Console.WriteLine("5. Display All Authors");
                Console.WriteLine("6. Back");
                Console.WriteLine("==================================================");

                int choice =
                    InputHelper.GetInt("Enter your choice: ");

                if (choice == 1)
                {
                    AddAuthor();
                }
                else if (choice == 2)
                {
                    UpdateAuthor();
                }
                else if (choice == 3)
                {
                    DeleteAuthor();
                }
                else if (choice == 4)
                {
                    SearchAuthor();
                }
                else if (choice == 5)
                {
                    Console.Clear();
                    library.DisplayAllAuthors();
                    Pause();
                }
                else if (choice == 6)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("[WARNING] Invalid choice.");
                    Pause();
                }
            }
        }

        private static void AddAuthor()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                    ADD AUTHOR");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt("Author ID: ");

            if (library.FindAuthorById(id) != null)
            {
                Console.WriteLine("[ERROR] Author ID already exists.");
                Pause();
                return;
            }

            string name =
                InputHelper.GetString("Author Name: ");

            string biography =
                InputHelper.GetString("Biography: ");

            Author author =
                new(id, name, biography);

            if (library.AddAuthor(author))
            {
                Console.WriteLine(
                    "[SUCCESS] Author added successfully.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Failed to add author.");
            }

            Pause();
        }

        private static void UpdateAuthor()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                  UPDATE AUTHOR");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt("Author ID: ");

            Author? author =
                library.FindAuthorById(id);

            if (author == null)
            {
                Console.WriteLine("[ERROR] Author not found.");
                Pause();
                return;
            }

            author.DisplayInfo();

            string name =
                InputHelper.GetString(
                    "New name (leave blank to keep): ",
                    true);

            string biography =
                InputHelper.GetString(
                    "New biography (leave blank to keep): ",
                    true);

            if (string.IsNullOrWhiteSpace(name))
            {
                name = author.Name;
            }

            if (string.IsNullOrWhiteSpace(biography))
            {
                biography = author.Biography;
            }

            if (library.UpdateAuthor(
                id,
                name,
                biography))
            {
                Console.WriteLine(
                    "[SUCCESS] Author updated.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Failed to update author.");
            }

            Pause();
        }

        private static void DeleteAuthor()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                  DELETE AUTHOR");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt("Author ID: ");

            Author? author =
                library.FindAuthorById(id);

            if (author == null)
            {
                Console.WriteLine("[ERROR] Author not found.");
                Pause();
                return;
            }

            author.DisplayInfo();

            bool confirm =
                InputHelper.GetYesNo(
                    "Are you sure you want to delete this author?");

            if (!confirm)
            {
                Console.WriteLine("Delete cancelled.");
                Pause();
                return;
            }

            if (library.DeleteAuthor(id))
            {
                Console.WriteLine(
                    "[SUCCESS] Author deleted.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Author cannot be deleted because books are assigned to this author.");
            }

            Pause();
        }

        private static void SearchAuthor()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                  SEARCH AUTHOR");
            Console.WriteLine("==================================================");

            string keyword =
                InputHelper.GetString("Enter author name: ");

            var results =
                library.SearchAuthors(keyword);

            if (results.Count == 0)
            {
                Console.WriteLine("[INFO] No authors found.");
            }
            else
            {
                foreach (Author author in results)
                {
                    author.DisplayInfo();
                }
            }

            Pause();
        }

        // ==================================================
        // BOOK MANAGEMENT
        // ==================================================

        private static void BookManagementMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("==================================================");
                Console.WriteLine("                 BOOK MANAGEMENT");
                Console.WriteLine("==================================================");
                Console.WriteLine();
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Update Book");
                Console.WriteLine("3. Delete Book");
                Console.WriteLine("4. Search Book");
                Console.WriteLine("5. Display All Books");
                Console.WriteLine("6. Back");
                Console.WriteLine("==================================================");

                int choice =
                    InputHelper.GetInt("Enter your choice: ");

                if (choice == 1)
                {
                    AddBook();
                }
                else if (choice == 2)
                {
                    UpdateBook();
                }
                else if (choice == 3)
                {
                    DeleteBook();
                }
                else if (choice == 4)
                {
                    SearchBook();
                }
                else if (choice == 5)
                {
                    Console.Clear();
                    library.DisplayAllBooks();
                    Pause();
                }
                else if (choice == 6)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("[WARNING] Invalid choice.");
                    Pause();
                }
            }
        }

        private static void AddBook()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                    ADD BOOK");
            Console.WriteLine("==================================================");

            var authors =
                library.GetAllAuthors();

            if (authors.Count == 0)
            {
                Console.WriteLine(
                    "[ERROR] No authors available.");

                Console.WriteLine(
                    "Please create an author first.");

                Pause();
                return;
            }

            int id =
                InputHelper.GetPositiveInt("Book ID: ");

            if (library.FindBookById(id) != null)
            {
                Console.WriteLine(
                    "[ERROR] Book ID already exists.");

                Pause();
                return;
            }

            string title =
                InputHelper.GetString("Title: ");

            Console.WriteLine();
            Console.WriteLine("AVAILABLE AUTHORS");
            Console.WriteLine("--------------------------------------------------");

            foreach (Author author in authors)
            {
                Console.WriteLine(
                    $"{author.AuthorId}. {author.Name}");
            }

            Console.WriteLine("--------------------------------------------------");

            int authorId =
                InputHelper.GetPositiveInt(
                    "Select Author ID: ");

            Author? selectedAuthor =
                library.FindAuthorById(authorId);

            if (selectedAuthor == null)
            {
                Console.WriteLine(
                    "[ERROR] Author not found.");

                Pause();
                return;
            }

            int year =
                InputHelper.GetYear(
                    "Publication Year: ");

            Book book =
                new(
                    id,
                    title,
                    selectedAuthor,
                    year);

            if (library.AddBook(book))
            {
                Console.WriteLine();
                Console.WriteLine(
                    "[SUCCESS] Book added successfully.");

                Console.WriteLine($"Book   : {book.Title}");
                Console.WriteLine(
                    $"Author : {book.Author.Name}");
                Console.WriteLine($"Year   : {book.Year}");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Failed to add book.");
            }

            Pause();
        }

        private static void UpdateBook()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                  UPDATE BOOK");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt("Book ID: ");

            Book? book =
                library.FindBookById(id);

            if (book == null)
            {
                Console.WriteLine("[ERROR] Book not found.");
                Pause();
                return;
            }

            book.DisplayInfo();

            string title =
                InputHelper.GetString(
                    "New title (leave blank to keep): ",
                    true);

            if (string.IsNullOrWhiteSpace(title))
            {
                title = book.Title;
            }

            Console.WriteLine();
            Console.WriteLine("AVAILABLE AUTHORS");
            Console.WriteLine("--------------------------------------------------");

            foreach (Author author in library.GetAllAuthors())
            {
                Console.WriteLine(
                    $"{author.AuthorId}. {author.Name}");
            }

            Console.WriteLine("--------------------------------------------------");

            int authorId =
                InputHelper.GetPositiveInt(
                    "New Author ID: ");

            Author? selectedAuthor =
                library.FindAuthorById(authorId);

            if (selectedAuthor == null)
            {
                Console.WriteLine(
                    "[ERROR] Author not found.");

                Pause();
                return;
            }

            int year =
                InputHelper.GetYear(
                    "New year (enter 0 to keep): ",
                    true);

            if (year == 0)
            {
                year = book.Year;
            }

            if (library.UpdateBook(
                id,
                title,
                selectedAuthor,
                year))
            {
                Console.WriteLine(
                    "[SUCCESS] Book updated.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Failed to update book.");
            }

            Pause();
        }

        private static void DeleteBook()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                   DELETE BOOK");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt("Book ID: ");

            Book? book =
                library.FindBookById(id);

            if (book == null)
            {
                Console.WriteLine(
                    "[ERROR] Book not found.");

                Pause();
                return;
            }

            book.DisplayInfo();

            bool confirm =
                InputHelper.GetYesNo(
                    "Are you sure you want to delete this book?");

            if (!confirm)
            {
                Console.WriteLine(
                    "Delete cancelled.");

                Pause();
                return;
            }

            if (library.DeleteBook(id))
            {
                Console.WriteLine(
                    "[SUCCESS] Book deleted.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Book cannot be deleted because it is currently borrowed.");
            }

            Pause();
        }

        private static void SearchBook()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                   SEARCH BOOK");
            Console.WriteLine("==================================================");

            string keyword =
                InputHelper.GetString(
                    "Enter title or author: ");

            var results =
                library.SearchBooks(keyword);

            if (results.Count == 0)
            {
                Console.WriteLine(
                    "[INFO] No books found.");
            }
            else
            {
                foreach (Book book in results)
                {
                    book.DisplayInfo();
                }
            }

            Pause();
        }

        // ==================================================
        // MEMBER MANAGEMENT
        // ==================================================

        private static void MemberManagementMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("==================================================");
                Console.WriteLine("                MEMBER MANAGEMENT");
                Console.WriteLine("==================================================");
                Console.WriteLine();
                Console.WriteLine("1. Register Student Member");
                Console.WriteLine("2. Register Teacher Member");
                Console.WriteLine("3. Register Staff Member");
                Console.WriteLine("4. Update Member");
                Console.WriteLine("5. Delete Member");
                Console.WriteLine("6. Search Member");
                Console.WriteLine("7. Display All Members");
                Console.WriteLine("8. Back");
                Console.WriteLine("==================================================");

                int choice =
                    InputHelper.GetInt("Enter your choice: ");

                if (choice == 1)
                {
                    RegisterStudentMember();
                }
                else if (choice == 2)
                {
                    RegisterTeacherMember();
                }
                else if (choice == 3)
                {
                    RegisterStaffMember();
                }
                else if (choice == 4)
                {
                    UpdateMember();
                }
                else if (choice == 5)
                {
                    DeleteMember();
                }
                else if (choice == 6)
                {
                    SearchMember();
                }
                else if (choice == 7)
                {
                    Console.Clear();
                    library.DisplayAllMembers();
                    Pause();
                }
                else if (choice == 8)
                {
                    return;
                }
                else
                {
                    Console.WriteLine(
                        "[WARNING] Invalid choice.");

                    Pause();
                }
            }
        }

        private static void RegisterStudentMember()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("             REGISTER STUDENT MEMBER");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt(
                    "Member ID: ");

            if (library.FindMemberById(id) != null)
            {
                Console.WriteLine(
                    "[ERROR] Member ID already exists.");

                Pause();
                return;
            }

            string name =
                InputHelper.GetString("Name: ");

            string phone =
                InputHelper.GetString("Phone: ");

            string studentId =
                InputHelper.GetString("Student ID: ");

            int year =
                InputHelper.GetPositiveInt("Year: ");

            StudentMember student =
                new(
                    id,
                    name,
                    phone,
                    studentId,
                    year);

            if (library.AddMember(student))
            {
                Console.WriteLine(
                    "[SUCCESS] Student member registered.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Failed to register student.");
            }

            Pause();
        }

        private static void RegisterTeacherMember()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("             REGISTER TEACHER MEMBER");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt(
                    "Member ID: ");

            if (library.FindMemberById(id) != null)
            {
                Console.WriteLine(
                    "[ERROR] Member ID already exists.");

                Pause();
                return;
            }

            string name =
                InputHelper.GetString("Name: ");

            string phone =
                InputHelper.GetString("Phone: ");

            string teacherId =
                InputHelper.GetString("Teacher ID: ");

            string department =
                InputHelper.GetString("Department: ");

            TeacherMember teacher =
                new(
                    id,
                    name,
                    phone,
                    teacherId,
                    department);

            if (library.AddMember(teacher))
            {
                Console.WriteLine(
                    "[SUCCESS] Teacher member registered.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Failed to register teacher.");
            }

            Pause();
        }

        private static void RegisterStaffMember()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("              REGISTER STAFF MEMBER");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt(
                    "Member ID: ");

            if (library.FindMemberById(id) != null)
            {
                Console.WriteLine(
                    "[ERROR] Member ID already exists.");

                Pause();
                return;
            }

            string name =
                InputHelper.GetString("Name: ");

            string phone =
                InputHelper.GetString("Phone: ");

            string staffId =
                InputHelper.GetString("Staff ID: ");

            string position =
                InputHelper.GetString("Position: ");

            StaffMember staff =
                new(
                    id,
                    name,
                    phone,
                    staffId,
                    position);

            if (library.AddMember(staff))
            {
                Console.WriteLine(
                    "[SUCCESS] Staff member registered.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Failed to register staff.");
            }

            Pause();
        }

        private static void UpdateMember()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                  UPDATE MEMBER");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt(
                    "Member ID: ");

            Member? member =
                library.FindMemberById(id);

            if (member == null)
            {
                Console.WriteLine(
                    "[ERROR] Member not found.");

                Pause();
                return;
            }

            member.DisplayInfo();

            string name =
                InputHelper.GetString(
                    "New name (leave blank to keep): ",
                    true);

            string phone =
                InputHelper.GetString(
                    "New phone (leave blank to keep): ",
                    true);

            if (string.IsNullOrWhiteSpace(name))
            {
                name = member.Name;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                phone = member.Phone;
            }

            if (library.UpdateMember(
                id,
                name,
                phone))
            {
                Console.WriteLine(
                    "[SUCCESS] Member updated.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Failed to update member.");
            }

            Pause();
        }

        private static void DeleteMember()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                  DELETE MEMBER");
            Console.WriteLine("==================================================");

            int id =
                InputHelper.GetPositiveInt(
                    "Member ID: ");

            Member? member =
                library.FindMemberById(id);

            if (member == null)
            {
                Console.WriteLine(
                    "[ERROR] Member not found.");

                Pause();
                return;
            }

            member.DisplayInfo();

            bool confirm =
                InputHelper.GetYesNo(
                    "Are you sure you want to delete this member?");

            if (!confirm)
            {
                Console.WriteLine(
                    "Delete cancelled.");

                Pause();
                return;
            }

            if (library.DeleteMember(id))
            {
                Console.WriteLine(
                    "[SUCCESS] Member deleted.");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Cannot delete member because they have an active borrowing.");
            }

            Pause();
        }

        private static void SearchMember()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                  SEARCH MEMBER");
            Console.WriteLine("==================================================");

            string keyword =
                InputHelper.GetString(
                    "Enter name or phone: ");

            var results =
                library.SearchMembers(keyword);

            if (results.Count == 0)
            {
                Console.WriteLine(
                    "[INFO] No members found.");
            }
            else
            {
                foreach (Member member in results)
                {
                    member.DisplayInfo();
                }
            }

            Pause();
        }

        // ==================================================
        // BORROW
        // ==================================================

        private static void BorrowBook()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                    BORROW BOOK");
            Console.WriteLine("==================================================");

            int bookId =
                InputHelper.GetPositiveInt(
                    "Book ID: ");

            int memberId =
                InputHelper.GetPositiveInt(
                    "Member ID: ");

            Book? book =
                library.FindBookById(bookId);

            if (book == null)
            {
                Console.WriteLine(
                    "[ERROR] Book not found.");

                Pause();
                return;
            }

            Member? member =
                library.FindMemberById(memberId);

            if (member == null)
            {
                Console.WriteLine(
                    "[ERROR] Member not found.");

                Pause();
                return;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine(
                    "[ERROR] Book is currently borrowed.");

                Pause();
                return;
            }

            if (library.BorrowBook(
                bookId,
                memberId))
            {
                Console.WriteLine();
                Console.WriteLine(
                    "[SUCCESS] Book borrowed successfully.");

                Console.WriteLine(
                    $"Book   : {book.Title}");

                Console.WriteLine(
                    $"Author : {book.Author.Name}");

                Console.WriteLine(
                    $"Member : {member.Name}");

                Console.WriteLine(
                    "Loan period: 7 days");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Could not borrow book.");
            }

            Pause();
        }

        // ==================================================
        // RETURN
        // ==================================================

        private static void ReturnBook()
        {
            Console.Clear();

            Console.WriteLine("==================================================");
            Console.WriteLine("                    RETURN BOOK");
            Console.WriteLine("==================================================");

            int bookId =
                InputHelper.GetPositiveInt(
                    "Book ID: ");

            if (library.ReturnBook(
                bookId,
                out decimal fine))
            {
                Console.WriteLine();
                Console.WriteLine(
                    "[SUCCESS] Book returned successfully.");

                Console.WriteLine(
                    $"Fine: ${fine:0.00}");
            }
            else
            {
                Console.WriteLine(
                    "[ERROR] Book cannot be returned.");
            }

            Pause();
        }

        // ==================================================
        // PAUSE
        // ==================================================

        private static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine(
                "Press any key to continue...");

            Console.ReadKey(true);
        }

        // ==================================================
        // SEED DATA
        // ==================================================

        private static void SeedData()
        {
            // ==================================================
            // AUTHORS
            // ==================================================

            Author author1 =
                new(
                    1,
                    "Robert C. Martin",
                    "Software engineer and author of Clean Code.");

            Author author2 =
                new(
                    2,
                    "Andrew Hunt",
                    "Co-author of The Pragmatic Programmer.");

            Author author3 =
                new(
                    3,
                    "John Smith",
                    "Author of C# Programming.");

            library.AddAuthor(author1);
            library.AddAuthor(author2);
            library.AddAuthor(author3);

            // ==================================================
            // BOOKS
            // ==================================================

            library.AddBook(
                new Book(
                    1,
                    "Clean Code",
                    author1,
                    2008));

            library.AddBook(
                new Book(
                    2,
                    "The Pragmatic Programmer",
                    author2,
                    1999));

            library.AddBook(
                new Book(
                    3,
                    "C# Programming",
                    author3,
                    2022));

            // ==================================================
            // STUDENT
            // ==================================================

            library.AddMember(
                new StudentMember(
                    1,
                    "Dara",
                    "012345678",
                    "ST001",
                    2));

            // ==================================================
            // TEACHER
            // ==================================================

            library.AddMember(
                new TeacherMember(
                    2,
                    "Sokha",
                    "098765432",
                    "T001",
                    "Information Technology"));

            // ==================================================
            // STAFF
            // ==================================================

            library.AddMember(
                new StaffMember(
                    3,
                    "Sreyneang",
                    "097654321",
                    "SF001",
                    "Librarian"));
        }
    }
}