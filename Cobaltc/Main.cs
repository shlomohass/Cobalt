using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using GruntXProductions;
using Viper;
namespace Cobaltc
{
	public static class Entry
	{
		
		public static void Main (string[] args)
		{
			int neg = -1;
			uint id = (uint)neg;
			Console.WriteLine(id.ToString());
			if(args.Length == 0)
				Console.Error.WriteLine("No input!");
			else
			{
				string Input = args[0];
				string Output = null;
				Assembly prog = new Assembly();
				List<string> references = new List<string>();
				for(int i = 0; i < args.Length; i++)
				{
					if(args[i] == "-o")
					{
						Output = args[i + 1];
						i++;
					}
					else if (args[i] == "--uselib")
					{
						i++;
						references.Add(args[i]);
					}
				}
				if(Output == null)
					Output = Path.GetFileNameWithoutExtension(Input) + ".gex";
				
				CodeGen cgen = null;
				try
				{
					Parser parser = new Parser();
					parser.BeginParse(File.ReadAllText(Input));
					cgen = new CodeGen(prog,parser.ParseTree);
					cgen.Compile();
					foreach(string lib in references)
						cgen.Assembler.Merge(GEX.Open(lib));
					cgen.Assembler.SaveGEX(Output);
					Assembly test = Disassembler.Disassemble(GEX.Open("test.gex"));
					foreach(Instruction il in test.innerCode)
					{
						Console.WriteLine(il.Mnemonic + " " + il.Operand);
					}
				}
				catch(ParserException exp)
				{
					Console.Error.WriteLine("Fatal error during parsing: " + exp.Error);
				}
				catch(CodeGenException exp)
				{
					Console.Error.WriteLine(exp.Error);
					foreach(string err in cgen.Errors)
					{
						Console.Error.WriteLine(err);
					}
				}
				catch (Exception exp)
				{
					Console.WriteLine("Fatal error: " + exp.ToString());
				}
			}
		}
	}
}
