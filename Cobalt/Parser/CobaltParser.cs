using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GruntXProductions.Cobalt.Lexer;
using GruntXProductions.Cobalt.Parser.Grammar;

namespace GruntXProductions.Cobalt.Parser
{
    public class CobaltParser
    {
        private IList<Token> tokenList;
        private int tokenCount;
        private int position = 0;

        public CobaltParser(IList<Token> tokens)
        {
            this.tokenList = tokens;
            this.tokenCount = tokens.Count;
        }

        public void Parse()
        {
        }

        private void createAst(IList<SyntaxNode> parseTree)
        {
        }

        public Token PeekToken()
        {
            return PeekToken(0);
        }

        public Token PeekToken(int depth)
        {
            int pos = depth + position;
            if (pos < tokenCount)
                return tokenList[pos];
            else
                return null;
        }

        public Token ReadToken()
        {
            if (position < tokenCount)
                return tokenList[position++];
            else
                return null;
        }

        public bool Match(params object[] rule)
        {
            int indx = 0, i = 0; 
            while(i < rule.Length)
            {
                if (PeekToken(indx).Class != (TokenClass)rule[i])
                    return false;
                i++;
                if (i < rule.Length && rule[i] is string)
                {
                    if (PeekToken(indx).Value.ToString() != rule[i].ToString())
                        return false;
                    i++;
                }
                indx++;

            }
            return true;
        }
        // Term(TokenClass.IDENTIFIER, TokenClass.ASSIGN, Expression);
        public bool Term(SyntaxNode parent, params object[] rule)
        {
            int i = 0;
            while (i < rule.Length)
            {
                if (rule[i] is Type)
                {
                    Type t = (Type)rule[i];
                    if (typeof(Expression).Equals(t))
                        parent.AddLeave(Expression.Parse(this));
                    else if (typeof(Identifier).Equals(t))
                        parent.AddLeave(Identifier.Parse(this));
                    i++;
                }
                else if (ReadToken().Class != (TokenClass)rule[i++])
                    return false;
                

            }
            return true;
        }

    }
}
