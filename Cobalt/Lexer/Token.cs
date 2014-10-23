using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GruntXProductions.Cobalt.Lexer
{
    public class Token
    {
        private TokenClass tokenClass;
        private object tokenValue;
        private int line;

        public TokenClass Class
        {
            get
            {
                return this.tokenClass;
            }
        }

        public object Value
        {
            get
            {
                return this.tokenValue;
            }
        }

        public int Line
        {
            get
            {
                return this.line;
            }
        }

        public Token(TokenClass clazz, object val, int line)
        {
            this.tokenClass = clazz;
            this.tokenValue = val;
            this.line = line;
        }
    }
}
