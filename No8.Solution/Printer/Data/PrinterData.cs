using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Printer.Data
{
	public struct PrinterData
	{
		/// <summary>
		/// Creator of the printer.
		/// </summary>
		public string Maker { get; set; }

		/// <summary>
		/// Model of the printer.
		/// </summary>
		public string Model { get; set; }

		public override string ToString()
		{
			return $"{this.Maker}.{this.Model}";
		}
	}
}
