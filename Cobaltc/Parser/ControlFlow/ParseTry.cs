using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public TryStatement ParseTry()
		{
			TryStatement ret = new TryStatement();
			readToken();
			ret.TryBlock = ParseBlock();
			if(peekToken().ToString() != "catch")
				return ret;
	
			if(peekToken() is Tokens.openParenthesis)
			{
				readToken();
				ret.ExceptionInfo = ParseDeclaration();
				if(!(readToken() is Tokens.closeParenthesis))
				   throw new ParserException(") Expected!");
			}
	
			ret.CatchBlock = ParseBlock();
			return ret;
		}
	}
}

