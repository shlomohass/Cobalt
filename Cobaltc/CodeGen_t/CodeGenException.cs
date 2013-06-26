using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
using Viper;
using Viper.Opcodes;

namespace Cobaltc
{
	public class CodeGenException : Exception
	{
		public string Error;
		public CodeGenException(string msg)
		{
			this.Error = msg;
		}
	}
}