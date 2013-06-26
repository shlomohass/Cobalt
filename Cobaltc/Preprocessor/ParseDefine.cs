using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	
	public partial class Parser
	{
		private void ParseDefine()
		{
			readToken();
			readToken();
			Definition def = new Definition();
			def.Name = readToken().ToString();
			while(!(peekToken() is Tokens.EOL))
				def.Value.Add(readToken());
			Defs.Add(def);
		}
	}
}