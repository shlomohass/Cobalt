using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class CodeAnalyzer
	{
		public void AnalyzeMethod(Method meth)
		{
			this.AnalyzeBlock(meth.block);
		}
	}
}
