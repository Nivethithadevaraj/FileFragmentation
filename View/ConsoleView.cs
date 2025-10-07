using System;

namespace FileFragmentation.View
{
    public class ConsoleView
    {
        public string GetParagraphInput()
        {
            List<string> lines = new();
            string? line;

            Console.WriteLine("Enter your paragraph (type END in a new line to finish):");
            while (true)
            {
                line = Console.ReadLine();

                if (line == null)
                {
                    Console.WriteLine("?? Input is null. Please type again.");
                    continue;
                }

                if (line.Trim().Equals("END", StringComparison.OrdinalIgnoreCase))
                    break;

                // Ignore completely empty lines (e.g., pressing Enter after END or multiple enters)
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                lines.Add(line);
            }

            // remove trailing empty lines if any
            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[^1]))
            {
                lines.RemoveAt(lines.Count - 1);
            }

            return string.Join(Environment.NewLine, lines);
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
