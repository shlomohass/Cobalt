using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	
	public partial class Parser
	{
		private void ParseError()
		{
			readToken();
			readToken();
			if(peekToken() is Tokens.StringLiteral)
			{
				Tokens.StringLiteral sl = readToken() as Tokens.StringLiteral;
				this.ParserErrors.Add(sl.Value);
			}
			else
			{
				this.ParserErrors.Add("String literal expected after #error directive!");
			}
		}
	}
}