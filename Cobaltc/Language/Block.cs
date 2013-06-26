using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class Block : SyntaxNode 
	{
		public Block()
		{
		}
		public Block(List<SyntaxNode> sn)
		{
			this.Body = sn;
		}
		public List<SyntaxNode> Body = new List<SyntaxNode>();
	}
}

