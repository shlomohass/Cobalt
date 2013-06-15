using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class IfStatement : SyntaxNode
	{
		public Expression Compare = new Expression();
		public Block IfBlock = new Block();
		public Block ElseBlock = new Block();
	}
}

