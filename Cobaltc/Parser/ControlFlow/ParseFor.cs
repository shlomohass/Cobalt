using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public ForStatement ParseForStatement()
		{
			ForStatement forstat = new ForStatement();
			readToken();
			if(!(readToken() is Tokens.openParenthesis))
				throw new ParserException("Expected (");
			forstat.Declaration = ParseLine();
			forstat.Compare = ParseExpression();
			CheckSemi();
			forstat.Step = ParseAssignment();
			if(!(readToken() is Tokens.closeParenthesis))
				throw new ParserException(") expected!");
			forstat.Body = ParseBlock();
			return forstat;
			
		}
	}
}

