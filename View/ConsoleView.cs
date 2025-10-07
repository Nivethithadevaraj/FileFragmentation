using System;

namespace FileFragmentation.View
{
    public class ConsoleView
    {
        public string GetParagraphInput()
        {
            Console.WriteLine("Enter your paragraph (type END in a new line to finish):");

            string input = "";
            while (true)
            {
                string? line = Console.ReadLine();

                if (line == null) // null check (rare, happens if input stream is broken)
                {
                    Console.WriteLine("?? Input was null. Please enter text or END to finish:");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.WriteLine("?? Empty line entered. Type something or END to finish:");
                    continue;
                }

                if (line.Trim().Equals("END", StringComparison.OrdinalIgnoreCase))
                    break;

                input += line + Environment.NewLine;
            }

            // If user never typed anything except END
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("?? No content entered. Defaulting to empty paragraph.");
                return "";
            }

            return input;
        }

        public int GetFragmentSize()
        {
            Console.WriteLine("Enter the fragment size (integer > 0): ");
            while (true)
            {
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("?? Input cannot be empty. Please enter a number:");
                    continue;
                }

                if (int.TryParse(input, out int size) && size > 0)
                    return size;

                Console.WriteLine("?? Invalid input. Please enter a positive integer:");
            }
        }

        public void DisplayFiles(System.Collections.Generic.List<string> files)
        {
            Console.WriteLine("\n?? Fragmented Files:");
            if (files.Count == 0)
            {
                Console.WriteLine("?? No files created.");
                return;
            }

            foreach (var file in files)
                Console.WriteLine(file);
        }

        public bool AskToDisplayFile()
        {
            Console.WriteLine("\nDo you want to view any file? (Y/N)");

            while (true)
            {
                string? input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("?? Invalid input. Please type Y or N:");
                    continue;
                }

                input = input.Trim().ToUpper();

                if (input == "Y") return true;
                if (input == "N") return false;

                Console.WriteLine("?? Invalid choice. Enter Y or N:");
            }
        }

        public string GetFileName()
        {
            while (true)
            {
                Console.WriteLine("Enter the file name (e.g., 01.txt): ");
                string? fileName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    Console.WriteLine("?? File name cannot be empty.");
                    continue;
                }

                return fileName.Trim();
            }
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
