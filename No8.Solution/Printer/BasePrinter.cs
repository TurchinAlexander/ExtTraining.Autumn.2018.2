using System;
using System.IO;

namespace No8.Solution.Printer
{
	public class BasePrinter : IEquatable<BasePrinter>
	{
		public BasePrinter(string maker, string model)
		{
			if (maker == null)
				throw new ArgumentNullException($"{nameof(maker)} cannot be null.");

			if (model == null)
				throw new ArgumentNullException($"{nameof(maker)} cannot be null.");

			this.Maker = maker;
			this.Model = model;
		}

		public string Maker { get; private set; }

		public string Model { get; private set; }

		public virtual void Print(FileStream fs)
		{
			for (int i = 0; i < fs.Length; i++)
			{
				Console.WriteLine(fs.ReadByte());
			}
		}

		public bool Equals(BasePrinter other)
		{
			return (this.Name.ToUpper().Equals(other.Name.ToUpper())) &&
				(this.Model.ToUpper().Equals(other.Model.ToUpper()));
		}

		public override bool Equals(object obj)
		{
			if (obj is BasePrinter)
			{
				return this.Equals(obj as BasePrinter);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return (this.Name.ToUpper() + this.Model.ToUpper()).GetHashCode();
		}
	}
}
