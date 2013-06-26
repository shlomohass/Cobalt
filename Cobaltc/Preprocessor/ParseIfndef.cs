using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	
	public partial class Parser
	{
		private void ParseIfdef()
		{
			readToken();
			readToken();
			if(isDef(readToken().ToString()))
				IfScopes.Push(true);
			else
				IfScopes.Push(false);
		}
		private void ParseIfndef()
		{
			readToken();
			readToken();
			if(!isDef(readToken().ToString()))
				IfScopes.Push(true);
			else
				IfScopes.Push(false);
		}
	}
}