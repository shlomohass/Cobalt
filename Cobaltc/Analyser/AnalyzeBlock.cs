using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class CodeAnalyzer
	{
		public void AnalyzeBlock(Block bk)
		{
			foreach(SyntaxNode sn in bk.Body)
				AnalyzeLine(sn);
		}
	}
}

