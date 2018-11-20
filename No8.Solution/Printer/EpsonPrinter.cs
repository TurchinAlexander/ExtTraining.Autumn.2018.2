using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Printer
{
	class EpsonPrinter : BasePrinter
	{
		public EpsonPrinter(string maker, string model) : base(maker, model) { }
	}
}
