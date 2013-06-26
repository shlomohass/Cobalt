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
		public void GetStringValue(SyntaxNode sn)
		{
			if(sn is StringLiteral)
				Assembler.Emit(new push_ptr(SymHelper.DeclareStringLiteral(((StringLiteral)sn).Value)));
			
			else if (sn is Expression)
				CompileStringExpression(sn as Expression);
			else if (sn is FunctionCall)
			{
				FunctionCall func = sn as FunctionCall;
				if(getMethod(func.Target).ReturnType == "char" && getMethod(func.Target).ReturnsPtr)
				{
					Call(func);
				}
				else
				{
					Errors.Add(func.Target + " does not return type  char ptr!");
				}
			}
			else if (sn is SymbolReference)
			{
				SymbolReference Ref = sn as SymbolReference;
				if(SymHelper[Ref.Symbol] != "")
				{
					if(SymHelper.getType(Ref.Symbol) != VType.String)
						Errors.Add(Ref.Symbol + " can not be converted to string!");
					else
					{
						Assembler.Emit(new push_ptr(SymHelper[Ref.Symbol]));
						Assembler.Emit(new dload());
					}
						
				}
				else
					Errors.Add(Ref.Symbol + " does not exist!");
			}
			else
				Errors.Add("Unexpected " + sn.ToString());
		}
		public void CompileStringExpression(Expression exp)
		{
			foreach(SyntaxNode sn in exp.Value)
			{
				GetStringValue(sn);
				
			}
		}
	}
	
}