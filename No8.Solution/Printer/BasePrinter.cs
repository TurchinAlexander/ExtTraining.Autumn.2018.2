using System;
using System.IO;

namespace No8.Solution.Printer
{
	/// <summary>
	/// Base class for all printers.
	/// </summary>
	public class BasePrinter
	{
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

			this.Maker = maker;
			this.Model = model;
		}

		/// <summary>
		/// Creator of the printer.
		/// </summary>
		public string Maker { get; private set; }

		/// <summary>
		/// Model of the printer.
		/// </summary>
		public string Model { get; private set; }

		/// <summary>
		/// Method to print some information.
		/// </summary>
		/// <param name="fs">Input stream of data.</param>
		public virtual void Print(FileStream fs)
		{
			for (int i = 0; i < fs.Length; i++)
			{
				Console.WriteLine(fs.ReadByte());
			}
		}

		public override string ToString()
		{
			return $"{this.Maker}.{this.Model}";
		}
	}
}
