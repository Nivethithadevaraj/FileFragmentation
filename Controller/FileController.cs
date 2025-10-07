using System;
using FileFragmentation.View;
using FileFragmentation.Model;

namespace FileFragmentation.Controller
{
    public class FileController
    {
        private readonly ConsoleView view;
        private readonly FileManager manager;

        public FileController()
        {
            view = new ConsoleView();
            manager = new FileManager();
        }

        public void Start()
        {
            try
            {
                manager.ClearDataFolder();

                string paragraph = view.GetParagraphInput();
                manager.SaveInputFile(paragraph);

                int size = view.GetFragmentSize();
                manager.FragmentFile(size);
                view.DisplayFiles(manager.GetFragmentFiles());

                while (true)
                {
                    if (!view.AskToDisplayFile()) break;
                    string filename = view.GetFileName();
                    string? content = manager.ReadFragment(filename);
                    view.ShowFileContent(filename, content);
                }

                string defragmented = manager.Defragment();
                view.ShowMessage("\nDefragmented content:", ConsoleColor.Cyan);
                Console.WriteLine(defragmented);

                if (manager.CompareFiles())
                    view.ShowMessage("\n SUCCESS: Input and Output are the same!", ConsoleColor.Green);
                else
                    view.ShowMessage("\n Files do not match.", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                view.ShowMessage($" Error: {ex.Message}", ConsoleColor.Red);
            }
        }
    }
}
