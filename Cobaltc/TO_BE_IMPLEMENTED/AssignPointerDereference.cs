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
		public void AssignPointerDereference(PointerDereferenceAssign pda)
		{
			if(GuessPtrType(pda.PtrRef) == VType.Int32)
			{
				CompileIntExpression(pda.Value);
				GetIntValue(pda.PtrRef);
			}
			else if (GuessPtrType(pda.PtrRef) == VType.Int8)
			{
				CompileCharExpression(pda.Value);
				GetIntValue(pda.PtrRef);
			}
		}
	}
}

