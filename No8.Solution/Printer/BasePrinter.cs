using System;
using System.IO;

namespace No8.Solution.Printer.Entities
{
	/// <summary>
	/// Base class for all printers.
	/// </summary>
	public abstract class BasePrinter
	{
        public string Maker { get; private set; }
        public string Model { get; private set; }

        public event EventHandler<string> StartPrint = delegate { };
        public event EventHandler<string> EndPrint = delegate { };

        /// <summary>
        /// Creation of <see cref="BasePrinter"/>.
        /// </summary>
        /// <param name="maker">The printer's name.</param>
        /// <param name="model">The printer's model.</param>
        /// <exception cref="ArgumentNullException">if <paramref name="maker"/> or <paramref name="model"/> is null.</exception>
        public BasePrinter(string maker, string model)
		{
			if (maker == null)
				throw new ArgumentNullException($"{nameof(maker)} cannot be null.");
			if (model == null)
				throw new ArgumentNullException($"{nameof(maker)} cannot be null.");

            this.Maker = maker;
            this.Model = model;
		}

		/// <summary>
		/// Method to print some information.
		/// </summary>
		/// <param name="fs">Input stream of data.</param>
		public void Print(FileStream fs)
        {
            StartPrint(this, $"{this} start printing");

            PrintLogic(fs);

            EndPrint(this, $"{this} end printing");
        }

        protected abstract void PrintLogic(FileStream fs);

		public override string ToString()
		{
            return $"{this.Maker}.{this.Model}";
		}
	}
}
