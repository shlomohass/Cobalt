using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public WhileStatement ParseWhileStatement()
		{
			WhileStatement ret = new WhileStatement();
			readToken();
			readToken();
			ret.Compare = ParseExpression();
			if(!(readToken() is Tokens.closeParenthesis))
				throw new ParserException("Expected )");
			ret.Body = ParseBlock();
			return ret;
		}
	}
}

