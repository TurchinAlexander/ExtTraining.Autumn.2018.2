using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using No8.Solution.Printer;
using No8.Solution.Interfaces;

namespace No8.Solution
{
	public class PrinterManager
	{
		public event EventHandler<string> OnPrinted;

		private List<BasePrinter> listPrinters;
		private ILogger logger;

		/// <summary>
		/// Creation a <see cref="PrinterManager"/>.
		/// </summary>
		/// <param name="logger">The logger to write information.</param>
		public PrinterManager(ILogger logger)
		{
			if (logger == null)
				throw new ArgumentNullException($"{nameof(logger)} cannot be null");

			this.logger = logger;
			this.listPrinters = new List<BasePrinter>();
		}

		/// <summary>
		/// Add new printer.
		/// </summary>
		/// <param name="printer">New printer.</param>
		public void Add(BasePrinter printer)
		{
			if (!this.listPrinters.Contains(printer))
			{
				this.Log($"{printer.Name}.{printer.Model} is added");
				this.listPrinters.Add(printer);
			}
		}

		public void Print(int indexPrinter)
		{
			if ((indexPrinter < 0) && (indexPrinter > this.listPrinters.Count))
				throw new IndexOutOfRangeException($"{nameof(indexPrinter)} is out of range.");

			this.Log("Start printing");
			FileStream fs = TakeAFile();
			this.listPrinters[indexPrinter].Print(fs);
			this.Log("End printing");

		}

		public string[] ShowAll()
		{
			string[] result = new string[listPrinters.Count];

			for (int i = 0; i < listPrinters.Count; i++)
			{
				result[i] = $"{i}. {this.listPrinters[i].Name} {this.listPrinters[i].Model}";
			}

			return result;
		}

		/// <summary>
		/// Logging someinforamation.
		/// </summary>
		/// <param name="s">The information.</param>
		public void Log(string s)
		{
			this.logger.Log(s);
		}

		private FileStream TakeAFile()
		{
			var o = new OpenFileDialog();
			o.ShowDialog();
			var file = File.OpenRead(o.FileName);

			return file;
		}

		private string[] 
	}
}
