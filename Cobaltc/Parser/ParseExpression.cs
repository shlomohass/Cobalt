using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public partial class Parser
	{
		public SyntaxNode getNode(bool checkForArrays = true)
		{
			SyntaxNode ret = null;
			if(peekToken() is Tokens.IntLiteral)
			{
				ret = new IntLiteral(((Tokens.IntLiteral)readToken()).Value); 
			}
			else if (peekToken() is Tokens.StringLiteral)
			{
				ret = new StringLiteral(((Tokens.StringLiteral)readToken()).Value);
				
			}
			else if (peekToken() is Tokens.Tilda)
			{
				readToken();
				Not nt = new Not();
				nt.Op = getNode(checkForArrays);
				ret = nt;
			}
			else if (peekToken() is Tokens.Dereference)
			{
				PointerReference ptr = new PointerReference();
				Tokens.Dereference Ref = readToken() as Tokens.Dereference;
				ptr.Symbol = Ref.Name;
				ret = ptr;
			}
			else if (peekToken() is Tokens.openParenthesis && peekToken(1) is Tokens.Statement && peekToken(2) is Tokens.closeParenthesis)
			{
				readToken();
				string t = readToken().ToString();
				readToken();
				Cast cast = new Cast();
				cast.Type = t;
				cast.Data = getNode();
				ret = cast;
			}
			else if (peekToken() is Tokens.openParenthesis && peekToken(1) is Tokens.Statement && peekToken(2) is Tokens.Mul && peekToken(3) is Tokens.closeParenthesis)
			{
				readToken();
				string t = readToken().ToString();
				readToken();
				readToken();
				Cast cast = new Cast();
				cast.Pointer = true;
				cast.Type = t;
				cast.Data = getNode();
				ret = cast;
			}
			else if (peekToken() is Tokens.Statement && peekToken(1) is Tokens.openParenthesis)
			{
				ret = ParseCall();
			}
			else if (peekToken() is Tokens.Statement)
			{
				ret = new SymbolReference(readToken().ToString());
			}
			else if (peekToken() is Tokens.openParenthesis)
			{
				readToken();
				Expression exp = ParseExpression();
				if(!(readToken() is Tokens.closeParenthesis))
					throw new ParserException("Expected )");
				ret =  exp;
			}
			else
			{
				return null;
			}
			if (peekToken() is Tokens.openBracket &&  checkForArrays )
			{
				GetAccessor Get =new GetAccessor();
				Get.Var = ret;
				readToken();
				Get.Index = ParseExpression();
				if(!(readToken() is Tokens.closeBracket))
					throw new ParserException("] Expected!");
				return Get;
			}
			else
				return ret;
		}
		public Expression ParseBooleanExpression(Expression InitVal)
		{
			if(peekToken() is Tokens.BooleanAnd)
			{
				readToken();
				BooleanAnd AND = new BooleanAnd();
				AND.Op1 = InitVal;
				AND.Op2 = ParseExpression(true);
				Expression nxp = new Expression();
				nxp.Value.Add(AND);
				return nxp;
			}
			else if(peekToken() is Tokens.BooleanOr)
			{
				readToken();
				BooleanOr OR = new BooleanOr();
				OR.Op1 = InitVal;
				OR.Op2 = ParseExpression(true);
				Expression nxp = new Expression();
				nxp.Value.Add(OR);
				return nxp;
			}
			else
				throw new ParserException("Unexpected " + peekToken().ToString());
		}
		public Expression ParseExpression(bool firstTime = true)
		{
			Expression exp = new Expression();
			while(true)
			{
				if((peekToken() is Tokens.BooleanAnd || peekToken() is Tokens.BooleanOr )&& firstTime)
				{
					return ParseBooleanExpression(exp);
				}
				else if(peekToken() is Tokens.Equal)
				{
					BooleanEqu equ = new BooleanEqu();
					readToken();
					equ.Op1 = exp;
					equ.Op2 = ParseExpression(false);
					exp = new Expression();
					exp.Value.Add(equ);
				}
				else if(peekToken() is Tokens.NotEqual)
				{
					NotEqual equ = new NotEqual();
					readToken();
					equ.Op1 = exp;
					equ.Op2 = ParseExpression(false);
					exp = new Expression();
					exp.Value.Add(equ);
				}
				else if(peekToken() is Tokens.GreaterThan)
				{
					GreaterThan equ = new GreaterThan();
					readToken();
					equ.Op1 = exp;
					equ.Op2 = ParseExpression(false);
					exp = new Expression();
					exp.Value.Add(equ);
				}
				else if(peekToken() is Tokens.LessThan)
				{
					LessThan equ = new LessThan();
					readToken();
					equ.Op1 = exp;
					equ.Op2 = ParseExpression(false);
					exp = new Expression();
					exp.Value.Add(equ);
				}
				else
				{
					SyntaxNode n1 = getNode();
					if(n1 != null)
					{
						if(peekToken() is Tokens.Add)
						{
							readToken();
							Add addition = new Add();
							addition.Op1 = n1;
							addition.Op2 = getNode();
							exp.Value.Add(addition);
						}
						else if(peekToken() is Tokens.Sub)
						{
							readToken();
							Sub sub = new Sub();
							sub.Op1 = n1;
							sub.Op2 = getNode();
							exp.Value.Add(sub);
						}
						else if (peekToken() is Tokens.Div)
						{
							readToken();
							Div div = new Div();
							div.Op1 = n1;
							div.Op2 = getNode();
							exp.Value.Add(div);
						}
						else if (peekToken() is Tokens.Percent)
						{
							readToken();
							Mod div = new Mod();
							div.Op1 = n1;
							div.Op2 = getNode();
							exp.Value.Add(div);
						}
						else if (peekToken() is Tokens.Mul)
						{
							readToken();
							Mul mul = new Mul();
							mul.Op1 = n1;
							mul.Op2 = getNode();
							exp.Value.Add(mul);
						}
						else if (peekToken() is Tokens.And)
						{
							readToken();
							And band = new And();
							band.Op1 = n1;
							band.Op2 = getNode();
							exp.Value.Add(band);
						}
						else if (peekToken() is Tokens.Pipe)
						{
							readToken();
							Bor or = new Bor();
							or.Op1 = n1;
							or.Op2 = getNode();
							exp.Value.Add(or);
						}
						else if (peekToken() is Tokens.LeftBitshift)
						{
							readToken();
							Shl sl = new Shl();
							sl.Op1 = n1;
							sl.Op2 = getNode();
							exp.Value.Add(sl);
						}
						else if (peekToken() is Tokens.RightBitshift)
						{
							readToken();
							Shr sr = new Shr();
							sr.Op1 = n1;
							sr.Op2 = getNode();
							exp.Value.Add(sr);
						}
						else if (peekToken() is Tokens.Question)
						{
							readToken();
							Ternary tern = new Ternary();
							tern.Op1 = n1;
							tern.Op2 = getNode();
							if(!(readToken() is Tokens.Colon))
								throw new ParserException(": Expected!");
							else	
							{
								tern.DefaultValue = getNode();
								exp.Value.Add(tern);
							}
						}
						else
						{
							exp.Value.Add(n1);
						}
					}
					else
					{
						if(peekToken() is Tokens.BooleanAnd && firstTime)
							return ParseBooleanExpression(exp);
						return exp;
					}
				}
			}
		}
	}
}

