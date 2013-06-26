using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	public class Definition
	{
		public string Name;
		public List<Token> Value = new List<Token>();
	}
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
		private void IncludeFile(string file)
		{
			Parser pars = new Parser();
			pars.Defs = this.Defs;
			pars.BeginParse(File.ReadAllText(file));
			IncludedFile include = new IncludedFile(pars.ParseTree);
			include.Name = file;
			this.Defs = pars.Defs;
			this.ParseTree.Add(include);
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
					{
						readToken();
						readToken();
						Definition def = new Definition();
						def.Name = readToken().ToString();
						while(!(peekToken() is Tokens.EOL))
							def.Value.Add(readToken());
						Defs.Add(def);
					}
					else if (peekToken(1).ToString() == "ifdef")
					{
						readToken();
						readToken();
						if(isDef(readToken().ToString()))
							IfScopes.Push(true);
						else
							IfScopes.Push(false);
					}
					else if (peekToken(1).ToString() == "include")
					{
						readToken();
						readToken();
						if(peekToken() is Tokens.StringLiteral)
						{
							Tokens.StringLiteral sl = readToken() as Tokens.StringLiteral;
							IncludeFile(sl.Value);
							
						}
						else if (peekToken() is Tokens.LessThan)
						{
							readToken();
							StringBuilder accum = new StringBuilder();
							while(!(peekToken() is Tokens.GreaterThan))
							{
								accum.Append(readToken().ToString());
							}
							IncludeFile(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/include/" + accum.ToString());
							readToken();
						}
					}
					else if (peekToken(1).ToString() == "import")
					{
						readToken();
						readToken();
						if(peekToken() is Tokens.StringLiteral)
						{
							Tokens.StringLiteral sl = readToken() as Tokens.StringLiteral;
							if(!importedHeaders.Contains(sl.Value))
							{
								IncludeFile( sl.Value);
							}
						}
						
					}
					else if (peekToken(1).ToString() == "ifndef")
					{
						readToken();
						readToken();
						if(!isDef(readToken().ToString()))
							IfScopes.Push(true);
						else
							IfScopes.Push(false);
					}
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