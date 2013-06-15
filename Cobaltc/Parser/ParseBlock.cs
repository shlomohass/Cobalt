using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public Block ParseBlock()
		{
			Block blk = new Block();
			if(!(peekToken() is Tokens.openCurlyBracket))
				blk.Body.Add(ParseLine());
			else
			{
				readToken();
				while(!(peekToken() is Tokens.closeCurlyBracket))
				{
					blk.Body.Add(ParseLine());
				}
				readToken();
				
			}
			return blk;
		}
	}
}

