using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using No8.Solution.Printer.Data;
using No8.Solution.Printer.Entities;
using No8.Solution.Logger.Entities;

namespace No8.Solution.WithConsole
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			string logTxt = "log.txt";
			PrinterManager manager = new PrinterManager(new FileLogger(logTxt));
			manager.Add(new CanonPrinter("xls-2000"));
			manager.Add(new CanonPrinter("jubr-x"));
			manager.Add(new EpsonPrinter("eps-w2"));

			ConsoleKeyInfo key;
			bool isOut = false;

			do
			{
				Console.WriteLine("Select your choice:");
				Console.WriteLine("1:Add new printer");
				Console.WriteLine("2:Print by choosed printer");
				Console.WriteLine("3:Exit.");

				key = Console.ReadKey();
				PrinterData printerData;

				switch (key.Key)
				{
					case ConsoleKey.D1:
						CreatePrinter(manager);
						break;

					case ConsoleKey.D2:
						if (TryChoosePrinter(manager, ref printerData))
						{
							manager.Print(printerData);
						}
						break;

					default:
						isOut = true;
						break;
				}
				Console.WriteLine();
			}
			while (!isOut);
		}

		private static void CreatePrinter(PrinterManager manager)
		{
			BasePrinter printer;
			string maker;
			string model;

			Console.Write("Enter your printer's maker: ");
			maker = Console.ReadLine();
			Console.Write("Enter your printer's model: ");
			model = Console.ReadLine();

			if (maker == "Canon")
			{
				printer = new CanonPrinter(model);
			}
			else if (maker == "Epson")
			{
				printer = new EpsonPrinter(model);
			}
			else
			{
				printer = new PlugPrinter(maker, model);
			}

			manager.Add(printer);
		}

		/// <summary>
		/// Choose printer.
		/// </summary>
		/// <param name="manager">Class, which contains printers.</param>
		/// <param name="printerData">Information about our chosed printer.</param>
		/// <returns><c>true</c>, if we found. Otherwise, <c>false</c>.</returns>
		private static bool TryChoosePrinter(PrinterManager manager, ref PrinterData printerData)
		{
			Console.WriteLine("Makers:");
			string maker = GetString(manager.ShowPrinterMaker());

			Console.WriteLine("Printers:");
			string model = GetString(manager.ShowPrinters(maker));

			printerData = new PrinterData() { Maker = maker, Model = model };
			return true;
		}

		private static string GetString(string[] results)
		{
			PrintStrings(results);

			Console.Write("Choose: ");
			string strNumber = Console.ReadLine();

			if ((strNumber.Length == 0) ||
				((!int.TryParse(strNumber, out int index) && (index > -1) && (index < results.Length))))
			{
				Console.WriteLine("Invalid index.");
				index = -1;
				return null;
			}
			return results[index];
		}

		/// <summary>
		/// Print string to the console.
		/// </summary>
		/// <param name="array">Array of <see cref="string"/>.</param>
		private static void PrintStrings(string[] array)
		{
			if (array.Length == 0)
			{
				Console.WriteLine("Nothing in the storage.");
				return;
			}

			for (int i = 0; i < array.Length; i++)
			{
				Console.WriteLine($"  {i}.{array[i]}");
			}
		}
	}
}
