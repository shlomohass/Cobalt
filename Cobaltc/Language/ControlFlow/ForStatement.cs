using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class ForStatement : SyntaxNode
	{
		public Expression Compare = new Expression();
		public Assignment Step;
		public SyntaxNode Declaration;
		public Block Body = new Block();
	}
}

