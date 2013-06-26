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
		public void CompileTry(TryStatement trystat)
		{

			CompileBlock(trystat.TryBlock);
			if(trystat.ExceptionInfo != null)
			{
				if(getTypeFromName(trystat.ExceptionInfo.Type) != VType.Int32)
					Errors.Add(trystat.ExceptionInfo.Type + " can not be used with catch!");
				else
				{
					
					DeclareVar(trystat.ExceptionInfo);
				}
			}
			CompileBlock(trystat.CatchBlock);

		}
		
	}
}

