using System;
using System.IO;

namespace No8.Solution.Printer.Entities
{
	public class PlugPrinter : BasePrinter
	{
		public PlugPrinter(string maker, string model) : base(maker, model) { }

		public override void Print(FileStream fs)
		{
			{
				for (int i = 0; i < fs.Length; i++)
				{
					Console.WriteLine("Plug " + fs.ReadByte());
				}
			}
		}
	}
}
