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
		public void CompileReturn(Return rtn)
		{
			if(rtn.Value.Value.Count > 0)
			{
				if(this.returnType == VType.Int32 || this.ReturnPtr)
					CompileIntExpression(rtn.Value);
				else if (this.returnType == VType.Int8)
					CompileCharExpression(rtn.Value);
			}
			if(!isInlineMethod)
				Assembler.Emit(new ret());			
		}
		
	}
}

