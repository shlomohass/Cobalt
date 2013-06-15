using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
namespace Cobaltc
{
	public partial class Parser
	{
		public SetAccessor ParseSetIndex(SyntaxNode val)
		{
			SetAccessor ret = new SetAccessor();
			ret.Var = val;
			if(!(readToken() is Tokens.openBracket))
				throw new ParserException("[ expected");
			ret.Index = ParseExpression();
			if(!(readToken() is Tokens.closeBracket))
				throw new ParserException("] expected!");
			if(!(readToken() is Tokens.Assign))
				throw new ParserException("= expected!");
			ret.Value = ParseExpression();
			return ret;
 		}
	}
}

