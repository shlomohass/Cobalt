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
		
		public void CompileFile(List<SyntaxNode> tree)
		{
			SymHelper.BeginScope();
			foreach(SyntaxNode sn in tree)
			{
				if(sn is Method)
					CompileMethod(sn as Method);
				else if (sn is Declaration)
				{
					Declaration decl = sn as Declaration;
					if(decl.Static)
					{
						decl.Static = false;
						DeclareVar(decl, false);
					}
					else
						DeclareVar(sn as Declaration, false);	
				}
				else if (sn is IncludedFile)
				{
					IncludedFile include = sn as IncludedFile;
				
					CompileFile(include.Code);
					
				}
				else if (sn is Typedef)
				{
					Typedef td = sn as Typedef;
					SymHelper.DefineType(td.Typdef.Name,td.Typdef.Pointer, getTypeFromName(td.Typdef.Type));
				}
				else
				{
					Errors.Add("Unexpected " + sn.ToString());
				}
				
			}
			SymHelper.EndScope();
			
			if(this.Errors.Count > 0)
				throw new CodeGenException(this.Errors.Count.ToString() + " error(s) were found!");
		}
	}
}

