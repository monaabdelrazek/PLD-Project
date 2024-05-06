
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF           =  0, // (EOF)
        SYMBOL_ERROR         =  1, // (Error)
        SYMBOL_WHITESPACE    =  2, // Whitespace
        SYMBOL_MINUS         =  3, // '-'
        SYMBOL_MINUSMINUS    =  4, // '--'
        SYMBOL_EXCLAMEQ      =  5, // '!='
        SYMBOL_DOLLAR        =  6, // '$'
        SYMBOL_PERCENT       =  7, // '%'
        SYMBOL_LPAREN        =  8, // '('
        SYMBOL_RPAREN        =  9, // ')'
        SYMBOL_TIMES         = 10, // '*'
        SYMBOL_TIMESTIMES    = 11, // '**'
        SYMBOL_COMMA         = 12, // ','
        SYMBOL_DIV           = 13, // '/'
        SYMBOL_LBRACKET      = 14, // '['
        SYMBOL_RBRACKET      = 15, // ']'
        SYMBOL_LBRACE        = 16, // '{'
        SYMBOL_RBRACE        = 17, // '}'
        SYMBOL_PLUS          = 18, // '+'
        SYMBOL_PLUSPLUS      = 19, // '++'
        SYMBOL_LT            = 20, // '<'
        SYMBOL_LTEQ          = 21, // '<='
        SYMBOL_EQ            = 22, // '='
        SYMBOL_GT            = 23, // '>'
        SYMBOL_GTEQ          = 24, // '>='
        SYMBOL_ALOT          = 25, // alot
        SYMBOL_DIGIT         = 26, // Digit
        SYMBOL_DOUBLE        = 27, // double
        SYMBOL_ID            = 28, // Id
        SYMBOL_MONA          = 29, // Mona
        SYMBOL_MONAFUN       = 30, // MonaFun
        SYMBOL_MONAIF        = 31, // MonaIf
        SYMBOL_MONMONCALL    = 32, // MonmonCall
        SYMBOL_OR            = 33, // or
        SYMBOL_STRING        = 34, // string
        SYMBOL_ADDEXP        = 35, // <Add Exp>
        SYMBOL_ASSIGN        = 36, // <assign>
        SYMBOL_CALLINGMETHOD = 37, // <callingmethod>
        SYMBOL_CONDITION     = 38, // <condition>
        SYMBOL_DIGIT2        = 39, // <digit>
        SYMBOL_ID2           = 40, // <id>
        SYMBOL_IFSTATMENT    = 41, // <if statment>
        SYMBOL_LOOP          = 42, // <loop>
        SYMBOL_METHOD        = 43, // <method>
        SYMBOL_MULTEXP       = 44, // <Mult Exp>
        SYMBOL_OP            = 45, // <op>
        SYMBOL_PARMATER      = 46, // <parmater>
        SYMBOL_POWEREXP      = 47, // <power Exp>
        SYMBOL_PROGRAM       = 48, // <Program>
        SYMBOL_STATM_LIST    = 49, // <statm_list>
        SYMBOL_STEPS         = 50, // <steps>
        SYMBOL_TYPE          = 51, // <type>
        SYMBOL_VALUE         = 52  // <Value>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_MONA_LBRACE_RBRACE_MONA                      =  0, // <Program> ::= Mona '{' <statm_list> '}' Mona
        RULE_STATM_LIST                                           =  1, // <statm_list> ::= <assign>
        RULE_STATM_LIST2                                          =  2, // <statm_list> ::= <if statment>
        RULE_STATM_LIST3                                          =  3, // <statm_list> ::= <loop>
        RULE_STATM_LIST4                                          =  4, // <statm_list> ::= <method>
        RULE_STATM_LIST5                                          =  5, // <statm_list> ::= <callingmethod>
        RULE_ASSIGN_EQ_DOLLAR                                     =  6, // <assign> ::= <id> '=' <Add Exp> '$'
        RULE_ID_ID                                                =  7, // <id> ::= Id
        RULE_ADDEXP_PLUS                                          =  8, // <Add Exp> ::= <Add Exp> '+' <Mult Exp>
        RULE_ADDEXP_MINUS                                         =  9, // <Add Exp> ::= <Add Exp> '-' <Mult Exp>
        RULE_ADDEXP_PERCENT                                       = 10, // <Add Exp> ::= <Add Exp> '%' <Mult Exp>
        RULE_ADDEXP                                               = 11, // <Add Exp> ::= <Mult Exp>
        RULE_MULTEXP_TIMES                                        = 12, // <Mult Exp> ::= <Mult Exp> '*' <power Exp>
        RULE_MULTEXP_DIV                                          = 13, // <Mult Exp> ::= <Mult Exp> '/' <power Exp>
        RULE_MULTEXP                                              = 14, // <Mult Exp> ::= <power Exp>
        RULE_POWEREXP_TIMESTIMES                                  = 15, // <power Exp> ::= <Value> '**' <Add Exp>
        RULE_POWEREXP                                             = 16, // <power Exp> ::= <Value>
        RULE_VALUE_LPAREN_RPAREN                                  = 17, // <Value> ::= '(' <Add Exp> ')'
        RULE_VALUE                                                = 18, // <Value> ::= <id>
        RULE_VALUE2                                               = 19, // <Value> ::= <digit>
        RULE_DIGIT_DIGIT                                          = 20, // <digit> ::= Digit
        RULE_IFSTATMENT_MONAIF_LBRACE_RBRACE_LBRACKET_RBRACKET_OR = 21, // <if statment> ::= MonaIf '{' <condition> '}' '[' <statm_list> ']' or <statm_list>
        RULE_CONDITION                                            = 22, // <condition> ::= <Add Exp> <op> <Add Exp>
        RULE_OP_LT                                                = 23, // <op> ::= '<'
        RULE_OP_GT                                                = 24, // <op> ::= '>'
        RULE_OP_LTEQ                                              = 25, // <op> ::= '<='
        RULE_OP_GTEQ                                              = 26, // <op> ::= '>='
        RULE_OP_EXCLAMEQ                                          = 27, // <op> ::= '!='
        RULE_LOOP_ALOT_LPAREN_MONA_MONA_RPAREN_LBRACE_RBRACE      = 28, // <loop> ::= alot '(' <type> <assign> Mona <condition> Mona <steps> ')' '{' <statm_list> '}'
        RULE_TYPE_DOUBLE                                          = 29, // <type> ::= double
        RULE_TYPE_STRING                                          = 30, // <type> ::= string
        RULE_STEPS_PLUSPLUS                                       = 31, // <steps> ::= '++' <id>
        RULE_STEPS_PLUSPLUS2                                      = 32, // <steps> ::= <id> '++'
        RULE_STEPS_MINUSMINUS                                     = 33, // <steps> ::= '--' <id>
        RULE_STEPS_MINUSMINUS2                                    = 34, // <steps> ::= <id> '--'
        RULE_STEPS                                                = 35, // <steps> ::= <assign>
        RULE_METHOD_MONAFUN_LPAREN_RPAREN_LBRACE_RBRACE           = 36, // <method> ::= MonaFun <type> <id> '(' <parmater> ')' '{' <statm_list> '}'
        RULE_PARMATER_COMMA                                       = 37, // <parmater> ::= <id> ',' <parmater>
        RULE_PARMATER                                             = 38, // <parmater> ::= <id>
        RULE_CALLINGMETHOD_MONMONCALL_LPAREN_RPAREN               = 39  // <callingmethod> ::= MonmonCall <id> '(' <parmater> ')'
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst;
        ListBox lst2;
        public MyParser(string filename, ListBox lst,ListBox lst2)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst2 = lst2;
            this.lst = lst;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXCLAMEQ :
                //'!='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOLLAR :
                //'$'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMESTIMES :
                //'**'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACKET :
                //'['
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACKET :
                //']'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ALOT :
                //alot
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT :
                //Digit
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DOUBLE :
                //double
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID :
                //Id
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MONA :
                //Mona
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MONAFUN :
                //MonaFun
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MONAIF :
                //MonaIf
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MONMONCALL :
                //MonmonCall
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OR :
                //or
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRING :
                //string
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADDEXP :
                //<Add Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGN :
                //<assign>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CALLINGMETHOD :
                //<callingmethod>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONDITION :
                //<condition>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIGIT2 :
                //<digit>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ID2 :
                //<id>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFSTATMENT :
                //<if statment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOOP :
                //<loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHOD :
                //<method>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULTEXP :
                //<Mult Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARMATER :
                //<parmater>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_POWEREXP :
                //<power Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<Program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATM_LIST :
                //<statm_list>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEPS :
                //<steps>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPE :
                //<type>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VALUE :
                //<Value>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_MONA_LBRACE_RBRACE_MONA :
                //<Program> ::= Mona '{' <statm_list> '}' Mona
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATM_LIST :
                //<statm_list> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATM_LIST2 :
                //<statm_list> ::= <if statment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATM_LIST3 :
                //<statm_list> ::= <loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATM_LIST4 :
                //<statm_list> ::= <method>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATM_LIST5 :
                //<statm_list> ::= <callingmethod>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGN_EQ_DOLLAR :
                //<assign> ::= <id> '=' <Add Exp> '$'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ID_ID :
                //<id> ::= Id
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_PLUS :
                //<Add Exp> ::= <Add Exp> '+' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_MINUS :
                //<Add Exp> ::= <Add Exp> '-' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_PERCENT :
                //<Add Exp> ::= <Add Exp> '%' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP :
                //<Add Exp> ::= <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_TIMES :
                //<Mult Exp> ::= <Mult Exp> '*' <power Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_DIV :
                //<Mult Exp> ::= <Mult Exp> '/' <power Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP :
                //<Mult Exp> ::= <power Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_POWEREXP_TIMESTIMES :
                //<power Exp> ::= <Value> '**' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_POWEREXP :
                //<power Exp> ::= <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_LPAREN_RPAREN :
                //<Value> ::= '(' <Add Exp> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE :
                //<Value> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE2 :
                //<Value> ::= <digit>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_DIGIT_DIGIT :
                //<digit> ::= Digit
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATMENT_MONAIF_LBRACE_RBRACE_LBRACKET_RBRACKET_OR :
                //<if statment> ::= MonaIf '{' <condition> '}' '[' <statm_list> ']' or <statm_list>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONDITION :
                //<condition> ::= <Add Exp> <op> <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LTEQ :
                //<op> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GTEQ :
                //<op> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_EXCLAMEQ :
                //<op> ::= '!='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOOP_ALOT_LPAREN_MONA_MONA_RPAREN_LBRACE_RBRACE :
                //<loop> ::= alot '(' <type> <assign> Mona <condition> Mona <steps> ')' '{' <statm_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_DOUBLE :
                //<type> ::= double
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_STRING :
                //<type> ::= string
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_PLUSPLUS :
                //<steps> ::= '++' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_PLUSPLUS2 :
                //<steps> ::= <id> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_MINUSMINUS :
                //<steps> ::= '--' <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_MINUSMINUS2 :
                //<steps> ::= <id> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS :
                //<steps> ::= <assign>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHOD_MONAFUN_LPAREN_RPAREN_LBRACE_RBRACE :
                //<method> ::= MonaFun <type> <id> '(' <parmater> ')' '{' <statm_list> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARMATER_COMMA :
                //<parmater> ::= <id> ',' <parmater>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARMATER :
                //<parmater> ::= <id>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CALLINGMETHOD_MONMONCALL_LPAREN_RPAREN :
                //<callingmethod> ::= MonmonCall <id> '(' <parmater> ')'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '" + args.UnexpectedToken.ToString() + "  in line " + args.UnexpectedToken.Location.LineNr;
            lst.Items.Add(message);
            string m2 = args.ExpectedTokens.ToString();
            lst.Items.Add(m2);
            //todo: Report message to UI?
        }

        private void TokenReadEvent(LALRParser pr, TokenReadEventArgs args)
        {

            string info = args.Token.Text + "\t \t" + (SymbolConstants)args.Token.Symbol.Id;
            lst2.Items.Add(info);
        }
    }
}
