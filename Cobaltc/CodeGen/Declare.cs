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
		public void DeclareVar(Declaration decl)
		{
			
			if(getTypeFromName(decl.Type) == VType.Int32)
				SymHelper.DeclareInt32(decl.Name, decl.Pointer, decl.Constant);	
			else if (getTypeFromName(decl.Type) == VType.Int8)
				SymHelper.DeclareInt8(decl.Name, decl.Pointer, decl.Constant);
			else if (getTypeFromName(decl.Type) == VType.Void && decl.Pointer)
				SymHelper.DeclareVoidPtr(decl.Name, decl.Constant);
			else
			{
				Errors.Add(decl.Type + " does not exist in the current context!");
			}
			if(decl.Assign.Value.Value.Count > 0)
				Assign(decl.Assign, true);
		}
		
	}
}

