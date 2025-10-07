using System;
using System.Collections.Generic;

namespace FileFragmentation.View
{
    public class ConsoleView
    {
        public string GetParagraphInput()
        {
            while (true)
            {
                ShowMessage("Enter your paragraph (type END to finish):", ConsoleColor.Yellow);
                var lines = new List<string>();

                while (true)
                {
                    string? line = Console.ReadLine();
                    if (line != null && line.Trim().Equals("END", StringComparison.OrdinalIgnoreCase))
                        break;
                    lines.Add(line ?? "");
                }

                while (lines.Count > 0 && string.IsNullOrEmpty(lines[^1]))
                    lines.RemoveAt(lines.Count - 1);

                if (lines.Count > 0)
                    return string.Join(Environment.NewLine, lines);

                ShowMessage("Paragraph cannot be empty.", ConsoleColor.Red);
            }
        }

        public int GetFragmentSize()
        {
            ShowMessage("Enter fragment size:", ConsoleColor.Yellow);

            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int size) && size > 0)
                    return size;

                ShowMessage("Invalid number, try again:", ConsoleColor.Red);
            }
        }

        public void DisplayFiles(List<string> files)
        {
            ShowMessage("\n Fragmented Files:", ConsoleColor.Magenta);
            if (files.Count == 0) ShowMessage("No fragments found.", ConsoleColor.Yellow);
            foreach (var f in files) Console.WriteLine(f);
        }

        public bool AskToDisplayFile()
        {
            ShowMessage("\nDo you want to view a file? (Y/N)", ConsoleColor.Yellow);

            while (true)
            {
                string? input = Console.ReadLine()?.Trim().ToUpper();
                if (input == "Y" || input == "YES") return true;
                if (input == "N" || input == "NO") return false;
                ShowMessage("Enter Y or N.", ConsoleColor.Red);
            }
        }

        public string GetFileName()
        {
            ShowMessage("Enter filename (e.g., 01.txt):", ConsoleColor.Yellow);
            return Console.ReadLine()!.Trim();
        }

        public void ShowFileContent(string filename, string? content)
        {
            if (content == null)
                ShowMessage($"File {filename} not found.", ConsoleColor.Red);
            else
            {
                ShowMessage($"\nContent of {filename}:", ConsoleColor.Cyan);
                Console.WriteLine(content);
            }
        }

        public void ShowMessage(string message, ConsoleColor color = ConsoleColor.Gray)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = prev;
        }
    }
}
