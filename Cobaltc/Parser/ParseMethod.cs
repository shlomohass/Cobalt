using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		private bool isMethodAttribute()
		{
			if(peekToken().ToString() == "inline" || peekToken().ToString() == "static")
				return true;
			else
				return false;
		}
		
		public Method ParseMethod(bool inline = false, bool external = false)
		{
			bool RetPtr = false;
			string Type = readToken().ToString();
			if(peekToken() is Tokens.Mul)
			{
				readToken();
				RetPtr = true;
			}
			string name = readToken().ToString();
			Method ret = new Method(name,Type);
			ret.ReturnsPtr = RetPtr;
			if(!(readToken() is Tokens.openParenthesis))
			   throw new ParserException("( Expected!");
			ret.Inline = inline;
			while(!(peekToken() is Tokens.closeParenthesis))
			{
				ret.Arguments.Add(ParseDeclaration(false));
				if (peekToken() is Tokens.Comma)
					readToken();
				else if(!(peekToken() is Tokens.closeParenthesis))
				{
					throw new ParserException("Unexpected " + peekToken().ToString());
				}
			}
			readToken();
			if(external)
			{
				ret.External = true;
				return ret;
			}
			ret.block = ParseBlock();
			return ret;
		}
	}
}

