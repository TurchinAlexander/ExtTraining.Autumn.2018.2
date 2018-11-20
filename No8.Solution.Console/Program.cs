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

				switch(key.Key)
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
				printer = new BasePrinter(maker, model);
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
			int indexMaker = -1;
			int indexModel = -1;

			Console.WriteLine("Makers:");
			string[] makers = manager.ShowPrinterMaker();
			PrintStrings(makers);

			Console.Write("Choose maker:");
			if (!CheckInput(makers, out indexMaker))
				return false;


			Console.WriteLine("Printers:");
			string[] models = manager.ShowPrinters(makers[indexMaker]);
			PrintStrings(models);

			Console.Write("Choose printer:");
			if (!CheckInput(models, out indexModel))
				return false;

			printerData = new PrinterData() { Maker = makers[indexMaker], Model = models[indexModel] };
			return true;
		}

		/// <summary>
		/// Check user input.
		/// </summary>
		/// <param name="array">Array of <see cref="string"/>.</param>
		/// <param name="index">Out index.</param>
		/// <returns><c>true</c>, if user was correct. Otherwise, <c>false</c>.</returns>
		private static bool CheckInput(string[] array, out int index)
		{
			string strNumber = Console.ReadLine();

			if ((strNumber.Length == 0) ||
				((!int.TryParse(strNumber, out index) && (index > -1) && (index < array.Length))))
			{
				Console.WriteLine("Invalid index.");
				index = -1;
				return false;
			}
			return true;
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
