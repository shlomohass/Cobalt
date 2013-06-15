using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class Cast : SyntaxNode 
	{
		public string Type;
		public bool Pointer = false;
		public SyntaxNode Data;
	}
}

