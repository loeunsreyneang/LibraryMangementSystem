using System;

namespace LibraryManagementSystem.Utilities
{
    public static class InputHelper
    {
        public static int GetInt(string message)
        {
            while (true)
            {
                Console.Write(message);

                string input = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                Console.WriteLine("[ERROR] Please enter a valid number.");
            }
        }

        public static int GetPositiveInt(string message)
        {
            while (true)
            {
                int value = GetInt(message);

                if (value > 0)
                {
                    return value;
                }

                Console.WriteLine(
                    "[ERROR] Please enter a number greater than 0.");
            }
        }

        public static int GetYear(
            string message,
            bool allowZeroOk = false)
        {
            int currentYear = DateTime.Now.Year;

            while (true)
            {
                int year = GetInt(message);

                if (allowZeroOk && year == 0)
                {
                    return 0;
                }

                if (year >= 1000 && year <= currentYear)
                {
                    return year;
                }

                Console.WriteLine(
                    $"[ERROR] Enter a year between 1000 and {currentYear}.");
            }
        }

        public static string GetString(
            string message,
            bool allowEmpty = false)
        {
            while (true)
            {
                Console.Write(message);

                string value = Console.ReadLine() ?? string.Empty;

                value = value.Trim();

                if (allowEmpty || !string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }

                Console.WriteLine("[ERROR] This field cannot be empty.");
            }
        }

        public static bool GetYesNo(string message)
        {
            while (true)
            {
                Console.Write($"{message} (Y/N): ");

                string input =
                    (Console.ReadLine() ?? string.Empty)
                    .Trim();

                if (input.Equals(
                    "Y",
                    StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                if (input.Equals(
                    "N",
                    StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                Console.WriteLine("[ERROR] Please enter Y or N.");
            }
        }
    }
}