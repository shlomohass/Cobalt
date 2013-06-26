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
		public void DeclareVar(Declaration decl, bool Global = false)
		{
			
			if(getTypeFromName(decl.Type) == VType.Int32)
				SymHelper.DeclareInt32(decl.Name, Global, decl.Pointer, decl.Constant);	
			else if (getTypeFromName(decl.Type) == VType.Int8)
				SymHelper.DeclareInt8(decl.Name,  Global,decl.Pointer, decl.Constant);
			else if (getTypeFromName(decl.Type) == VType.Void && decl.Pointer)
				SymHelper.DeclareVoidPtr(decl.Name, Global,decl.Constant);
			else
			{
				Errors.Add(decl.Type + " does not exist in the current context!");
			}
			if(decl.Assign.Value.Value.Count > 0)
			{
				if(decl.Static)
				{
					if(getTypeFromName(decl.Type) == VType.Int32)
					{
						Assembler.Emit(new push_ptr(decl.Name));	
						Assembler.Emit(new dload());
					}
					else if(getTypeFromName(decl.Type) == VType.Int8)
					{
						Assembler.Emit(new push_ptr(decl.Name));	
						Assembler.Emit(new bload());
						Assembler.Emit(new convb_d());
					}
					string tmpLabel = "_endStaticCheck" + IfIndex.ToString();
					IfIndex++;
					Assembler.Emit(new bnz(tmpLabel));
					Assign(decl.Assign, true);
					Assembler.CreateLabel(tmpLabel);
					
				}
				else
					Assign(decl.Assign, true);
			}
		}
		
	}
}

