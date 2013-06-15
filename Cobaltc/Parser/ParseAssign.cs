using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public Assignment ParseAssignment()
		{
			Assignment asn = new Assignment();
			asn.Var = readToken().ToString();
			if(peekToken() is Tokens.Increment)
			{
				readToken();
				Add ad = new Add();
				ad.Op1 = new SymbolReference(asn.Var);
				ad.Op2 = new IntLiteral(1);
				Expression exp = new Expression();
				exp.Value.Add(ad);
				asn.Value = exp;
				return asn;
			}
			else if(peekToken() is Tokens.Decrement)
			{
				readToken();
				Sub ad = new Sub();
				ad.Op1 = new SymbolReference(asn.Var);
				ad.Op2 = new IntLiteral(1);
				Expression exp = new Expression();
				exp.Value.Add(ad);
				asn.Value = exp;
				return asn;
			}
			readToken();
			asn.Value = ParseExpression();
			return asn;
		}
	}
}


