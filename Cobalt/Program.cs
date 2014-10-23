using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GruntXProductions.Cobalt.Lexer;
using GruntXProductions.Cobalt.Parser;
using GruntXProductions.Cobalt.Parser.Grammar;
namespace GruntXProductions.Cobalt
{
    class Program
    {
        static void Main(string[] args)
        {
            long l = 4 + 2 * 4 + (1 + 2 * 2);
            Console.WriteLine(l);
            CobaltLexer lexer = new CobaltLexer("{i = 5 + 5;x = 2; y = 3;}");
            lexer.Scan();
            
            CobaltParser parser = new CobaltParser(lexer.TokenList);
            
            Console.ReadLine();
        }


       

        static long process(SyntaxNode sn)
        {
            if (sn is BinOp)
            {
                BinOp op = sn as BinOp;
                switch (op.Operation)
                {
                    case BinaryOperation.ADD:
                        Console.WriteLine("{0} + {1} ", process(op.Leaves[0]), process(op.Leaves[1]));
                        return process(op.Leaves[0]) + process(op.Leaves[1]);
                    case BinaryOperation.SUB:
                        return process(op.Leaves[0]) - process(op.Leaves[1]);
                    case BinaryOperation.DIV:
                        return process(op.Leaves[0]) / process(op.Leaves[1]);
                    case BinaryOperation.MUL:
                        Console.WriteLine("{0} * {1} ", process(op.Leaves[0]), process(op.Leaves[1]));
                        return process(op.Leaves[0]) * process(op.Leaves[1]);
                    case BinaryOperation.EQUAL:
                        return process(op.Leaves[0]) == process(op.Leaves[1]) ? 1 : 0;
                    case BinaryOperation.NOT_EQUAL:
                        return process(op.Leaves[0]) != process(op.Leaves[1]) ? 1 : 0;
                    case BinaryOperation.BOOL_AND:
                        return (process(op.Leaves[0]) & process(op.Leaves[1])) != 0 ? 1 : 0;
                    case BinaryOperation.BOOL_OR:
                        return (process(op.Leaves[0]) | process(op.Leaves[1])) != 0 ? 1 : 0;
                }
                Console.WriteLine("0");
                return 0;
            }
            else if (sn is Constant)
            {
                Constant cnst = sn as Constant;
                return cnst.Value;
            }
            else if (sn is Expression)
            {
                return process(sn.Leaves[0]);
            }
            else
            {
                return 0;
            }
           
            
        }
    }
}
