using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	
	public partial class Parser
	{
		public List<Definition> Defs = new List<Definition>();
		private Stack<bool> IfScopes = new Stack<bool>();
		private List<string> importedHeaders = new List<string>();
		private bool isDef(string name)
		{
			foreach(Definition def in this.Defs)
			{
				if(def.Name == name)
					return true;
			}
			return false;
		}
		private List<Token> getDef(string name)
		{
			foreach(Definition def in this.Defs)
			{
				if(def.Name == name)
					return def.Value;
			}
			return null;
		}
		public List<Token> ParsePreprocessorDirectives()
		{
			this.index = 0;
			List<Token> NewTokens = new List<Token>();
			while(index < this.tokens.Count)
			{
				if (peekToken() is Tokens.Hash && peekToken(1).ToString() == "endif")
				{
					readToken();
					readToken();
					IfScopes.Pop();
				}
				else if(peekToken() is Tokens.Hash && peekToken(1) is Tokens.Statement && !IfScopes.Contains(false))
				{
					if(peekToken(1).ToString() == "define")
						ParseDefine();
					else if (peekToken(1).ToString() == "ifdef")
						ParseIfdef();
					else if (peekToken(1).ToString() == "include")
						ParseInclude();
					else if (peekToken(1).ToString() == "import")
						ParseImport();
					else if (peekToken(1).ToString() == "ifndef")
						ParseIfndef();
					else if (peekToken(1).ToString() == "error")
						ParseError();
					else if (!(peekToken() is Tokens.EOL))
					{
						NewTokens.Add(readToken());
					}
					else
						readToken();
					
				}
				if(isDef(peekToken().ToString()) && !IfScopes.Contains(false))
					NewTokens.AddRange(getDef(readToken().ToString()));
				else if (!(peekToken() is Tokens.EOL) && !IfScopes.Contains(false))
					NewTokens.Add(readToken());
			
				else
					readToken();
				
			}
			
			this.index = 0;
			return NewTokens;
		}
	}
}