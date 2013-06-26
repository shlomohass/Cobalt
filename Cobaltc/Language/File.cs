using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class IncludedFile : SyntaxNode
	{
		public string Name;
		public IncludedFile(List<SyntaxNode> tree)
		{
			this.Code = tree;
		}
		public List<SyntaxNode> Code = new List<SyntaxNode>();
	}
}

