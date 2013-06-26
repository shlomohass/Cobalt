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
		public void CompileWhile(WhileStatement whilestat)
		{
			string label = "_while_" + IfIndex.ToString();
			string end_label = "_endwhile_" + IfIndex.ToString();
			IfIndex++;
			Assembler.CreateLabel(label);
			CompileIntExpression(whilestat.Compare);
			Assembler.Emit(new bz(end_label));
			CompileBlock(whilestat.Body);
			Assembler.Emit(new bra(label));
			Assembler.CreateLabel(end_label);
		}
		
	}
}

