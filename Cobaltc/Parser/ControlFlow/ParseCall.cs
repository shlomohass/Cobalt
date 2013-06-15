using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
namespace Cobaltc
{
	public partial class Parser
	{
		public FunctionCall ParseCall()
		{
			string name = readToken().ToString();
			FunctionCall func_call = new FunctionCall(name);
			if(!(readToken() is Tokens.openParenthesis))
				throw new ParserException("Expected (");
			while(!(peekToken() is Tokens.closeParenthesis))
			{
				func_call.Arguments.Add(ParseExpression());
				if(!(peekToken() is Tokens.Comma) && !(peekToken() is Tokens.closeParenthesis))
					throw new ParserException("Expected , or )" + peekToken().ToString());
				else if (peekToken() is Tokens.Comma)
					readToken();
			}
			readToken();
			return func_call;
			
		}
	}
}

