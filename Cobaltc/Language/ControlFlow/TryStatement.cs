using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class TryStatement : SyntaxNode
	{
		public Declaration ExceptionInfo;
		public Block TryBlock = new Block();
		public Block CatchBlock = new Block();
		
	}
}

