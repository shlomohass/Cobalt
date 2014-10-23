using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GruntXProductions.Cobalt.Parser.Grammar
{
    public class Constant : SyntaxNode
    {
        private long constant;

        public long Value
        {
            get
            {
                return this.constant;
            }
        }

        public Constant(long v)
        {
            this.constant = v;
        }
    }
}
