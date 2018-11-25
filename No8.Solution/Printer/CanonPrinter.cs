using System;
using System.IO;

namespace No8.Solution.Printer.Entities
{
	/// <summary>
	/// Represents a Canon printer
	/// </summary>
	public class CanonPrinter : BasePrinter
	{
		public CanonPrinter(string model) : base("Canon", model) { }

		protected override void PrintLogic(FileStream fs)
		{
			{
				for (int i = 0; i < fs.Length; i++)
				{
					Console.WriteLine("Canon " + fs.ReadByte());
				}
			}
		}
	}
}
