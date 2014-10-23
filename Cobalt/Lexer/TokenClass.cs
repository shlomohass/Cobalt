using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GruntXProductions.Cobalt.Lexer
{
    public enum TokenClass
    {
        IDENTIFIER = 0,
        KEYWORD = 1,
        CONSTANT = 2,
        STRING_LITERAL = 3,
        BIN_OP = 4,
        UNARY_OP = 5,
        OPEN_PARAN = 6,
        CLOSE_PARAN = 7,
        OPEN_BRACKET= 8,
        CLOSE_BRACKET = 9,
        ASSIGN = 10,
        SEMI_COLON = 11,
        COMMA = 12,
    }
}
