using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileFragmentation.Model
{
    public class FileManager
    {
        private readonly string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        private readonly string inputFile = "input.txt";
        private readonly string outputFile = "output.txt";

        public FileManager()
        {
            Directory.CreateDirectory(dataPath);
        }

        public void ClearDataFolder()
        {
            if (!Directory.Exists(dataPath)) return;
            var di = new DirectoryInfo(dataPath);
            foreach (var file in di.GetFiles()) file.Delete();
            foreach (var dir in di.GetDirectories()) dir.Delete(true);
        }

        public void SaveInputFile(string content)
        {
            File.WriteAllText(Path.Combine(dataPath, inputFile), content ?? string.Empty);
        }

        public void FragmentFile(int size)
        {
            string path = Path.Combine(dataPath, inputFile);
            string content = File.Exists(path) ? File.ReadAllText(path) : string.Empty;

            if (content.Length == 0)
            {
                File.WriteAllText(Path.Combine(dataPath, "1.txt"), string.Empty);
                return;
            }

            int totalFiles = (int)Math.Ceiling((double)content.Length / size);
            int digits = totalFiles.ToString().Length;
            int index = 0;

            for (int i = 0; i < totalFiles; i++)
            {
                int take = Math.Min(size, content.Length - index);
                string part = content.Substring(index, take);
                index += take;
                string filename = (i + 1).ToString().PadLeft(digits, '0') + ".txt";
                File.WriteAllText(Path.Combine(dataPath, filename), part);
            }
        }

        public List<string> GetFragmentFiles()
        {
            if (!Directory.Exists(dataPath)) return new List<string>();

            return Directory.GetFiles(dataPath, "*.txt")
                            .Select(Path.GetFileName)
                            .Where(f => f != inputFile && f != outputFile)
                            .OrderBy(f => f)
                            .ToList()!;
        }

        public string? ReadFragment(string filename)
        {
            string path = Path.Combine(dataPath, filename);
            return File.Exists(path) ? File.ReadAllText(path) : null;
        }

        public string Defragment()
        {
            var fragments = GetFragmentFiles();
            var sb = new StringBuilder();

            foreach (var file in fragments)
                sb.Append(File.ReadAllText(Path.Combine(dataPath, file)));

            string combined = sb.ToString();
            File.WriteAllText(Path.Combine(dataPath, outputFile), combined);
            return combined;
        }

        public bool CompareFiles()
        {
            string inPath = Path.Combine(dataPath, inputFile);
            string outPath = Path.Combine(dataPath, outputFile);

            if (!File.Exists(inPath) || !File.Exists(outPath)) return false;

            string inputContent = File.ReadAllText(inPath);
            string outputContent = File.ReadAllText(outPath);

            return string.Equals(inputContent, outputContent, StringComparison.Ordinal);
        }
    }
}
