using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GruntXProductions.Cobalt.Lexer
{
    public class CobaltLexer
    {
        private string lexerInput;
        private int lexerPosition = 0;
        private int inputLength;
        private List<Token> tokenList = new List<Token>();

        public IList<Token> TokenList
        {
            get
            {
                return this.tokenList;
            }
        }

        public CobaltLexer(string input)
        {
            this.inputLength = input.Length;
            this.lexerInput = input;
        }

      
        public void Scan()
        {
            while (peekChar() != -1)
            {
                while (char.IsWhiteSpace((char)peekChar())) readChar();
                Token tok = readToken();
                if (tok != null)
                    this.tokenList.Add(tok);
                else
                    readChar();
            }
        }

        private Token readToken()
        {
            char ch = (char)peekChar();
            if (Char.IsDigit(ch))
                return scanNumber();
            else if (Char.IsLetter(ch))
                return scanIdentifier();
            else if (isOperator(ch))
                return scanOperator();
            else
                return null;
        }

        private int peekChar()
        {
            return peekChar(0);
        }

        private int peekChar(int i)
        {
            int pos = lexerPosition + i;
            if (pos < inputLength)
            {
                return lexerInput[pos];
            }
            else
                return -1;
        }

        private string peekString(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int c = peekChar(i);
                if(c != -1)
                    sb.Append((char)c);
            }
            return sb.ToString();
        }

        private int readChar()
        {
            if (lexerPosition < inputLength)
            {
                return lexerInput[lexerPosition++];
            }
            else
                return -1;
        }

        private string readString(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int c = readChar();
                if (c != -1)
                    sb.Append((char)c);
            }
            return sb.ToString();
        }

        private bool isOperator(char ch)
        {
            return ";!=&|^(){}[]+-/*==".Contains(ch);
        }

        private Token scanNumber()
        {
            if (peekChar(0) == '0' && peekChar(1) == 'x')
            {
                readChar();
                readChar();
                return scanHexNumber();
            }
            else
            {
                StringBuilder accum = new StringBuilder();
                while (char.IsDigit((char)peekChar()))
                {
                    accum.Append((char)readChar());
                }
                return new Token(TokenClass.CONSTANT, long.Parse(accum.ToString()), 0);
            }
        }

        private Token scanHexNumber()
        {
            string hexNumbers="012345679ABCDEFabcdef";
            StringBuilder accum = new StringBuilder();
            while (hexNumbers.Contains((char)peekChar()))
            {
                accum.Append((char)readChar());
            }
            return new Token(TokenClass.CONSTANT, long.Parse(accum.ToString(), System.Globalization.NumberStyles.HexNumber), 0);
        }

        private Token scanIdentifier()
        {
            int ch = readChar();
            StringBuilder str = new StringBuilder();
            do
            {
                str.Append((char)ch);
                ch = readChar();
            } while (char.IsLetterOrDigit((char)ch) || ch == '_');
            return new Token(TokenClass.IDENTIFIER, str.ToString(), 0);
        }

        private Token scanOperator()
        {
            char op = (char)readChar();
            switch (op)
            {
                case '=':
                    if ((char)peekChar() == '=')
                    {
                        readChar();
                        return new Token(TokenClass.BIN_OP, "==", 0);
                    }
                    return new Token(TokenClass.ASSIGN, "=", 0);
                case '&':
                    if ((char)peekChar() == '&')
                    {
                        readChar();
                        return new Token(TokenClass.BIN_OP, "&&", 0);
                    }
                    return new Token(TokenClass.ASSIGN, "&", 0);
                case '(':
                    return new Token(TokenClass.OPEN_PARAN, "(", 0);
                case ')':
                    return new Token(TokenClass.CLOSE_PARAN, "(", 0);
                case '+':
                    return new Token(TokenClass.BIN_OP, "+", 0);
                case '-':
                    return new Token(TokenClass.BIN_OP, "-", 0);
                case '*':
                    return new Token(TokenClass.BIN_OP, "*", 0);
                case '/':
                    return new Token(TokenClass.BIN_OP, "/", 0);
                case ';':
                    return new Token(TokenClass.SEMI_COLON, ";", 0);
                default:
                    return null;
            }
        }
    }
}
