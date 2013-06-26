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
		private bool isInlineMethod = false;
		public void Call(FunctionCall func)
		{
			
			if(methodExists(func.Target))
			{
				func.Arguments.Reverse();
				foreach(Expression exp in func.Arguments)
				{
					if(GuessType(exp) == VType.Int32)
						CompileIntExpression(exp);
					else if (GuessType(exp) == VType.Int8)
						CompileCharExpression(exp);
					else if (GuessType(exp) == VType.String)
					{
						CompileStringExpression(exp);
					}
				}
				func.Arguments.Reverse();
				if(!getMethod(func.Target).Inline)
					Assembler.Emit(new bsr(func.Target));
				else
				{
					bool inline = isInlineMethod;
					isInlineMethod = true;
					Method inlineMethod = getMethod(func.Target);
					List<Scope> root = SymHelper.gotoRootScope();
					SymHelper.BeginScope();
					foreach(Declaration decl in inlineMethod.Arguments)
					{
						DeclareVar(decl);
						Assembler.Emit(new push_ptr(SymHelper[decl.Name]));
						if(getTypeFromName(decl.Type) == VType.Int32 || decl.Pointer)
							Assembler.Emit(new dstore());
						else if (getTypeFromName(decl.Type) == VType.Int8)
							Assembler.Emit(new bstore());
					}
					CompileBlock(inlineMethod.block);
					SymHelper.EndScope();
					SymHelper.RestoreScope(root);
					isInlineMethod = inline;
				}
			}
			else
				Errors.Add(func.Target + " does not exist in the current context!");
		}
	}
}

