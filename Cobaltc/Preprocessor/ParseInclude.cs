using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	
	public partial class Parser
	{
		private void ParseImport()
		{
			readToken();
			readToken();
			if(peekToken() is Tokens.StringLiteral)
			{
				Tokens.StringLiteral sl = readToken() as Tokens.StringLiteral;
				if(!importedHeaders.Contains(sl.Value))
				{
					IncludeFile( sl.Value);
				}
			}
		}
		private void ParseInclude()
		{
			readToken();
			readToken();
			if(peekToken() is Tokens.StringLiteral)
			{
				Tokens.StringLiteral sl = readToken() as Tokens.StringLiteral;
				IncludeFile(sl.Value);
				
			}
			else if (peekToken() is Tokens.LessThan)
			{
				readToken();
				StringBuilder accum = new StringBuilder();
				while(!(peekToken() is Tokens.GreaterThan))
				{
					accum.Append(readToken().ToString());
				}
				IncludeFile(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/include/" + accum.ToString());
				readToken();
			}
		}
	}
}