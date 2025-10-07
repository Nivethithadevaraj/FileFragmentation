using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileFragmentation.Model
{
	public class FileManager
	{
		private readonly string dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
		private readonly string inputFile = "input.txt";
		private readonly string outputFile = "output.txt";

		public FileManager()
		{
			if (!Directory.Exists(dataPath))
				Directory.CreateDirectory(dataPath);
		}

		public void ClearDataFolder()
		{
			DirectoryInfo di = new DirectoryInfo(dataPath);
			foreach (FileInfo file in di.GetFiles())
			{
				file.Delete();
			}
		}

		public void SaveInputFile(string content)
		{
			File.WriteAllText(Path.Combine(dataPath, inputFile), content);
		}

		public void FragmentFile(int size)
		{
			string content = File.ReadAllText(Path.Combine(dataPath, inputFile));

			int totalFiles = (int)Math.Ceiling((double)content.Length / size);

			int digits = totalFiles.ToString().Length; // For leading zeros
			int index = 0;

			for (int i = 0; i < totalFiles; i++)
			{
				string part = content.Substring(index, Math.Min(size, content.Length - index));
				index += size;

				string filename = (i + 1).ToString().PadLeft(digits, '0') + ".txt";
				File.WriteAllText(Path.Combine(dataPath, filename), part);
			}
		}

		public List<string> GetFragmentFiles()
		{
			return Directory.GetFiles(dataPath, "*.txt")
							.Where(f => !f.EndsWith(inputFile) && !f.EndsWith(outputFile))
							.Select(Path.GetFileName)
							.OrderBy(f => f)
							.ToList();
		}

		public string ReadFragment(string filename)
		{
			string path = Path.Combine(dataPath, filename);
			if (!File.Exists(path)) return null;
			return File.ReadAllText(path);
		}

		public string Defragment()
		{
			var fragments = GetFragmentFiles();
			string combined = "";

			foreach (string file in fragments)
			{
				combined += File.ReadAllText(Path.Combine(dataPath, file));
			}

			File.WriteAllText(Path.Combine(dataPath, outputFile), combined);
			return combined;
		}

		public bool CompareFiles()
		{
			string inputContent = File.ReadAllText(Path.Combine(dataPath, inputFile));
			string outputContent = File.ReadAllText(Path.Combine(dataPath, outputFile));
			return inputContent.Equals(outputContent);
		}
	}
}
