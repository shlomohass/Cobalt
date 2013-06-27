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
			this.ReturnPtr = meth.ReturnsPtr;
			if(meth.ReturnType != "void")
			{
				returnType = getTypeFromName(meth.ReturnType);
				
				if(returnType == VType.Unknown)
					Errors.Add("Unknown type " + meth.ReturnType); 
			}
			if(meth.External || meth.Inline)
			{
				Assembler.CreateExternalSymbol(meth.Name);
				return;
			}
			
			Assembler.CreateGlobalLabel(meth.Name);
			SymHelper.BeginScope(meth.Name);
			SymbolHelper.LocalIndex = 0;
			foreach(Declaration decl in meth.Arguments)
			{
				DeclareVar(decl, true);
				if(getTypeFromName(decl.Type) == VType.Int32 || decl.Pointer || getTypeFromName(decl.Type) ==VType.String)
					Assembler.Emit(new stloc_d(SymHelper.getIndex(decl.Name)));
				else if (getTypeFromName(decl.Type) == VType.Int8)
					Assembler.Emit(new stloc_b(SymHelper.getIndex(decl.Name)));
			}
			CompileBlock(meth.block);
			SymHelper.EndScope();
			Assembler.Emit(new ret());	
		}
	}
}

