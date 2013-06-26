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
		public void CompileBlock(Block blck)
		{
			foreach(SyntaxNode sn in blck.Body)
			{
				if(sn is Return)
				{
					CompileLine(sn);
					return;
				}
				else
					CompileLine(sn);
			}
		}
	}
}

