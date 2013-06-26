using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class Declaration : SyntaxNode
	{
		public string Name;
		public bool Signed = false;
		public bool Constant = false;
		public bool Pointer = false;
		public bool Static = false;
		public string Type;
		public Assignment Assign = new Assignment();
	}
	public class DeclarationGroup : Declaration
	{
		public List<Declaration> Declarations = new List<Declaration>();
	}
	public class Assignment : SyntaxNode
	{
		public string Var;
		public Expression Value = new Expression();
	}
}

