using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using No8.Solution.Printer.Entities;
using No8.Solution.Logger.Interface;

namespace No8.Solution
{
	public class PrinterManager
	{
		private List<BasePrinter> printerList = new List<BasePrinter>();
		private ILogger logger;

		/// <summary>
		/// Creation a <see cref="PrinterManager"/>.
		/// </summary>
		/// <param name="logger">The logger to write information.</param>
		/// <exception cref="ArgumentNullException">if <paramref name="logger"/> is null.</exception>
		public PrinterManager(ILogger logger)
		{
			this.logger = logger ?? throw new ArgumentNullException($"{nameof(logger)} cannot be null");
		}

		/// <summary>
		/// Add new printer.
		/// </summary>
		/// <param name="printer">New printer.</param>
		public void Add(BasePrinter printer)
		{
            if (this.printerList.Contains(printer))
                return;

			this.printerList.Add(printer);
            this.Log($"Printer {printer} is added");

            printer.StartPrint += (sender, text) => this.Log(text);
            printer.EndPrint += (sender, text) => this.Log(text);
            
		}

		/// <summary>
		/// Print some information using chosed printer.
		/// </summary>
		/// <exception cref="InvalidOperationException">if printer wasn't chosed.</exception>
		public void Print(BasePrinter printer, FileStream fs)
		{
            if (!this.printerList.Contains(printer))
                throw new ArgumentException($"{printer} is not registered.");

			printer.Print(fs);
		}

		/// <summary>
		/// Show all makers.
		/// </summary>
		/// <returns>Array of <see cref="string"/>.</returns>
		public BasePrinter[] GetAllPrinters()
		{
            printerList.Sort((a, b) =>
            {
                int resultCompare = a.Maker.CompareTo(b.Maker);
                if (resultCompare != 0)
                    return resultCompare;

                resultCompare = a.Model.CompareTo(b.Model);
                return resultCompare;

            });

			return this.printerList.ToArray();
		}

        /// <summary>
        /// Logging some inforamation.
        /// </summary>
        /// <param name="s">The information.</param>
        public void Log(string s)
		{
			this.logger.Log(s);
		}
    }
}
