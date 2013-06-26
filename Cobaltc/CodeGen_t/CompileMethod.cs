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
		private VType returnType;
		private bool ReturnPtr = false;
		public void CompileMethod(Method meth)
		{
			if(meth.ReturnType != "void")
			{
				returnType = getTypeFromName(meth.ReturnType);
				this.ReturnPtr = meth.ReturnsPtr;
				if(returnType == VType.Unknown)
					Errors.Add("Unknown type " + meth.ReturnType); 
			}
			if(meth.External)
			{
				Assembler.CreateExternalSymbol(meth.Name);
				return;
			}
			
			Assembler.CreateGlobalLabel(meth.Name);
			SymHelper.BeginScope(meth.Name);
			foreach(Declaration decl in meth.Arguments)
			{
				DeclareVar(decl);
				Assembler.Emit(new push_ptr(SymHelper[decl.Name]));
				if(getTypeFromName(decl.Type) == VType.Int32 || decl.Pointer)
					Assembler.Emit(new dstore());
				else if (getTypeFromName(decl.Type) == VType.Int8)
					Assembler.Emit(new bstore());
			}
			CompileBlock(meth.block);
			SymHelper.EndScope();
			Assembler.Emit(new ret());	
		}
	}
}

