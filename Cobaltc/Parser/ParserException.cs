using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class ParserException : Exception
	{
		public string Error;
		public ParserException(string msg)
		{
			this.Error = msg;
		}
	}
}

