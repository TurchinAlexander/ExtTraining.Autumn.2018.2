using System;
using System.IO;

using No8.Solution.Printer.Data;

namespace No8.Solution.Printer.Entities
{
	/// <summary>
	/// Base class for all printers.
	/// </summary>
	public abstract class BasePrinter
	{
		public PrinterData data;

		/// <summary>
		/// Creation of <see cref="BasePrinter"/>.
		/// </summary>
		/// <param name="maker">The printer's name.</param>
		/// <param name="model">The printer's model.</param>
		/// <exception cref="ArgumentNullException">if <paramref name="maker"/> or <paramref name="model"/> is null.</exception>
		public BasePrinter(string maker, string model)
		{
			if (maker == null)
				throw new ArgumentNullException($"{nameof(maker)} cannot be null.");
			if (model == null)
				throw new ArgumentNullException($"{nameof(maker)} cannot be null.");

			data = new PrinterData() { Maker = maker, Model = model };
		}

		/// <summary>
		/// Method to print some information.
		/// </summary>
		/// <param name="fs">Input stream of data.</param>
		public abstract void Print(FileStream fs);

		public override string ToString()
		{
			return data.ToString();
		}
	}
}
