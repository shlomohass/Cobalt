using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class FunctionCall : SyntaxNode
	{
		public string Target;
		public List<Expression> Arguments = new List<Expression>();
		public FunctionCall()
		{
		}
		public FunctionCall(string targ)
		{
			this.Target = targ;
		}
	}
}

