using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class Loop : SyntaxNode
	{
		public Expression Compare = new Expression();
		public Block Body = new Block();
	}
}

