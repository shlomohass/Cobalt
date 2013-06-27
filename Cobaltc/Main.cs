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
		
			if(args.Length == 0)
				Console.Error.WriteLine("No input!");
			else
			{
				string Input = Environment.CurrentDirectory + "/" + args[0];
				string Output = null;
				bool outputASM = false;
				Environment.CurrentDirectory = Path.GetDirectoryName(args[0]);
				Assembly prog = new Assembly();
				List<string> references = new List<string>();
				for(int i = 0; i < args.Length; i++)
				{
					if(args[i] == "-o")
					{
						Output = args[i + 1];
						i++;
					}
					else if (args[i] == "-s")
						outputASM = true;
					else if (args[i] == "--uselib")
					{
						i++;
						references.Add(args[i]);
					}
				}
				if(Output == null)
					Output = Path.GetFileNameWithoutExtension(Input) + ".gex";
				
				CodeGen cgen = null;
				Parser parser = null;
				try
				{
					parser = new Parser();
					parser.BeginParse(File.ReadAllText(Input));
					cgen = new CodeGen(prog,parser.ParseTree);
					cgen.Compile();
					if(outputASM)
					{
						StreamWriter sw = new StreamWriter(Output);
						foreach(Instruction i in cgen.Assembler.innerCode)
						{
							sw.WriteLine("0x" + i.Address.ToString("X8") + ":  " + i.Mnemonic.ToString() + " " + i.Operand.ToString());
						}
						sw.Close();
					}
					else
					{
					foreach(string lib in references)
						cgen.Assembler.Merge(GEX.Open(lib, false));
					cgen.Assembler.Metadata.Add("languageLOL","cobalt");
					cgen.Assembler.CreateMethodAttribute("test",new System.Collections.Specialized.NameValueCollection());
					cgen.Assembler.SaveGEX(Output);
					}
					
				}
				catch(ParserException exp)
				{
					Console.Error.WriteLine("Fatal error during parsing: " + exp.Error);
					if(parser.ParserErrors.Count > 0)
					{
						foreach(string err in parser.ParserErrors)
							Console.Error.WriteLine(err);
					}
				}
				catch(CodeGenException exp)
				{
					Console.Error.WriteLine(exp.Error);
					foreach(string err in cgen.Errors)
						Console.Error.WriteLine(err);
				}
				catch (Exception exp)
				{
					Console.WriteLine("Fatal error: " + exp.ToString());
				}
			}
		}
	}
}
