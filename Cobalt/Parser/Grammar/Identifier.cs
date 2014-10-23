using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GruntXProductions.Cobalt.Lexer;

namespace GruntXProductions.Cobalt.Parser.Grammar
{
    public class Identifier : SyntaxNode
    {
        private string ident;

        public string Name
        {
            get
            {
                return this.ident;
            }
        }

        public Identifier(string name)
        {
            this.ident = name;
        }

        public override string ToString()
        {
            return this.ident;
        }

        public static Identifier Parse(CobaltParser parser)
        {
            Token tok = parser.ReadToken();
            if (tok.Class != TokenClass.IDENTIFIER)
                return null;
            else
                return new Identifier(tok.Value.ToString());
        }
    }
}
