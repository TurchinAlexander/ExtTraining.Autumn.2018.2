using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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

				switch (key.Key)
				{
					case ConsoleKey.D1:
						CreatePrinter(manager);
						break;

					case ConsoleKey.D2:
                        BasePrinter printer = ChoosePrinter(manager);
                        if (printer == null)
                        {
                            Console.WriteLine("Invalid input");
                            break;
                        }
                        
                        FileStream fs = OpenFile();
                        if (fs == null)
                        {
                            Console.WriteLine("Invalid input");
                            break;
                        }
                        manager.Print(printer, fs);
						
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
			string maker;
			string model;

			Console.Write("Enter your printer's maker: ");
			maker = Console.ReadLine();
			Console.Write("Enter your printer's model: ");
			model = Console.ReadLine();

            BasePrinter printer = PrinterFactory.Create(maker, model);
            manager.Add(printer);
		}

		private static BasePrinter ChoosePrinter(PrinterManager manager)
		{
			Console.WriteLine("Makers:");
            BasePrinter[] allPrinters = manager.GetAllPrinters();
            string[] makers = allPrinters
                .Select(p => p.Maker)
                .Distinct()
                .ToArray();

			string maker = GetString(makers);
            string[] models = allPrinters
                .Where(p => p.Maker.Equals(maker))
                .Select(p => p.Model)
                .ToArray();

			Console.WriteLine("Printers:");
			string model = GetString(models);

            BasePrinter result = allPrinters
                .Where(p => p.Maker.Equals(maker) && p.Model.Equals(model))
                .First();

			return result;
		}

		private static string GetString(string[] results)
		{
			PrintStrings(results);

			Console.Write("Choose: ");
			string strNumber = Console.ReadLine();

            if (strNumber.Length == 0)
            {
                return null;
            }

            if ((!uint.TryParse(strNumber, out uint index) && (index < results.Length)))
            {
                return null;
            }

            return results[index];
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

        private static FileStream OpenFile()
        {
            var o = new OpenFileDialog();
            o.ShowDialog();
            if (string.IsNullOrEmpty(o.FileName))
            {
                return null;
            }

            return File.OpenRead(o.FileName);
        }
	}
}
