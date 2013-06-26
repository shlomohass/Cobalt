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
		public void CompileFor(ForStatement forstat)
		{
			string label = "_for_" + IfIndex.ToString();
			string end_label = "_endfor_" + IfIndex.ToString();
			IfIndex++;
			CompileLine(forstat.Declaration);
			Assembler.CreateLabel(label);
			CompileIntExpression(forstat.Compare);
			Assembler.Emit(new bz(end_label));
			CompileBlock(forstat.Body);
			Assign(forstat.Step);
			Assembler.Emit(new bra(label));
			Assembler.CreateLabel(end_label);
			
		}
		
	}
}

