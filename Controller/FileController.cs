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
                // Step 0 ? Clean Data folder before every run
                manager.ClearDataFolder();

                // Step 1 ? Input paragraph
                string paragraph = view.GetParagraphInput();
                manager.SaveInputFile(paragraph);

                // Step 2 ? Fragmentation
                int size = view.GetFragmentSize();
                manager.FragmentFile(size);
                view.DisplayFiles(manager.GetFragmentFiles());

                // Step 3 ? File display option
                while (true)
                {
                    bool choice = view.AskToDisplayFile();
                    if (!choice) break;

                    string filename = view.GetFileName();
                    string content = manager.ReadFragment(filename);
                    if (content == null)
                        view.ShowMessage("? File does not exist.");
                    else
                        view.ShowMessage($"Content of {filename}: \n{content}");
                }

                // Step 4 ? Defragmentation
                string defragmented = manager.Defragment();
                view.ShowMessage("\n?? Defragmented content:");
                Console.WriteLine(defragmented);

                // Step 5 ? Compare
                if (manager.CompareFiles())
                    view.ShowMessage("\n? SUCCESS: Input and Output are the same!");
                else
                    view.ShowMessage("\n? Something went wrong. Files do not match.");

            }
            catch (Exception ex)
            {
                view.ShowMessage($"?? Error: {ex.Message}");
            }
        }
    }
}
