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
		public void GetIntValue(SyntaxNode sn)
		{
			if(sn is IntLiteral)
					Assembler.Emit(new push_d((uint)((IntLiteral)sn).Value));
			else if (sn is StringLiteral)
				Assembler.Emit(new push_ptr(SymHelper.DeclareStringLiteral(((StringLiteral)sn).Value)));	
			else if (sn is Expression)
				CompileIntExpression(sn as Expression);
			else if (sn is GetAccessor)
				CompileGetIndex(sn as GetAccessor);
			else if (sn is PointerReference)
			{
				PointerReference ptr = sn as PointerReference;
				if(SymHelper[ptr.Symbol] != "")
				{
					if(SymHelper.isPointer(ptr.Symbol))
						Errors.Add("Can not reference pointer " + ptr.Symbol);
					else
						Assembler.Emit(new push_ptr(SymHelper[ptr.Symbol]));
				}
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
					Assembler.Emit(new convb_d());
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
		public void CompileIntExpression(Expression exp)
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
						if(boolean is BooleanAnd)
							Assembler.Emit(new and());
						else if (boolean is BooleanOr)
							Assembler.Emit(new bor());
					}
					else
					{
						if(GuessType(boolean.Op1) == VType.Int32)
						{
							CompileIntExpression(boolean.Op1);
							CompileIntExpression(boolean.Op2);
							if(boolean is BooleanEqu)
								Assembler.Emit(new teq());
							else if (boolean is NotEqual)
								Assembler.Emit(new tne());
							else if (boolean is LessThan)
								Assembler.Emit(new tlt());
							else if (boolean is GreaterThan)
								Assembler.Emit(new tgt());
						}
						else if(GuessType(boolean.Op1) == VType.Int8)
						{
							CompileCharExpression(boolean.Op1);
							Assembler.Emit(new convb_d());
							CompileCharExpression(boolean.Op2);
							Assembler.Emit(new convb_d());
							if(boolean is BooleanEqu)
								Assembler.Emit(new teq());
							else if (boolean is NotEqual)
								Assembler.Emit(new tne());
							else if (boolean is LessThan)
								Assembler.Emit(new tlt());
							else if (boolean is GreaterThan)
								Assembler.Emit(new tgt());
						}
					}
				}
				else if(sn is BinOp)
				{
					BinOp op = sn as BinOp;
					GetIntValue(op.Op1);
					GetIntValue(op.Op2);
					if(op is Add)
						Assembler.Emit(new Viper.Opcodes.Add());
					else if (op is Sub)
						Assembler.Emit(new Viper.Opcodes.sub());
					else if (op is Mul)
						Assembler.Emit(new mul());
					else if (op is Div)
						Assembler.Emit(new div());
					else if (op is Mod)
						Assembler.Emit(new mod());
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
			if(sn is IntLiteral)
			{
				Assembler.Emit(new push_b((byte)((IntLiteral)sn).Value));
			
			}	
			else if (sn is Expression)
				CompileCharExpression(sn as Expression);
			else if (sn is GetAccessor)
			{
				CompileGetIndex(sn as GetAccessor);
			}
			else if (sn is Cast)
			{
				Cast DynamicCast = sn as Cast;
				if(GuessType(DynamicCast.Data) == VType.Int32 || DynamicCast.Pointer)
				{
					GetIntValue(DynamicCast.Data);
					Assembler.Emit(new convd_b());
				}
				else if (GuessType(DynamicCast.Data) == VType.Int8)
				{
					GetCharValue(DynamicCast.Data);
				}
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
					else
					{
						Assembler.Emit(new push_ptr(SymHelper[Ref.Symbol]));
						Assembler.Emit(new bload());
					}
						
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
					Assembler.Emit(new convb_d());
					GetCharValue(op.Op2);
					Assembler.Emit(new convb_d());
					if(op is Add)
						Assembler.Emit(new Viper.Opcodes.Add());
					else if (op is Sub)
						Assembler.Emit(new Viper.Opcodes.sub());
					else if (op is Mul)
						Assembler.Emit(new mul());
					else if (op is Div)
						Assembler.Emit(new div());
					else if (op is Mod)
						Assembler.Emit(new mod());
					else
					{
						Errors.Add(op.ToString() + " can not be used on type int");
					}
					
					Assembler.Emit(new convd_b());
				}
				else
				{
					GetCharValue(sn);
				}
			}
		}
	}
	
}