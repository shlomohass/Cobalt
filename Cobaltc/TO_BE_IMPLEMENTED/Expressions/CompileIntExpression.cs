using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
using Viper;
using Viper.Opcodes;

namespace Cobaltc
{
	public partial class CodeAnalyser
	{
		public void GetIntValue(SyntaxNode sn)
		{
			if(sn is IntLiteral || sn is StringLiteral)
			{
			}
			else if (sn is Expression)
				CompileIntExpression(sn as Expression);
			else if (sn is GetAccessor)
				CompileGetIndex(sn as GetAccessor);
			else if (sn is PointerReference)
			{
				PointerReference ptr = sn as PointerReference;
				if(SymHelper[ptr.Symbol] != "")
					if(SymHelper.isPointer(ptr.Symbol))
						Errors.Add("Can not reference pointer " + ptr.Symbol);
				else
				{
					Errors.Add(ptr.Symbol + " does not exist in the current context!");
				}
			}
			else if (sn is Cast)
			{
				Cast DynamicCast = sn as Cast;
				if(GuessType(DynamicCast.Data) == VType.Int32 || DynamicCast.Pointer)
					GetIntValue(DynamicCast.Data);
				else if (GuessType(DynamicCast.Data) == VType.Int8)
				{
					GetCharValue(DynamicCast.Data);
				}
			}
			else if (sn is FunctionCall)
			{
				FunctionCall func = sn as FunctionCall;
				if(getMethod(func.Target).ReturnType == "int" || getMethod(func.Target).ReturnsPtr)
				{
					Call(func);
				}
				else
				{
					Errors.Add(func.Target + " does not return type int!");
				}
			}
			else if (sn is SymbolReference)
			{
				SymbolReference Ref = sn as SymbolReference;
				if(SymHelper[Ref.Symbol] != "")
				{
					
					if(SymHelper.getType(Ref.Symbol) != VType.Int32 && !SymHelper.isPointer(Ref.Symbol))
						Errors.Add(Ref.Symbol + " can not be converted to int!");
						
				}
				else
					Errors.Add(Ref.Symbol + " does not exist!");
			}
			else
				Errors.Add("Unexpected " + sn.ToString());
		}
		
		public void CompileIntExpression(Expression exp, bool Signed = false)
		{
			foreach(SyntaxNode sn in exp.Value)
			{
				if(sn is BooleanOp)
				{
					BooleanOp boolean = sn as BooleanOp;
					if(boolean is LogicalOp)
					{
						CompileIntExpression(boolean.Op1);
						CompileIntExpression(boolean.Op2);
					}
					else
					{
						if(GuessType(boolean.Op1) == VType.Int32)
						{
							CompileIntExpression(boolean.Op1);
							CompileIntExpression(boolean.Op2);
						}
						else if(GuessType(boolean.Op1) == VType.Int8)
						{
							CompileCharExpression(boolean.Op1);
							CompileCharExpression(boolean.Op2);
						}
					}
				}
				else if(sn is BinOp)
				{
					BinOp op = sn as BinOp;
					GetIntValue(op.Op1);
					GetIntValue(op.Op2);
					
					if(op is Add)
					{}
					else if (op is Sub)
					{}
					else if (op is Mul && !Signed)
					{}
					else if (op is Mul)
					{}
					else if (op is Div && !Signed)
					{}
					else if (op is Div)
					{}
					else if (op is Mod)
					{}
				
					else
					{
						Errors.Add(op.ToString() + " can not be used on type int");
					}
				}
				else
				{
					GetIntValue(sn);
				}
			}
		}
		public void GetCharValue(SyntaxNode sn)
		{
			if(!(sn is IntLiteral))
				CompileCharExpression(sn as Expression);
			else if (sn is GetAccessor)
			{
				CompileGetIndex(sn as GetAccessor);
			}
			else if (sn is Cast)
			{
				Cast DynamicCast = sn as Cast;
				if(GuessType(DynamicCast.Data) == VType.Int32 || DynamicCast.Pointer)
					GetIntValue(DynamicCast.Data);
				else if (GuessType(DynamicCast.Data) == VType.Int8)
					GetCharValue(DynamicCast.Data);
			}
			else if (sn is FunctionCall)
			{
				FunctionCall func = sn as FunctionCall;
				if(getMethod(func.Target).ReturnType == "char" && !getMethod(func.Target).ReturnsPtr)
				{
					Call(func);
				}
				else
				{
					Errors.Add(func.Target + " does not return type char!");
				}
			}
			else if (sn is SymbolReference)
			{
				SymbolReference Ref = sn as SymbolReference;
				if(SymHelper[Ref.Symbol] != "")
				{
					if(SymHelper.getType(Ref.Symbol) != VType.Int8)
						Errors.Add(Ref.Symbol + " can not be converted to int!");
				}
				else
					Errors.Add(Ref.Symbol + " does not exist!");
			}
			else
				Errors.Add("Unexpected " + sn.ToString());
		}
		public void CompileCharExpression(Expression exp)
		{
			foreach(SyntaxNode sn in exp.Value)
			{
				if(sn is BinOp)
				{
					BinOp op = sn as BinOp;
					GetCharValue(op.Op1);
					GetCharValue(op.Op2);
					if(op is Add)
					{}
					else if (op is Sub)
					{}
					else if (op is Mul)
					{}
					else if (op is Div)
					{}
					else if (op is Mod)
					{}
					else
					{
						Errors.Add(op.ToString() + " can not be used on type int");
					}
					
				}
				else
				{
					GetCharValue(sn);
				}
			}
		}
	}
	
}