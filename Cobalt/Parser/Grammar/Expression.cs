using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GruntXProductions.Cobalt.Lexer;

namespace GruntXProductions.Cobalt.Parser.Grammar
{
    public class Expression : SyntaxNode
    {
        /*
         * <Expression>  ::= <Expression> '>'  <Add Exp> 
               |  <Expression> '<'  <Add Exp> 
               |  <Expression> '<=' <Add Exp> 
               |  <Expression> '>=' <Add Exp>
               |  <Expression> '==' <Add Exp>    !Equal
               |  <Expression> '<>' <Add Exp>    !Not equal
               |  <Add Exp> 
         * 
         */

        public Expression(SyntaxNode root)
        {
            this.AddLeave(root);
        }

        public static SyntaxNode Parse(CobaltParser parser)
        {
            return new Expression(ParseLevel0BinOperation(parser));
        }

        private static int getPrecedence(string op)
        {
            switch (op)
            {
                case "||":
                    return 0;
                case "&&":
                    return 1;
                case "!=":
                case "==":
                    return 2;
                case "<":
                case ">":
                case "<=":
                case ">=":
                    return 3;
                case ">>":
                case "<<":
                    return 4;
                case "+":
                case "-":
                    return 5;
                case "*":
                case "/":
                    return 6;
            }
            Console.WriteLine("Could not find " + op);
            return 6;
        }

        private static SyntaxNode ParseLevel0BinOperation(CobaltParser parser)
        {
            SyntaxNode left = ParseLevel1BinOperation(parser);

            if (parser.PeekToken() != null && parser.PeekToken().Class == TokenClass.BIN_OP
                && getPrecedence(parser.PeekToken().Value.ToString()) == 0)
            {
                string oper = parser.ReadToken().Value.ToString();
                BinOp ret = new BinOp(BinaryOperation.BOOL_OR);
                ret.AddLeave(left);
                ret.AddLeave(ParseLevel0BinOperation(parser));
                return ret;
            }


            return left;
        }

        private static SyntaxNode ParseLevel1BinOperation(CobaltParser parser)
        {
            SyntaxNode left = ParseLevel2BinOperation(parser);

            if (parser.PeekToken() != null && parser.PeekToken().Class == TokenClass.BIN_OP
                && getPrecedence(parser.PeekToken().Value.ToString()) == 1)
            {
                string oper = parser.ReadToken().Value.ToString();
                BinOp ret = new BinOp(BinaryOperation.BOOL_AND);
                ret.AddLeave(left);
                ret.AddLeave(ParseLevel1BinOperation(parser));
                return ret;
            }


            return left;
        }

        private static SyntaxNode ParseLevel2BinOperation(CobaltParser parser)
        {
            SyntaxNode left = ParseLevel3BinOperation(parser);

            if (parser.PeekToken() != null && parser.PeekToken().Class == TokenClass.BIN_OP
                && getPrecedence(parser.PeekToken().Value.ToString()) == 2)
            {
                string oper = parser.ReadToken().Value.ToString();
                BinOp ret = new BinOp(oper == "==" ? BinaryOperation.EQUAL : BinaryOperation.NOT_EQUAL);
                ret.AddLeave(left);
                ret.AddLeave(ParseLevel2BinOperation(parser));
                return ret;
            }


            return left;
        }

        private static SyntaxNode ParseLevel3BinOperation(CobaltParser parser)
        {
            SyntaxNode left = ParseLevel4BinOperation(parser);

            if (parser.PeekToken() != null && parser.PeekToken().Class == TokenClass.BIN_OP
                && getPrecedence(parser.PeekToken().Value.ToString()) == 3)
            {
                string oper = parser.ReadToken().Value.ToString();
                BinOp ret = new BinOp(oper == "==!=" ? BinaryOperation.ADD : BinaryOperation.SUB);
                ret.AddLeave(left);
                ret.AddLeave(ParseLevel3BinOperation(parser));
                return ret;
            }


            return left;
        }

        private static SyntaxNode ParseLevel4BinOperation(CobaltParser parser)
        {
            SyntaxNode left = ParseLevel5BinOperation(parser);

            if (parser.PeekToken() != null && parser.PeekToken().Class == TokenClass.BIN_OP
                && getPrecedence(parser.PeekToken().Value.ToString()) == 4)
            {
                string oper = parser.ReadToken().Value.ToString();
                BinOp ret = new BinOp(oper == "+" ? BinaryOperation.ADD : BinaryOperation.SUB);
                ret.AddLeave(left);
                ret.AddLeave(ParseLevel4BinOperation(parser));
                return ret;
            }


            return left;
        }

        private static SyntaxNode ParseLevel5BinOperation(CobaltParser parser)
        {
            SyntaxNode left = ParseLevel6BinOperation(parser);

            if (parser.PeekToken() != null && parser.PeekToken().Class == TokenClass.BIN_OP
                && getPrecedence(parser.PeekToken().Value.ToString()) == 5)
            {
                string oper = parser.ReadToken().Value.ToString();
                BinOp ret = new BinOp(oper == "+" ? BinaryOperation.ADD : BinaryOperation.SUB);
                ret.AddLeave(left);
                ret.AddLeave(ParseLevel5BinOperation(parser));
                return ret;
            }

            
            return left;
        }

        private static SyntaxNode ParseLevel6BinOperation(CobaltParser parser)
        {
            SyntaxNode left = ParseValue(parser);
            if (parser.PeekToken() != null && parser.PeekToken().Class == TokenClass.BIN_OP
                && getPrecedence(parser.PeekToken().Value.ToString()) == 6)
            {

                string oper = parser.ReadToken().Value.ToString();
                Console.WriteLine(oper);
                BinOp ret = new BinOp(oper == "*" ? BinaryOperation.MUL : BinaryOperation.DIV);
                ret.AddLeave(left);
                ret.AddLeave(ParseLevel6BinOperation(parser));
                return ret;
            }
            return left;
        }

        private static SyntaxNode ParseValue(CobaltParser parser)
        {
            if (parser.PeekToken().Class == TokenClass.IDENTIFIER)
                return new Identifier(parser.ReadToken().Value.ToString());
            else if (parser.PeekToken().Class == TokenClass.CONSTANT)
                return new Constant((long)parser.ReadToken().Value);
            else if (parser.PeekToken().Class == TokenClass.OPEN_PARAN)
            {
                parser.ReadToken();
                SyntaxNode ret = ParseLevel0BinOperation(parser);
                if (parser.ReadToken().Class != TokenClass.CLOSE_PARAN)
                    throw new Exception("Expected )");
                return ret;
            }
            else
            {
                Console.WriteLine(parser.PeekToken().Class);
                return null;
            }
        }
    }
}
