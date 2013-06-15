using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
using Viper;
using Viper.Opcodes;

namespace Cobaltc
{
	public class Variable
	{
		public string Name;
		public string RealName;
		public VType Type;
		public bool Constant = false;
		public bool Pointer = false;
	}
	public enum VType
	{
		
		Int32 = 0,
		Int8 = 1,
		String = 2,
		Unknown = 3,
		Void = 4,
	}
	public class Scope
	{
		public string Name = "";
		public List<UserDefinedType> Typedefs = new List<UserDefinedType>();
		public List<Variable> Variables = new List<Variable>();
	}
	public class UserDefinedType
	{
		public string Name;
		public VType Type;
	}
	public class SymbolHelper
	{
		private Assembly Assembler;
		private int ScopeIndex = 0;
		private int StringIndex = 0;
		public Stack<Scope> Scopes = new Stack<Scope>();
		public void DefineType(string t, bool Ptr, VType Type)
		{
			UserDefinedType td = new UserDefinedType();
			td.Name = t;
			td.Type = Type;
			td.Name = t;
			Scopes.Peek().Typedefs.Add(td);
		}
		public SymbolHelper(ref Assembly asm)
		{
			this.Assembler = asm;
		}
		public void BeginScope()
		{
			BeginScope(ScopeIndex.ToString());
			ScopeIndex++;
		}
		public void BeginScope(string nom)
		{
			Scope sc = new Scope();
			sc.Name = nom;
			Scopes.Push(sc);
		}
		public void EndScope()
		{
			Scopes.Pop();
		}
		private string GetScopePrefix()
		{
			StringBuilder ret = new StringBuilder("");
			foreach(Scope sc in this.Scopes)
			{
				ret.Append(sc.Name);
				ret.Append("_");
			}
			return ret.ToString();
		}
		public void DeclareVoidPtr(string name, bool Const = false)
		{
			Variable Int = new Variable();
			Int.Name = name;
			Int.RealName = GetScopePrefix() + name;
			Int.Type = VType.Void;
			Int.Pointer = true;
			Int.Constant = Const;
			Scopes.Peek().Variables.Add(Int);
			Assembler.CreateBuffer(Int.RealName,4);
		}
		public void DeclareInt32(string name, bool ptr = false, bool Const = false)
		{
			Variable Int = new Variable();
			Int.Name = name;
			Int.RealName = GetScopePrefix() + name;
			Int.Type = VType.Int32;
			Int.Pointer = ptr;
			Int.Constant = Const;
			Scopes.Peek().Variables.Add(Int);
			Assembler.CreateBuffer(Int.RealName,4);
		}
		public void DeclareInt8(string name, bool ptr = false, bool Const = false)
		{
			Variable Int = new Variable();
			Int.Name = name;
			Int.RealName = GetScopePrefix() + name;
			Int.Type = VType.Int8;
			Int.Pointer = ptr;
			Int.Constant = Const;
			Scopes.Peek().Variables.Add(Int);
			if(!ptr)
				Assembler.CreateBuffer(Int.RealName,1);
			else
				Assembler.CreateBuffer(Int.RealName,4);
		}
		public string DeclareStringLiteral(string val)
		{
			Assembler.CreateString("_stringLiteral_" + StringIndex.ToString(),val + "\0");
			string ret = "_stringLiteral_" + StringIndex.ToString();
			StringIndex++;
			return ret;
		}
		public string this [string name]
		{
			get
			{
				foreach(Scope sc in this.Scopes)
				{
					foreach(Variable v in sc.Variables)
					{
						if(v.Name == name)
							return v.RealName;
					}
				}
				return "";
			}
		}
		public VType getType(string Name)
		{
			foreach(Scope sc in this.Scopes)
			{
				foreach(Variable v in sc.Variables)
				{
					if(v.Name == Name)
						return v.Type;
				}
			}
			return VType.Unknown;
		}
		public bool isPointer(string Name)
		{
			foreach(Scope sc in this.Scopes)
			{
				foreach(Variable v in sc.Variables)
				{
					if(v.Name == Name)
						return v.Pointer;
				}
			}
			return false;
		}
		public bool isConstant(string Name)
		{
			foreach(Scope sc in this.Scopes)
			{
				foreach(Variable v in sc.Variables)
				{
					if(v.Name == Name)
						return v.Constant;
				}
			}
			return false;
		}
		public bool isType(string t)
		{
			foreach(Scope sc in this.Scopes)
			{
				foreach(UserDefinedType typedef in sc.Typedefs)
				{
					if(typedef.Name == t)
						return true;
				}
			}
			return false;
		}
		public VType getTypeDef(string t)
		{
			foreach(Scope sc in this.Scopes)
			{
				foreach(UserDefinedType typedef in sc.Typedefs)
				{
					if(typedef.Name == t)
						return typedef.Type;
				}
			}
			return VType.Unknown;
		} 
		public void DeclareString(string name)
		{
			Variable Str = new Variable();
			Str.Name = name;
			Str.RealName = GetScopePrefix() + name;
			Str.Type = VType.String;
			Scopes.Peek().Variables.Add(Str);
			Assembler.CreateBuffer(Str.RealName,4); 
		}
	}
}

