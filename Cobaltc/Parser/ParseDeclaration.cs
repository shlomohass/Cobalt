using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public Declaration ParseDeclaration()
		{
			bool signed =  false;
			bool Const = false;
			if(peekToken().ToString() == "const")
			{
				readToken();
				Const = true;
			}
			if(peekToken().ToString() == "signed")
			{
				readToken();signed  = true;
			}
			else if (peekToken().ToString() == "unsigned")
				readToken();
			if((peekToken() is Tokens.Statement && peekToken(1) is Tokens.Mul && peekToken(2) is Tokens.Statement))
			   return ParsePtrDeclaration();
			else if(!(peekToken() is Tokens.Statement && peekToken(1) is Tokens.Statement))
				throw new ParserException("Unexpected " + peekToken(1).ToString());
			else
			{
				string type = readToken().ToString();
				string name = readToken().ToString();
				Declaration decl = new Declaration();
				decl.Name = name;
				decl.Type = type;
				decl.Constant = Const;
				decl.Signed = signed;
				if(peekToken() is Tokens.Assign)
				{
					readToken();
					Assignment asn = new Assignment();
					asn.Value= ParseExpression();
					asn.Var = name;
					decl.Assign = asn;
				}
				return decl;
			}
		}
		public bool isDecaration()
		{
			if(peekToken().ToString() == "const" && peekToken(1) is Tokens.Statement)
				return true;
			else if(peekToken().ToString() == "unsigned" || peekToken().ToString() == "signed")
				return true;
			if((peekToken() is Tokens.Statement && peekToken(1) is Tokens.Statement) || (peekToken() is Tokens.Statement && peekToken(1) is Tokens.Mul && peekToken(2) is Tokens.Statement))
				return true;
			else
				return false;
		}
		public Declaration ParsePtrDeclaration()
		{
			bool signed =  false;
			bool Const = false;
			if(peekToken().ToString() == "const")
			{
				readToken();
				Const = true;
			}
			if(peekToken().ToString() == "signed")
			{
				readToken();signed  = true;
			}
			else if (peekToken().ToString() == "unsigned")
				readToken();
			if(!(peekToken() is Tokens.Statement && peekToken(1) is Tokens.Mul && peekToken(2) is Tokens.Statement))
				throw new ParserException("Unexpected " + peekToken(1).ToString());
			else
			{
				string type = readToken().ToString();
				readToken();
				string name = readToken().ToString();
				Declaration decl = new Declaration();
				decl.Name = name;
				decl.Type = type;
				decl.Constant = Const;
				decl.Pointer = true;
				decl.Signed = signed;
				if(peekToken() is Tokens.Assign)
				{
					readToken();
					Assignment asn = new Assignment();
					asn.Value= ParseExpression();
					asn.Var = name;
					decl.Assign = asn;
				}
				return decl;
			}
		}
	}
}

