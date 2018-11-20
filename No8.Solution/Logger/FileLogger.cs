using System.IO;

using No8.Solution.Interfaces;

namespace No8.Solution.Logger
{
	/// <summary>
	/// Class to logging information to file.
	/// </summary>
	public class FileLogger : ILogger
	{
		private string fileName;
		private StreamWriter streamWriter;

		/// <summary>
		/// Creation of <see cref="FileLogger"/>.
		/// </summary>
		/// <param name="fileName">The file, which we should use for logging.</param>
		public FileLogger(string fileName)
		{
			this.fileName = fileName;
			streamWriter = new StreamWriter(File.Create(this.fileName));
		}

		/// <summary>
		/// Log information to the file.
		/// </summary>
		/// <param name="s"></param>
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
