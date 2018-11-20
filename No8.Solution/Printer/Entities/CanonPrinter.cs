using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Printer.Entities
{
	/// <summary>
	/// Represents a Canon printer
	/// </summary>
	public class CanonPrinter : BasePrinter
	{
		public CanonPrinter(string model) : base("Canon", model) { }
	}
}
