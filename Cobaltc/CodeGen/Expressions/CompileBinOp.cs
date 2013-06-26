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
		public void CompileBinOp(BinOp op, bool Signed = false)
		{
			if (op is Ternary)
			{
				GetIntValue(op.Op1);
				Assembler.Emit(new bz ("_tern_val_" + IfIndex.ToString()));
				GetIntValue(op.Op2);
				Assembler.Emit(new bra("_end_tern_" + IfIndex.ToString()));
				Assembler.CreateLabel("_tern_val_" + IfIndex.ToString());
				GetIntValue(((Ternary)op).DefaultValue);
				Assembler.CreateLabel("_end_tern_" + IfIndex.ToString());
				IfIndex++;
			}
			else
			{
				GetIntValue(op.Op1);
				GetIntValue(op.Op2);
				if(op is Add)
					Assembler.Emit(new Viper.Opcodes.Add());
				else if (op is Sub)
					Assembler.Emit(new Viper.Opcodes.sub());
				else if (op is Mul && !Signed)
					Assembler.Emit(new mul());
				else if (op is Mul)
					Assembler.Emit(new mul_s());
				else if (op is Div && !Signed)
					Assembler.Emit(new div());
				else if (op is Div)
					Assembler.Emit(new div_s());
				else if (op is Mod)
					Assembler.Emit(new mod());
				else if (op is Bor)
					Assembler.Emit(new bor());
				else if (op is And)
					Assembler.Emit(new and());
				else if (op is Shl)
					Assembler.Emit(new shl());
				else if (op is Shr)
					Assembler.Emit(new shr());
				else
				{
					Errors.Add(op.ToString() + " can not be used on type int");
				}
			}
		}
	}
}

