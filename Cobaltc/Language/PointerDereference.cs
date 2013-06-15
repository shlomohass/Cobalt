using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class PointerDereference : SyntaxNode
	{
		public SyntaxNode PtrRef;
		public PointerDereference ()
		{
		}
		public PointerDereference (SyntaxNode ptr)
		{
			this.PtrRef = ptr;
		}
	}
	public class PointerDereferenceAssign : SyntaxNode
	{
		public SyntaxNode PtrRef;
		public Expression Value;
		public PointerDereferenceAssign (SyntaxNode ptr)
		{
			this.PtrRef = ptr;
		}
	}
}

