using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;

namespace Cobaltc
{
	
	public partial class Parser
	{
		private void IncludeFile(string file)
		{
			if(!File.Exists(file))
			{
				this.ParserErrors.Add(file + " does not exist in the current context!");
			}
			else
			{
				Parser pars = new Parser();
				pars.Defs = this.Defs;
				pars.BeginParse(File.ReadAllText(file));
				IncludedFile include = new IncludedFile(pars.ParseTree);
				include.Name = file;
				this.Defs = pars.Defs;
				this.ParseTree.Add(include);
			}
		}
	}
}