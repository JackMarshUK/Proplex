//  Proplex

using System;
using System.Collections.Generic;
using Proplex.Errors;

namespace Proplex.Syntax
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

            if(char.IsLetter(Current))
            {
                var start = m_position;

                while(char.IsLetter(this.Current))
                {
                    Next();
                }

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, text);
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
                case '&':
                    if(Lookahead == '&')
                        return new SyntaxToken(SyntaxKind.AmpersandAmpersandToken, MovePosition(2), "&&");
                    break;
                case '|':
                    if(Lookahead == '|')
                        return new SyntaxToken(SyntaxKind.PipePipeToken, MovePosition(2), "||");
                    break;
                case '=':
                    if(Lookahead == '=')
                        return new SyntaxToken(SyntaxKind.EqualsEqualsToken, MovePosition(2), "==");
                    break;
                case '!':
                    return this.Lookahead == '=' ? new SyntaxToken(SyntaxKind.BangEqualsToken, MovePosition(2), "!=") 
                               : new SyntaxToken(SyntaxKind.BangToken, m_position++, "!=");
            }
            throw new InvalidTokenException(message: $"ERROR 101: Bad character input: '{this.Current}'");
        }

        private char Current => Peek(0);
        private char Lookahead => Peek(1);
        private  int MovePosition(int amount) => m_position += amount;
        private Func<int, char> Peek => (int offset) => m_position + offset >= m_text.Length ? '\0' : m_text[m_position + offset];
    }
}
