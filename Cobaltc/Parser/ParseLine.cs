using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public SyntaxNode ParseLine()
		{
			
			if (peekToken().ToString() == "return")
			{
				readToken();
				if(peekToken() is Tokens.SemiColon)
					return new Return();
				else
				{
					Return ret = new Return();
					ret.Value = ParseExpression();
					CheckSemi();
					return ret;
				}
			}
			else if (peekToken().ToString() == "asm" && peekToken(1) is Tokens.openParenthesis)
			{
				readToken();
				readToken();
				InlineIL IL = new InlineIL();
				IL.IL = ((Tokens.StringLiteral)readToken()).Value;
				if(!(readToken() is Tokens.closeParenthesis))
					throw new ParserException(") expected!");
				CheckSemi();
				return IL;
			}
			else if(isDecaration())
			{
				Declaration decl = ParseDeclaration();
				CheckSemi();
				return decl;
			}
			else if (peekToken() is Tokens.Statement && peekToken(1) is Tokens.Increment)
			{
				Assignment asn = ParseAssignment();
				CheckSemi();
				return asn;
			}
			else if (peekToken().ToString() == "if")
			{
				return ParseIfStatement();
			}
			else if (peekToken().ToString() == "for")
			{
				return ParseForStatement();
			}
			else if (peekToken().ToString() == "while")
			{
				return ParseWhileStatement();
			}
			else if (peekToken().ToString() == "do")
			{
				return ParseDoStatement();
			}
			else if (peekToken() is Tokens.Statement && peekToken(1) is Tokens.Assign)
			{
				Assignment asn = ParseAssignment();
				CheckSemi();
				return asn;
			}
			else if (peekToken() is Tokens.Statement && peekToken(1) is Tokens.openParenthesis)
			{
				FunctionCall func = ParseCall();
				CheckSemi();
				return func;
			}
			else if (peekToken() is Tokens.Mul)
			{
				readToken();
				PointerDereferenceAssign pda = new PointerDereferenceAssign(getNode());
				if(!(readToken() is Tokens.Assign))
					throw new ParserException("= expected!");
				pda.Value = ParseExpression();
				CheckSemi();
				return pda;
			}
			else
			{
				int indx = index;
				SyntaxNode attempt = getNode(false);
				if(peekToken() is Tokens.openBracket)
				{
					SetAccessor ret = ParseSetIndex(attempt);
					CheckSemi();
					return ret;
				}
				this.index = indx;
				throw new ParserException("Unexpected " + peekToken().ToString());
			}
		}
	}
}

