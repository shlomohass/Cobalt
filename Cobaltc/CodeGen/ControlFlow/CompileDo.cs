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
		public void CompileDo(DoStatement whilestat)
		{
			string label = "_do_" + IfIndex.ToString();
			IfIndex++;
			Assembler.CreateLabel(label);;
			CompileBlock(whilestat.Body);
			CompileIntExpression(whilestat.Compare);
			Assembler.Emit(new bnz(label));
		}
		
	}
}

