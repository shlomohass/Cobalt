using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		private bool checkForSemicolons = true;
		public void CheckSemi()
		{
			if(checkForSemicolons)
			{
				if(!(readToken() is Tokens.SemiColon))	
				{
					throw new ParserException("Expected ;");
				}
			}
		}
	}
}

