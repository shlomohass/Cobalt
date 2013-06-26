using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
using Viper;
using Viper.Opcodes;

namespace Cobaltc
{
	public partial class CodeAnalyser
	{
		public Assembly Assembler;
		private List<SyntaxNode> parseTree = new List<SyntaxNode>();
		public List<string> Errors = new List<string>();
		private SymbolHelper SymHelper;
		public CodeAnalyser(Assembly prog, List<SyntaxNode> ParseTree)
		{
			SymHelper = new SymbolHelper(ref prog);
			Assembler = prog;
			this.parseTree = ParseTree;
		}
		public void Compile()
		{
			SymHelper.BeginScope();
			CompileFile(this.parseTree);
			SymHelper.EndScope();
			
			if(this.Errors.Count > 0)
				throw new CodeGenException(this.Errors.Count.ToString() + " error(s) were found!");
		}
	}
}
