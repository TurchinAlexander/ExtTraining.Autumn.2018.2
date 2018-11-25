using System;

using No8.Solution.Printer.Entities;

namespace No8.Solution.WithConsole
{
    public static class PrinterFactory
    {
        public static BasePrinter Create(string maker, string model)
        {
            if (maker.Equals("Canon"))
                return new CanonPrinter(model);
            else if (maker.Equals("Epson"))
                return new EpsonPrinter(maker);
            else
                throw new NotImplementedException($"{nameof(maker)} is not implemented");
        }
    }
}
