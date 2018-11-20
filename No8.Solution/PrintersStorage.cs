using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using No8.Solution.Printer;

namespace No8.Solution
{
	/// <summary>
	/// Represent class to store <see cref="BasePrinter"/> classes structed.
	/// </summary>
	public class PrintersStorage
	{
		private Dictionary<string, Dictionary<string, BasePrinter>> printers = new Dictionary<string, Dictionary<string, BasePrinter>>();

		/// <summary>
		/// Add <see cref="BasePrinter"/> to the storage.
		/// </summary>
		/// <param name="printer">The <see cref="BasePrinter"/>.</param>
		public void Add(BasePrinter printer)
		{
			if ((printer.Maker.Length == 0) || (printer.Model.Length == 0))
				return;

			if (this.Exists(printer))
				return;

			if (!this.printers.ContainsKey(printer.Maker))
			{
				Dictionary<string, BasePrinter> temp = new Dictionary<string, BasePrinter>();
				temp.Add(printer.Model, printer);

				this.printers.Add(printer.Maker, temp);
			} else
			{
				this.printers[printer.Maker].Add(printer.Model, printer);
			}
		}

		/// <summary>
		/// Check if <see cref="BasePrinter"/> is storaged.
		/// </summary>
		/// <param name="printer">The <see cref="BasePrinter"/>.</param>
		/// <returns><c>true</c> if yes. Otherwise, <c>false</c>.</returns>
		public bool Exists(BasePrinter printer)
		{
			if (this.printers.ContainsKey(printer.Maker))
			{
				return this.printers[printer.Maker].ContainsKey(printer.Model);
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
		public BasePrinter GetPrinter(string maker, string model)
		{
			return this.printers[maker][model];
		}
	}
}
