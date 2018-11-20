using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No8.Solution.Logger.Interface
{
	/// <summary>
	/// Interface to logger.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Logging information.
		/// </summary>
		/// <param name="s"></param>
		void Log(string s);
	}
}
