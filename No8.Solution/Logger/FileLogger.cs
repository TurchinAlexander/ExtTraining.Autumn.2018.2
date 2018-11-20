using System.IO;

using No8.Solution.Interfaces;

namespace No8.Solution.Logger
{
	public class FileLogger : ILogger
	{
		private string fileName;
		private StreamWriter streamWriter;

		public FileLogger(string fileName)
		{
			this.fileName = fileName;
			streamWriter = new StreamWriter(File.Create(this.fileName));
		}

		public void Log(string s)
		{
			streamWriter.WriteLine(s);
		}

		~FileLogger()
		{
			streamWriter.Close();
		}
	}
}
