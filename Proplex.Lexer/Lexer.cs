//  Proplex

using System;
using Proplex.Errors;

namespace Proplex.Lexer
{
    public class Lexer
    {
        private readonly string m_text;
        private int m_position;

        public Lexer(string text)
        {
            m_text = text;
        }

        private void Next()
        {
            m_position++;
        }

        public SyntaxToken NextToken()
        {

            //< Numbers>
            //+ - * / ( )
            // whitespace
            if(m_position >= m_text.Length)
            {
                return new SyntaxToken(SyntaxKind.EndOfFileToken, m_position, "\0", null);
            }

            if(char.IsDigit(Current))
            {
                var start = m_position;

                while(char.IsDigit(Current))
                {
                    Next();
                }

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                int.TryParse(text, out var value);
                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if(char.IsWhiteSpace(Current))
            {
                var start = m_position;

                while (char.IsWhiteSpace(Current))
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
                    throw new InvalidTokenException("Invalid token entered: " + this.Current);
            }
        }

        private char Current => m_position >= m_text.Length ? '\0' : m_text[m_position];
    }
}
