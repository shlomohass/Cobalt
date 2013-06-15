using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public DoStatement ParseDoStatement()
		{
			DoStatement ret = new DoStatement();
			readToken();
			ret.Body = ParseBlock();
			if(readToken().ToString() != "while")
				throw new ParserException("While Expected!");
			if(!(readToken() is Tokens.openParenthesis))
				throw new ParserException("( Expected!");
			ret.Compare = ParseExpression();
			if(!(readToken() is Tokens.closeParenthesis))
				throw new ParserException("Expected )");
			
			return ret;
		}
	}
}

