using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
namespace Cobaltc
{
	public partial class Parser
	{
		private Lexer Scanner;
		private List<Token> tokens = new List<Token>();
		private int index = 0;
		public List<SyntaxNode> ParseTree;
		private Token peekToken(int indx)
		{
			if(indx + index > tokens.Count)
				return new Tokens.Statement();
			else
				return tokens[indx + index];
		}
		private Token peekToken()
		{
			return peekToken(0);
		}
		private Token readToken()
		{
			index++;
			if(index > tokens.Count)
			{
				throw new ParserException("Unexpected end of file!");
			}
			else
			{
				return tokens[index - 1];
			}
		}
		public void BeginParse(string source)
		{
			this.ParseTree = new List<SyntaxNode>();
			Scanner = new Lexer();
			Scanner.ScanWhiteSpaces = true;
			Scanner.Scan(new StringReader(source));
			this.tokens = Scanner.tokens;
			this.tokens = ParsePreprocessorDirectives();
			while(index < tokens.Count)
			{
				if(peekToken().ToString() == "typedef")
				{
					readToken();
					Typedef td = new Typedef();
					td.Typdef = ParseDeclaration();
					CheckSemi();
					ParseTree.Add(td);
				}
				else if(peekToken() is Tokens.Statement && peekToken(1) is Tokens.Statement && peekToken(2) is Tokens.openParenthesis)
				{
					ParseTree.Add(ParseMethod());
				}
				else if(peekToken() is Tokens.Statement && peekToken(1) is Tokens.Mul && peekToken(2) is Tokens.Statement && peekToken(3) is Tokens.openParenthesis)
				{
					ParseTree.Add(ParseMethod());
				}
				else if (peekToken().ToString() == "extern")
				{
					readToken();
					ParseTree.Add(ParseMethod(true));
					CheckSemi();
				}
				else if (isDecaration())
				{
					ParseTree.Add(ParseDeclaration());
					CheckSemi();
				}
	
				else	
				{
					throw new ParserException("Parse encountered unexpected " + readToken().ToString());
				}
			}
		}
	}
}

