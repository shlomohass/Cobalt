using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public abstract class BooleanOp : SyntaxNode 
	{ 
		public Expression Op1;
		public Expression Op2;
	}
	public abstract class BinOp : SyntaxNode
	{
		public SyntaxNode Op1;
		public SyntaxNode Op2;
	}
	public class Add : BinOp {};
	public class Sub : BinOp {};
	public class Mul : BinOp {};
	public class Div : BinOp {};
	public class Mod : BinOp {};
	public class BooleanEqu : BooleanOp {};
	public class GreaterThan : BooleanOp {};
	public class LessThan : BooleanOp {};
	public class NotEqual : BooleanOp {};
	public abstract class LogicalOp : BooleanOp {};
	public class BooleanAnd : LogicalOp {};
	public class BooleanOr : LogicalOp {};
	
}

