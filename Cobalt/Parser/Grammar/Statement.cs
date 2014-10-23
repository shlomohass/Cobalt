using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GruntXProductions.Cobalt.Lexer;
using GruntXProductions.Cobalt.Parser;

namespace GruntXProductions.Cobalt.Parser.Grammar
{
    public class Statement : SyntaxNode
    {
        public static SyntaxNode Parse(CobaltParser parser)
        {
            if (parser.Match(TokenClass.IDENTIFIER, "if", TokenClass.OPEN_PARAN, "("))
            {
                Statement ret = new Statement();
                parser.Match(TokenClass.IDENTIFIER, TokenClass.OPEN_PARAN, typeof(Expression),
                    TokenClass.CLOSE_PARAN, typeof(Statement));
                return ret;
            }
            else if (parser.Match(TokenClass.IDENTIFIER, "while", TokenClass.OPEN_PARAN, "("))
            {
                Statement ret = new Statement();
                parser.Match(TokenClass.IDENTIFIER, TokenClass.OPEN_PARAN, typeof(Expression),
                    TokenClass.CLOSE_PARAN, typeof(Statement));
                return ret;
            }
            else if (parser.Match(TokenClass.IDENTIFIER, TokenClass.ASSIGN))
            {
                Statement stn = new Statement();
                parser.Term(stn, typeof(Identifier), TokenClass.ASSIGN, typeof(Expression), TokenClass.SEMI_COLON);
                return stn;
            }
            else if (parser.Match(TokenClass.OPEN_BRACKET))
            {
                CodeBlock block = new CodeBlock();
                parser.ReadToken();
                while (parser.PeekToken() != null && parser.PeekToken().Class != TokenClass.CLOSE_BRACKET)
                    block.AddLeave(Statement.Parse(parser));
                parser.ReadToken();
                return block;

            }
            return null;
        }

        private static void ParseBlock(CobaltParser parser)
        {

        }

        private static void parse(CobaltParser parser, Statement parent)
        {
            parent.AddLeave(Parse(parser));
        }
    }
}
