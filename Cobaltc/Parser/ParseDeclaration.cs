using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public Declaration ParseDeclaration(bool canHaveMultipleDeclarations = true)
		{
		
			bool signed =  false;
			bool Const = false;
			bool Static = false;
			while(isDeclarationAttribute())
			{
				if(peekToken().ToString() == "const")
				{
					readToken();
					Const = true;
				}
				if(peekToken().ToString() == "static")
				{
					readToken();
					Static = true;
				}
				if(peekToken().ToString() == "signed")
				{
					readToken();signed  = true;
				}
				else if (peekToken().ToString() == "unsigned")
					readToken();
			}
			if((peekToken() is Tokens.Statement && peekToken(1) is Tokens.Mul && peekToken(2) is Tokens.Statement))
			   return ParsePtrDeclaration();
			else if(!(peekToken() is Tokens.Statement && peekToken(1) is Tokens.Statement))
				throw new ParserException("Unexpected " + peekToken(1).ToString());
			else
			{
				string type = readToken().ToString();
				
				DeclarationGroup declg = new DeclarationGroup();
				do
				{
					
					Declaration decl = new Declaration();
					if(canHaveMultipleDeclarations && peekToken() is Tokens.Comma)
						readToken();
					string name = readToken().ToString();
					decl.Name = name;
					decl.Type = type;
					decl.Constant = Const;
					decl.Signed = signed;
					decl.Static = Static;
					if(peekToken() is Tokens.Assign)
					{
						readToken();
						Assignment asn = new Assignment();
						asn.Value= ParseExpression();
						asn.Var = name;
						decl.Assign = asn;
					}
					if(canHaveMultipleDeclarations)
						declg.Declarations.Add(decl);
					else
						return decl;
					
				} while(peekToken() is Tokens.Comma && canHaveMultipleDeclarations);
				return declg;
			}
		}
		public bool isDecaration()
		{
			if(peekToken().ToString() == "const" && peekToken(1) is Tokens.Statement)
				return true;
			else if(peekToken().ToString() == "static" && peekToken(1) is Tokens.Statement)
				return true;
			else if(peekToken().ToString() == "unsigned" || peekToken().ToString() == "signed")
				return true;
			if((peekToken() is Tokens.Statement && peekToken(1) is Tokens.Statement) || (peekToken() is Tokens.Statement && peekToken(1) is Tokens.Mul && peekToken(2) is Tokens.Statement))
				return true;
			else
				return false;
		}
		public bool isDeclarationAttribute()
		{
			string attr = peekToken().ToString();
			if(attr == "const" || attr == "unsigned" || attr == "signed" || attr == "static")
				return true;
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

