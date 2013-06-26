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
		public void CompileFor(ForStatement forstat)
		{
			CompileLine(forstat.Declaration);
			CompileIntExpression(forstat.Compare);
			CompileBlock(forstat.Body);
			Assign(forstat.Step);
			
		}
		
	}
}

