using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using No8.Solution.Logger;
using No8.Solution.Printer;

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
				Console.WriteLine("2:Choose an existing printer.");
				Console.WriteLine("3:Print by choosed printer");
				Console.WriteLine("4:Exit.");

				key = Console.ReadKey();


				switch(key.Key)
				{
					case ConsoleKey.D1:
						CreatePrinter(manager);
						break;

					case ConsoleKey.D2:
						ChooseExistingPrinter(manager);
						break;

					case ConsoleKey.D3:
						manager.Print();
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

		private static void ChooseExistingPrinter(PrinterManager manager)
		{
			string strNumber;
			int indexMaker;
			int indexPrinter;

			Console.WriteLine("Makers:");
			string[] makers = manager.ShowPrinterMaker();
			PrintStrings(makers);

			Console.Write("Choose maker:");
			strNumber = Console.ReadLine();

			if ((!int.TryParse(strNumber, out indexMaker) && (indexMaker > -1) && (indexMaker < makers.Length)))
			{
				Console.WriteLine("Invalid index.");
				return;
			}

			Console.WriteLine("Printers:");
			string[] printersByMaker = manager.ShowPrinters(makers[indexMaker]);
			PrintStrings(printersByMaker);

			Console.Write("Choose printer:");
			strNumber = Console.ReadLine();

			if (!int.TryParse(strNumber, out indexPrinter) && (indexPrinter > -1) && (indexPrinter < printersByMaker.Length))
			{
				Console.WriteLine("Invalid index.");
				return;
			}

			manager.ChoosePrinter(makers[indexMaker], printersByMaker[indexPrinter]);
		}

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
