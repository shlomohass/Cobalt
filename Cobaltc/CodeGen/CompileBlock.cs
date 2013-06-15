using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
using Viper;
using Viper.Opcodes;

namespace Cobaltc
{
	public partial class CodeGen
	{
		public void CompileBlock(Block blck)
		{
			foreach(SyntaxNode sn in blck.Body)
			{
				CompileLine(sn);
			}
		}
	}
}

