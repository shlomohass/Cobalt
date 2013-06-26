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
		private static int IfIndex = 0;
		public void CompileIf(IfStatement ifstat)
		{
			CompileIntExpression(ifstat.Compare);
			CompileBlock(ifstat.IfBlock);
			CompileBlock(ifstat.ElseBlock);
		}
		
	}
}

