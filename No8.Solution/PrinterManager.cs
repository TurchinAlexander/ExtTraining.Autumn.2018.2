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
		public event EventHandler<string> OnPrinted = delegate { };

		private PrintersStorage printers = new PrintersStorage();
		private ILogger logger;
		private BasePrinter currentPrinter;

		/// <summary>
		/// Creation a <see cref="PrinterManager"/>.
		/// </summary>
		/// <param name="logger">The logger to write information.</param>
		/// <exception cref="ArgumentNullException">if <paramref name="logger"/> is null.</exception>
		public PrinterManager(ILogger logger)
		{
			this.logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} cannot be null");
			currentPrinter = null;
		}

		/// <summary>
		/// Add new printer.
		/// </summary>
		/// <param name="printer">New printer.</param>
		public void Add(BasePrinter printer)
		{
			if (!this.printers.Exists(printer))
			{
				this.printers.Add(printer);
				this.Log($"Printer {printer.Maker}.{printer.Model} is added");
			}
		}

		/// <summary>
		/// Print some information using chosed printer.
		/// </summary>
		/// <exception cref="InvalidOperationException">if printer wasn't chosed.</exception>
		public void Print()
		{
			if (currentPrinter == null)
				throw new InvalidOperationException("You didn't choose the printer.");

			string start = "Start printing";
			string end = "End printing";


			this.Log(start);
			this.OnPrinted(this, start);

			FileStream fs = TakeAFile();
			this.currentPrinter.Print(fs);

			this.Log(end);
			this.OnPrinted(this, start);

		}

		/// <summary>
		/// Show all makers.
		/// </summary>
		/// <returns>Array of <see cref="string"/>.</returns>
		public string[] ShowPrinterMaker()
		{
			return this.printers.ShowMakers();
		}

		/// <summary>
		/// Show all printers' makers.
		/// </summary>
		/// <param name="maker">The maker.</param>
		/// <returns>Array of <see cref="string"/>.</returns>
		/// <exception cref="ArgumentNullException">if <paramref name="maker"/> is null.</exception>
		/// <exception cref="ArgumentException">if <paramref name="maker"/> is invalid.</exception>
		public string[] ShowPrinters(string maker)
		{
			if (maker == null)
				throw new ArgumentNullException($"{nameof(maker)} is null.");

			string[] result;

			try
			{
				result = this.printers.ShowPrinters(maker);
			}
			catch (ArgumentException)
			{
				throw new ArgumentException(nameof(maker));
			}

			return result;
		}

		/// <summary>
		/// Choose which printer should we use.
		/// </summary>
		/// <param name="maker">The printer's maker.</param>
		/// <param name="model">The printer's model.</param>
		public void ChoosePrinter(string maker, string model)
		{
			currentPrinter = this.printers.GetPrinter(maker, model);
			this.Log($"User chosed {currentPrinter} printer.");
		}

		/// <summary>
		/// Logging some inforamation.
		/// </summary>
		/// <param name="s">The information.</param>
		public void Log(string s)
		{
			this.logger.Log(s);
		}

		/// <summary>
		/// Choose file by user.
		/// </summary>
		/// <returns>The <see cref="FileStream"/>.</returns>
		private FileStream TakeAFile()
		{
			var o = new OpenFileDialog();
			o.ShowDialog();
			var file = File.OpenRead(o.FileName);

			return file;
		}
	}
}
