using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public IfStatement ParseIfStatement()
		{
			IfStatement ret = new IfStatement();
			readToken();
			readToken();
			ret.Compare = ParseExpression();
			if(!(readToken() is Tokens.closeParenthesis))
				throw new ParserException("Expected )");
			ret.IfBlock = ParseBlock();
			if(peekToken().ToString() == "else")
			{
				readToken();
				
				Block elseBlock = new Block();
				if(peekToken() is Tokens.openCurlyBracket)
				{
					elseBlock = ParseBlock();
				}
				else if (peekToken().ToString() == "if")
				{
					elseBlock.Body.Add(ParseIfStatement());
					
				}
				else
					throw new ParserException("Unexpected " + readToken().ToString());
				ret.ElseBlock = elseBlock;
			}
			return ret;
		}
	}
}

