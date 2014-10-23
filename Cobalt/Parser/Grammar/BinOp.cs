using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GruntXProductions.Cobalt.Parser.Grammar
{
    public class BinOp : SyntaxNode
    {
        private BinaryOperation operation;

        public BinaryOperation Operation
        {
            get
            {
                return this.operation;
            }
        }

        public BinOp(BinaryOperation operation)
        {
            this.operation = operation;
        }
    }
}
