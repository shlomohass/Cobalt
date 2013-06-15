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
		public void CompileLine(SyntaxNode sn)
		{
			if(sn is Declaration)
				DeclareVar(sn as Declaration);
			else if (sn is Return)
				CompileReturn(sn as Return);
			else if (sn is Assignment)
				Assign(sn as Assignment);
			else if (sn is FunctionCall)
			{
				FunctionCall FC = sn as FunctionCall;
				if(getMethod(FC.Target) == null)
					Errors.Add(FC.Target + " does not exist in the current context!");
				else
				{
					Call(FC);
					if(getMethod(FC.Target).ReturnsPtr || getReturnType(FC.Target) == VType.Int32)
						Assembler.Emit(new pop_d());
					else if (getReturnType(FC.Target) == VType.Int8)
						Assembler.Emit(new pop_b());
						
						
				}
				
			}
			else if (sn is DoStatement) 
				CompileDo(sn as DoStatement);
			else if (sn is SetAccessor)
				CompileSetIndex(sn as SetAccessor);
			else if (sn is IfStatement)
				CompileIf(sn as IfStatement);
			else if (sn is ForStatement)
				CompileFor(sn as ForStatement);
			else if (sn is WhileStatement)
				CompileWhile(sn as WhileStatement);
			else if (sn is PointerDereferenceAssign)
				AssignPointerDereference(sn as PointerDereferenceAssign);
			else if (sn is InlineIL)
			{
				InlineIL il = sn as InlineIL;
				foreach(Scope sc in SymHelper.Scopes)
				{
					foreach(Variable v in sc.Variables)
					{
						il.IL = il.IL.Replace("%" + v.Name, v.RealName);
					}
				}
				vasm.MainClass.Complile(il.IL,ref Assembler);
			}
		}
	}
}

