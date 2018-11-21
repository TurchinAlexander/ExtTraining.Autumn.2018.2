﻿using System;
using System.IO;

namespace No8.Solution.Printer.Entities
{
	public class EpsonPrinter : BasePrinter
	{
		public EpsonPrinter(string model) : base("Epson", model) { }

		public override void Print(FileStream fs)
		{
			{
				for (int i = 0; i < fs.Length; i++)
				{
					Console.WriteLine("Epson " + fs.ReadByte());
				}
			}
		}
	}
}
