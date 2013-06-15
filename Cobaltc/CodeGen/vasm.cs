using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Collections = System.Collections.Generic;
using IO = System.IO;
using Text = System.Text;
using Viper;
using Viper.Opcodes;
namespace vasm
{

    class MainClass
    {
        private static IList<object> tokens;
        private static int index = 0;
        const byte PUSH = 0x01;
        const byte POP = 0x02;
        const byte SSS = 0x03;
        const byte LSS = 0x04;
        const byte SSP = 0x05;
        const byte LSP = 0x06;
        const byte LOAD = 0x07;
        const byte STORE = 0x08;
        const byte BEQ = 0x09;
        const byte BNE = 0x0A;
        const byte BGT = 0x0B;
        const byte BLT = 0x0C;
        const byte BZ = 0x0D;
        const byte BNZ = 0xF0;
        const byte BRA = 0x0F;
        const byte BSR = 0x10;
        const byte FADD = 0x11;
        const byte ADD = 0x12;
        const byte FSUB = 0x13;
        const byte SUB = 0x14;
        const byte FMUL = 0x15;
        const byte MUL = 0x16;
        const byte FDIV = 0x17;
        const byte DIV = 0x18;
        const byte SYSF = 0x27;
        const byte BEGIN_FAULT = 0x50;
        const byte END_FAULT = 0x51;
        const byte THROW = 0x52;
        private static object readToken()
        {
            ++index;
            return tokens[index - 1];
        }
        private static object peekToken(int depth = 0)
        {
            return tokens[depth + index];

        }
        private static void WriteInstruction(byte opcode, object data = null)
        {

        }
        public static void Complile(string IL, ref Assembly asm)
        {

            Scanner scr = new Scanner(new StringReader(IL));
            tokens = scr.Tokens;
            index = 0;
            while (index < tokens.Count)
            {
                if (peekToken().ToString().ToLower() == "push")
                {
                    readToken();
                    if (peekToken().ToString().ToLower() == "dword")
                    {
                        readToken();
                        uint u = (uint)(int)readToken();
                        asm.Emit(new push_d(u));
                    }
                    else if (peekToken().ToString().ToLower() == "word")
                    {
                        readToken();
                        ushort u = (ushort)(int)readToken();
                        asm.Emit(new push_w(u));
                    }
                    else if (peekToken().ToString().ToLower() == "byte")
                    {
                    }

                    else if (peekToken().ToString().ToLower() == "ptr")
                    {
                        readToken();
                        asm.Emit(new push_ptr(readToken().ToString()));
                    }
                    else if (peekToken().ToString().ToLower() == "qword")
                    {
                    }
                }
                else if (peekToken().ToString().ToLower() == "add")
                {
                    readToken();
                    asm.Emit(new Add());
                }
                else if (peekToken().ToString().ToLower() == "sub")
                {
                    readToken();
                    asm.Emit(new sub());
                }
                else if (peekToken().ToString().ToLower() == "div")
                {
                    readToken();
                    asm.Emit(new div());
                }
                else if (peekToken().ToString().ToLower() == "mul")
                {
                    readToken();
                    asm.Emit(new mul());
                }
                else if (peekToken().ToString() == "sysf")
                {
                    readToken();
                    asm.Emit(new sysf((uint)(int)readToken()));
                }
                else if (peekToken().ToString() == "end")
                {
                    readToken();
                    WriteInstruction(0x0);
                }
                else if (peekToken().ToString().ToLower() == "dload")
                {
                    readToken();
                    asm.Emit(new dload());
                }
                else if (peekToken().ToString().ToLower() == "dstore")
                {
                    readToken();
                    asm.Emit(new dstore());
                }
                else if (peekToken().ToString().ToLower() == "wstore")
                {
                    readToken();
                    asm.Emit(new wstore());
                }
                else if (peekToken().ToString().ToLower() == "bra")
                {
                    readToken();
                    asm.Emit(new bra(readToken().ToString()));
                }
                else if (peekToken().ToString().ToLower() == "bz")
                {
                    readToken();

                    asm.Emit(new bz(readToken().ToString()));
                }
                else if (peekToken().ToString().ToLower() == "begin_fault")
                {
                    readToken();
                    asm.Emit(new begin_fault(readToken().ToString()));
                }
                else if (peekToken().ToString() == "end_fault")
                {
                    readToken();
                    asm.Emit(new end_fault());
                }
                else if (peekToken().ToString().ToLower() == "heapalloc")
                {
                    readToken();
                    asm.Emit(new heapalloc());
                }
                else if (peekToken().ToString().ToLower() == "stackalloc")
                {
                    readToken();
                    asm.Emit(new stack_alloc());
                }
                else if (peekToken().ToString().ToLower() == "heapfree")
                {
                    readToken();
                    asm.Emit(new heapfree());
                }
                else if (peekToken().ToString().ToLower() == "throw")
                {
                    readToken();
                    asm.Emit(new throw_exception());
                }
                else if (peekToken().ToString().ToLower() == "bnz")
                {
                    readToken();
                    asm.Emit(new bnz(readToken().ToString()));
                }
                else if (peekToken().ToString().ToLower() == "bsr")
                {
                    readToken();
                    asm.Emit(new bsr(readToken().ToString()));
                }
                else if (peekToken().ToString().ToLower() == "beq")
                {
                    readToken();

                    asm.Emit(new beq(readToken().ToString()));
                }
                else if (peekToken().ToString().ToLower() == "bne")
                {
                    readToken();
                    asm.Emit(new bne(readToken().ToString()));
                }
                else if (peekToken().ToString().ToLower() == "blt")
                {
                    readToken();
                    asm.Emit(new blt(readToken().ToString()));
                }
                else if (peekToken().ToString().ToLower() == "bgt")
                {
                    readToken();
                    asm.Emit(new bgt(readToken().ToString()));
                }
                else if (peekToken().ToString().ToString() == "ret")
                {
                    readToken();
                    asm.Emit(new ret());
                }
                else if (peekToken().ToString().ToLower() == "pop")
                {
                    readToken();
                    WriteInstruction(POP, (uint)0);
                }
                else if (peekToken().ToString().ToLower() == "proc")
                {
                    readToken();
                    asm.CreateLabel(readToken().ToString());
                }
                else if (peekToken(1) == Scanner.Colon)
                {

                    string name = readToken().ToString();
                    asm.CreateLabel(name);
                    readToken();
                }
                else if (peekToken(1).ToString().ToLower() == "buffer")
                {
                    string name = readToken().ToString();
                    readToken();

                    uint length = (uint)(int)readToken();
                    asm.CreateBuffer(name, length);
                }

                else if (peekToken(1).ToString().ToLower() == "string")
                {
                    string name = readToken().ToString();
                    readToken();
                    StringLiteral sl = readToken() as StringLiteral;
                    sl.Value = sl.Value.Replace(@"\n", "\n");
                    asm.CreateString(name, sl.Value + "\0");
                }
                else
                {
                    Console.WriteLine("Unknown keyword: " + readToken().ToString());
                    return;
                }
            }
        }

    }

    public class StringLiteral
    {
        public string Value;
    }
    public class Keyword
    {
        public string Value;
    }
    public class Scanner
    {
        private readonly Collections.IList<object> result;

        public Scanner(IO.TextReader input)
        {
            this.result = new Collections.List<object>();
            this.Scan(input);
        }
        public Collections.IList<object> Tokens
        {
            get { return this.result; }
        }
        public static readonly object Add = new object();
        public static readonly object Sub = new object();
        public static readonly object Mul = new object();
        public static readonly object Div = new object();
        public static readonly object Semi = new object();
        public static readonly object Assign = new object();
        public static readonly object OpenBracket = new object();
        public static readonly object CloseBracker = new object();
        public static readonly object OpenPar = new object();
        public static readonly object ClosePar = new object();
        public static readonly object Comma = new object();
        public static readonly object EOL = new object();
        public static readonly object Dot = new object();
        public static readonly object And = new object();
        public static readonly object Equal = new object();
        public static readonly object NotEqual = new object();
        public static readonly object Colon = new object();
        public static readonly object At = new object();
        public static readonly object GreaterThan = new object();
        public static readonly object LessThen = new object();
        public static readonly object PlusEqual = new object();
        public static List<List<int>> Lines = new List<List<int>>();
        int line = 0;
        private void Scan(IO.TextReader input)
        {
            Lines = new List<List<int>>();
            Lines.Add(new List<int>());
            while (input.Peek() != -1)
            {
                char ch = (char)input.Peek();
                Lines[line].Add(result.Count);
                if (ch == ';')
                {
                    while (ch != '\n')
                    {
                        ch = (char)input.Read();
                    }
                }
                else if (ch == '\n')
                {
                    input.Read();
                    Lines.Add(new List<int>());
                    line++;

                }
                else if (char.IsWhiteSpace(ch))
                {
                    input.Read();
                }

                else if (char.IsLetter(ch) || ch == '_')
                {
                    Text.StringBuilder accum = new Text.StringBuilder();

                    while (char.IsLetter(ch) || char.IsNumber(ch) || ch == '_')
                    {
                        accum.Append(ch);
                        input.Read();

                        if (input.Peek() == -1)
                        {
                            break;
                        }
                        else
                        {
                            ch = (char)input.Peek();
                        }
                    }
                    Keyword k = new Keyword();
                    k.Value = accum.ToString();
                    this.result.Add(k.Value);
                }
                else if (ch == '\'')
                {
                    input.Read();
                    result.Add((char)input.Read());
                    if (((char)input.Read()) != '\'')
                    {
                        throw new System.Exception("Expected '");
                    }

                }
                else if (ch == '"')
                {
                    Text.StringBuilder accum = new Text.StringBuilder();

                    input.Read();

                    if (input.Peek() == -1)
                    {
                        throw new System.Exception("Expected \"");
                    }

                    while ((ch = (char)input.Peek()) != '"')
                    {
                        accum.Append(ch);
                        input.Read();

                        if (input.Peek() == -1)
                        {
                            throw new System.Exception("Expected \"");
                        }
                    }
                    StringLiteral sl = new StringLiteral();
                    input.Read();
                    sl.Value = accum.ToString();
                    this.result.Add(sl);
                }
                else if (ch == '$')
                {
                    input.Read();
                    string hexString = "";
                    while (char.IsDigit(ch) || char.ToLower(ch) == 'a' || char.ToLower(ch) == 'b' || char.ToLower(ch) == 'c' || char.ToLower(ch) == 'd' || char.ToLower(ch) == 'e' || char.ToLower(ch) == 'f')
                    {
                        hexString += ch.ToString();
                    }
                    result.Add(int.Parse(hexString, System.Globalization.NumberStyles.HexNumber));
                }
                else if (char.IsDigit(ch))
                {
                    Text.StringBuilder accum = new Text.StringBuilder();

                    while (char.IsDigit(ch) || ch == '.')
                    {
                        accum.Append(ch);
                        input.Read();

                        if (input.Peek() == -1)
                        {
                            break;
                        }
                        else
                        {
                            ch = (char)input.Peek();
                        }
                    }
                    if (accum.ToString().Contains("."))
                    {
                        float f = float.Parse(accum.ToString());
                        int i = System.BitConverter.ToInt32(System.BitConverter.GetBytes(f), 0);
                        this.result.Add(i);
                    }
                    else
                        this.result.Add(int.Parse(accum.ToString()));
                }
                else switch (ch)
                    {
                        case '+':
                            input.Read();
                            if (input.Peek() == (int)'=')
                            {
                                input.Read();
                                this.result.Add(Scanner.PlusEqual);
                                break;
                            }
                            this.result.Add(Scanner.Add);
                            break;

                        case '-':
                            input.Read();
                            this.result.Add(Scanner.Sub);
                            break;

                        case '*':
                            input.Read();
                            this.result.Add(Scanner.Mul);
                            break;
                        case '@':
                            input.Read();
                            this.result.Add(Scanner.At);
                            break;
                        case '/':
                            input.Read();
                            this.result.Add(Scanner.Div);
                            break;
                        case '!':
                            input.Read();
                            if ((char)(input.Peek()) == '=')
                            {
                                input.Read();
                                this.result.Add(Scanner.NotEqual);
                            }
                            break;
                        case '=':
                            input.Read();
                            if (((char)input.Peek()) == '=')
                            {
                                input.Read();
                                this.result.Add(Scanner.Equal);
                            }
                            else
                                this.result.Add(Scanner.Assign);
                            break;

                        case ';':
                            input.Read();
                            this.result.Add(Scanner.Semi);
                            break;
                        case '[':
                            input.Read();
                            this.result.Add(Scanner.OpenBracket);
                            break;
                        case ']':
                            input.Read();
                            this.result.Add(Scanner.CloseBracker);
                            break;
                        case '(':
                            input.Read();
                            this.result.Add(Scanner.OpenPar);
                            break;
                        case '<':
                            input.Read();
                            this.result.Add(Scanner.LessThen);
                            break;
                        case '>':
                            input.Read();
                            this.result.Add(Scanner.GreaterThan);
                            break;
                        case ')':
                            input.Read();
                            this.result.Add(Scanner.ClosePar);
                            break;
                        case ',':
                            input.Read();
                            this.result.Add(Scanner.Comma);
                            break;
                        case '.':
                            input.Read();
                            this.result.Add(Scanner.Dot);
                            break;
                        case '&':
                            input.Read();
                            this.result.Add(Scanner.And);
                            break;
                        case ':':
                            input.Read();
                            this.result.Add(Scanner.Colon);
                            break;
                        default:
                            throw new System.Exception("Scanner encountered unrecognized character '" + ch + "'");
                    }

            }
        }
    }

}