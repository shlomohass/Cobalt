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
		public void CompileTry(TryStatement trystat)
		{
			string label = "_catch_" + IfIndex.ToString();
			string end_label = "_end_try_" + IfIndex.ToString();
			Assembler.Emit(new begin_fault(label));
			CompileBlock(trystat.TryBlock);
			Assembler.Emit(new end_fault());
			Assembler.Emit(new bra(end_label));
			Assembler.CreateLabel(label);
			if(trystat.ExceptionInfo != null)
			{
				if(getTypeFromName(trystat.ExceptionInfo.Type) != VType.Int32)
					Errors.Add(trystat.ExceptionInfo.Type + " can not be used with catch!");
				else
				{
					
					DeclareVar(trystat.ExceptionInfo, true);
					Assembler.Emit(new push_ptr(this.SymHelper[trystat.ExceptionInfo.Name]));
					Assembler.Emit(new dstore());
				}
			}
			else
				Assembler.Emit(new pop_d());
			CompileBlock(trystat.CatchBlock);
			Assembler.CreateLabel(end_label);

		}
		
	}
}

