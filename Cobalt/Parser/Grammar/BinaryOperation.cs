using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GruntXProductions.Cobalt.Parser.Grammar
{
    public enum BinaryOperation
    {
        ADD = 0,
        SUB = 1,
        DIV = 2,
        MUL = 3,
        BOOL_OR = 4,
        BOOL_AND = 5,
        EQUAL = 6,
        NOT_EQUAL = 7,
        GREATER = 8,
        LESSER = 9,
        GREATER_EQ = 10,
        LESSER_EQ = 11,
    }
}
