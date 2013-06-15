using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class PointerReference : SyntaxNode
	{
		public string Symbol;
		public PointerReference()
		{
		}
		public PointerReference(string str)
		{
			this.Symbol = str;
		}
	}
}

