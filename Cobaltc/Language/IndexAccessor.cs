using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public abstract class IndexAccessor : SyntaxNode
	{
		public Expression Index;
		public SyntaxNode Var;
	}
	public class GetAccessor : IndexAccessor
	{
		
	}
	public class SetAccessor : IndexAccessor
	{
		public Expression Value;
	}
	
}