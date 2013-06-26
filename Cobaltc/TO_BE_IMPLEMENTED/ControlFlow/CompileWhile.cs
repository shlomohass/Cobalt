using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
using Viper;
using Viper.Opcodes;

namespace Cobaltc
{
	public partial class CodeAnalyser
	{
		public void CompileWhile(WhileStatement whilestat)
		{
			CompileIntExpression(whilestat.Compare);
			CompileBlock(whilestat.Body);
		}
		
	}
}

