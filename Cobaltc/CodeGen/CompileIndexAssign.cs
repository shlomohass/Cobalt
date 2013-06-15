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
		public void CompileGetIndex(GetAccessor Get)
		{
			VType t = GuessPtrType(Get.Var);
			if(t == VType.Unknown)
				Errors.Add(Get.Var + " does not exist in the current context!");
			else
			{
				if(t == VType.Int32)
				{
					CompileIntExpression(Get.Index);
					Assembler.Emit(new push_d(4));
					Assembler.Emit(new mul());
					GetIntValue(Get.Var);
					Assembler.Emit(new Viper.Opcodes.Add());
					Assembler.Emit(new dload());
				}
				else if (t == VType.Int8)
				{
					CompileIntExpression(Get.Index);
					GetIntValue(Get.Var);
					Assembler.Emit(new Viper.Opcodes.Add());
					Assembler.Emit(new bload());
				}
			}
		}
		public void CompileSetIndex(SetAccessor Set)
		{
			VType t = GuessPtrType(Set.Var);
			if(t == VType.Unknown)
				Errors.Add(Set.Var + " does not exist in the current context!");
			else
			{
				if(t == VType.Int32)
				{
					CompileIntExpression(Set.Value);
					CompileIntExpression(Set.Index);
					Assembler.Emit(new push_d(4));
					Assembler.Emit(new mul());
					GetIntValue(Set.Var);
					Assembler.Emit(new Viper.Opcodes.Add());
					Assembler.Emit(new dstore());
				}
				else if (t == VType.Int8)
				{
					CompileCharExpression(Set.Value);
					CompileIntExpression(Set.Index);
					GetIntValue(Set.Var);
					Assembler.Emit(new Viper.Opcodes.Add());
					Assembler.Emit(new bstore());
				}
				
				
			}
		}
	}
	
}