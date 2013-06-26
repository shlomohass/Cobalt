using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class CodeAnalyzer
	{
		public void AnalyzeLine(SyntaxNode sn)
		{
			if(sn is Loop)
			{
				Loop lp = new Loop();
				this.NewTree.Add(lp);
				AnalyzeBlock(lp.Body);
			}
			this.NewTree.Add(sn);
		}
	}
}

