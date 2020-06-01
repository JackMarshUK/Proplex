//  Proplex

using System;
using System.Collections.Generic;
using Proplex.Core.Nodes;
using Proplex.Errors;

namespace Proplex.Core.Lexer
{
    internal sealed class Lexer
    {
        private readonly string m_text;
        private int m_position;
        private List<string> m_diagnostics = new List<string>();

        public IEnumerable<string> Diagnostics => m_diagnostics;

        public Lexer(string text)
        {
            m_text = text;
        }

        private void Next()
        {
            m_position++;
        }

        public SyntaxToken Lex()
        {

            //< Numbers>
            //+ - * / ( )
            // whitespace
            if(m_position >= m_text.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, m_position, "\0", null);
            }

            if(char.IsDigit(this.Current))
            {
                var start = m_position;

                while(char.IsDigit(this.Current))
                {
                    Next();
                }

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                if(!int.TryParse(text, out var value))
                {
                    throw new InvalidCastException(message: $"ERROR 103: The : The number {m_text} isnt a valid Int32");
                }
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if(char.IsWhiteSpace(this.Current))
            {
                var start = m_position;

                while (char.IsWhiteSpace(this.Current))
                {
                    Next();
                }

                var length = m_position - start;
                var text = m_text.Substring(start, length);

                return new SyntaxToken(SyntaxKind.WhiteSpaceToken, start, text);
            }

            switch(this.Current)
            {
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, m_position++, "+");
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, m_position++, "-");
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, m_position++, "*");
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToken, m_position++, "/");
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken, m_position++, "(");
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, m_position++, ")");
                default:
                    throw new InvalidTokenException(message: $"ERROR 101: Bad character input: '{this.Current}'");
            }
        }

        private char Current => m_position >= m_text.Length ? '\0' : m_text[m_position];
    }
}
