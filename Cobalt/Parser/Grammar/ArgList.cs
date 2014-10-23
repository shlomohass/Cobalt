using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GruntXProductions.Cobalt.Parser.Grammar
{
    public class ArgList : SyntaxNode
    {
        public static SyntaxNode Parse(CobaltParser parser)
        {
            ArgList list = new ArgList();
            while (parser.PeekToken().Class != Lexer.TokenClass.CLOSE_PARAN)
            {
                list.AddLeave(Expression.Parse(parser));
                if (parser.PeekToken().Class != Lexer.TokenClass.CLOSE_PARAN && parser.PeekToken().Class
                    != Lexer.TokenClass.COMMA)
                    throw new Exception("Oh now");
            }
            parser.ReadToken();
            return list;
        }
    }
}
