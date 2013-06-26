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
		public VType GuessType(SyntaxNode sn)
		{
			if(sn is IntLiteral)
				return VType.Int32;
			else if (sn is StringLiteral)
				return VType.String;
			else if (sn is Expression)
				return GuessType(sn as Expression);
			else if (sn is BinOp)
				return GuessType(((BinOp)sn).Op1);
			else if (sn is GetAccessor)
				return GuessPtrType(((GetAccessor)sn).Var);
			else if (sn is SymbolReference)
			{
				if(SymHelper.isPointer(((SymbolReference)sn).Symbol))
					return VType.Int32;
				return SymHelper.getType(((SymbolReference)sn).Symbol);
			}
			else if (sn is PointerReference)
				return VType.Int32;
			else if (sn is FunctionCall)
			{
				FunctionCall fc = sn as FunctionCall;
				if(getMethod(fc.Target).ReturnsPtr)
					return VType.Int32;
				return getReturnType(fc.Target);
			}
			return VType.Unknown;
		}
		public VType GuessPtrType(SyntaxNode sn)
		{
			if(sn is IntLiteral)
				return VType.Int32;
			else if (sn is StringLiteral)
				return VType.String;
			else if (sn is Expression)
				return GuessPtrType(sn as Expression);
			else if (sn is BinOp)
				return GuessPtrType(((BinOp)sn).Op1);
			else if (sn is GetAccessor)
				return GuessPtrType(((GetAccessor)sn).Var);
			else if (sn is SymbolReference)
			{
				return SymHelper.getType(((SymbolReference)sn).Symbol);
			}
			else if (sn is Cast)
			{
				Cast cst = sn as Cast;
				return getTypeFromName(cst.Type);
			}
			else if (sn is FunctionCall)
			{
				FunctionCall fc = sn as FunctionCall;
				return getReturnType(fc.Target);
			}
			return VType.Unknown;
		}
		public VType GuessPtrType(Expression exp)
		{
			foreach(SyntaxNode sn in exp.Value)
			{
				return GuessPtrType(sn);
			}
			return VType.Unknown;
		}
		public VType GuessType(Expression exp)
		{
			foreach(SyntaxNode sn in exp.Value)
			{
				return GuessType(sn);
			}
			return VType.Unknown;
		}
		public Method getMethod(string name)
		{
			foreach(SyntaxNode sn in this.parseTree)
			{
				if(sn is Method)
				{
					Method m = sn as Method;
					if(m.Name == name)
						return m;
				}
				else if (sn is IncludedFile)
				{
					if(getMethod(name, ((IncludedFile)sn).Code) != null)
						return getMethod(name, ((IncludedFile)sn).Code);
				} 
			}
			return null;
			
		}
		public Method getMethod(string name, List<SyntaxNode> tree)
		{
			foreach(SyntaxNode sn in tree)
			{
				if(sn is Method)
				{
					Method m = sn as Method;
					if(m.Name == name)
						return m;
				}
				else if (sn is IncludedFile)
				{
					if(getMethod(name, ((IncludedFile)sn).Code) != null)
						return getMethod(name, ((IncludedFile)sn).Code);
				}
			}
			return null;
			
		}
		public VType getReturnType(string name)
		{
			if(methodExists(name))
			{
				return getTypeFromName(getMethod(name).ReturnType);
			}
			else
			{
				return VType.Unknown;
			}
			
		}
		public bool methodExists(string name)
		{
			if(getMethod(name) != null)
				return true;
			else
				return false;
		}
		public VType getTypeFromName(string name)
		{
			if(name == "string")
				return VType.String;
			else if (name == "int")
				return VType.Int32;
			else if (name == "char")
				return VType.Int8;
			else if (name == "void")
				return VType.Void;
			else if (SymHelper.isType(name))
				return SymHelper.getTypeDef(name);
			else
				return VType.Unknown;
		}
		
	}
	
}