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
				return;
			SymHelper.BeginScope(meth.Name);
			foreach(Declaration decl in meth.Arguments)
				DeclareVar(decl);
			CompileBlock(meth.block);
			SymHelper.EndScope();
		}
	}
}

