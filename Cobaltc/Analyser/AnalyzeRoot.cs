using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class CodeAnalyzer
	{
		public void AnalyzeFile(List<SyntaxNode> tree)
		{
			foreach(SyntaxNode sn in tree)
			{
				if(sn is Method)
					AnalyzeMethod(sn as Method);
				else if (sn is IncludedFile)
				{
					IncludedFile include = sn as IncludedFile;
				
					AnalyzeFile(include.Code);
					
				}
				else
				{
					this.NewTree.Add(sn);
				}
				
			}
		}
	}
}

