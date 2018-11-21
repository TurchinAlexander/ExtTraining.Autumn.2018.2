using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using No8.Solution.Printer.Data;
using No8.Solution.Printer.Entities;

namespace No8.Solution
{
	/// <summary>
	/// Represent class to store <see cref="BasePrinter"/> classes structed.
	/// </summary>
	internal class PrintersStorage
	{
		private Dictionary<string, Dictionary<string, BasePrinter>> printers = new Dictionary<string, Dictionary<string, BasePrinter>>();

		/// <summary>
		/// Add <see cref="BasePrinter"/> to the storage.
		/// </summary>
		/// <param name="printer">The <see cref="BasePrinter"/>.</param>
		public void Add(BasePrinter printer)
		{
			PrinterData data = printer.data;
			CheckData(data);

			if (this.Exists(printer))
				return;

			if (!this.printers.ContainsKey(data.Maker))
			{
				Dictionary<string, BasePrinter> temp = new Dictionary<string, BasePrinter>();
				temp.Add(data.Model, printer);

				this.printers.Add(data.Maker, temp);
			} else
			{
				this.printers[data.Maker].Add(data.Model, printer);
			}
		}

		/// <summary>
		/// Check if <see cref="BasePrinter"/> is storaged.
		/// </summary>
		/// <param name="printer">The <see cref="BasePrinter"/>.</param>
		/// <returns><c>true</c> if yes. Otherwise, <c>false</c>.</returns>
		public bool Exists(BasePrinter printer)
		{
			if (this.printers.ContainsKey(printer.data.Maker))
			{
				return this.printers[printer.data.Maker].ContainsKey(printer.data.Model);
			}

			return false;
		}

		/// <summary>
		/// Show all makers, which we have in the storage.
		/// </summary>
		/// <returns>Array of <see cref="string"/>.</returns>
		public string[] ShowMakers()
		{
			return this.printers.Keys.ToArray();
		}

		/// <summary>
		/// Show all printer's models by maker.
		/// </summary>
		/// <param name="maker">The maker's name.</param>
		/// <returns>Array of <see cref="string"/>.</returns>
		/// <exception cref="ArgumentNullException">if <paramref name="maker"/> is null.</exception>
		/// <exception cref="ArgumentException">if <paramref name="maker"/> is invalid.</exception>
		public string[] ShowPrinters(string maker)
		{
			if (maker == null)
				throw new ArgumentNullException($"{nameof(maker)} is null.");
			if (!this.printers.ContainsKey(maker))
				throw new ArgumentException($"There is no such maker {nameof(maker)}.");

			return this.printers[maker].Keys.ToArray();
		}

		/// <summary>
		/// Take <see cref="BasePrinter"/> from the storage.
		/// </summary>
		/// <param name="maker">The printer's maker.</param>
		/// <param name="model">The printer's model.</param>
		/// <returns></returns>
		public BasePrinter GetPrinter(PrinterData data)
		{
			return this.printers[data.Maker][data.Model];
		}

		/// <summary>
		/// Check input data for valid.
		/// </summary>
		/// <param name="data">The data to check.</param>
		private void CheckData(PrinterData data)
		{
			if (data.Maker == null)
				throw new ArgumentNullException(nameof(data.Maker));
			if (data.Model == null)
				throw new ArgumentNullException(nameof(data.Model));

			if (data.Maker.Length == 0)
				throw new ArgumentException(nameof(data.Maker));
			if (data.Model.Length == 0)
				throw new ArgumentException(nameof(data.Model));
		}
	}
}
