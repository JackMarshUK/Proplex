//  Proplex

using System;
using System.Collections.Generic;
using System.Linq;
using Proplex.Core.Evaluator;
using Proplex.Errors;

namespace Proplex.Core.Syntax

{
    internal sealed class Lexer
    {
        private readonly string m_text;
        private int m_position;
        private readonly DiagnosticBag m_diagnostics = new DiagnosticBag();

        public Lexer(string text)
        {
            m_text = text;
        }

        public DiagnosticBag Diagnostics => m_diagnostics;

        private char Current => Peek(0);

        private char Lookahead => Peek(1);

        private char Peek(int offset)
        {
            var index = m_position + offset;

            return index >= m_text.Length ? '\0' : m_text[index];
        }

        private void Next()
        {
            m_position++;
        }

        public SyntaxToken Lex()
        {
            if(m_position >= m_text.Length)
                return new SyntaxToken(SyntaxKind.EndOfFileToken, m_position, "\0", null);

            var start = m_position;

            if(char.IsDigit(Current))
            {
                while(char.IsDigit(Current))
                    Next();

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                if(!int.TryParse(text, out var value))
                    m_diagnostics.ReportInvalidNumber(new TextSpan(start, length), m_text, typeof(int));

                return new SyntaxToken(SyntaxKind.NumberToken, start, text, value);
            }

            if(char.IsWhiteSpace(Current))
            {
                while(char.IsWhiteSpace(Current))
                    Next();

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                return new SyntaxToken(SyntaxKind.WhitespaceToken, start, text, null);
            }

            if(char.IsLetter(Current))
            {
                while(char.IsLetter(Current))
                    Next();

                var length = m_position - start;
                var text = m_text.Substring(start, length);
                var kind = SyntaxFacts.GetKeywordKind(text);
                return new SyntaxToken(kind, start, text, null);
            }

            switch(Current)
            {
                case '+':
                    return new SyntaxToken(SyntaxKind.PlusToken, m_position++, "+", null);
                case '-':
                    return new SyntaxToken(SyntaxKind.MinusToken, m_position++, "-", null);
                case '*':
                    return new SyntaxToken(SyntaxKind.StarToken, m_position++, "*", null);
                case '/':
                    return new SyntaxToken(SyntaxKind.SlashToken, m_position++, "/", null);
                case '(':
                    return new SyntaxToken(SyntaxKind.OpenParenthesisToken, m_position++, "(", null);
                case ')':
                    return new SyntaxToken(SyntaxKind.CloseParenthesisToken, m_position++, ")", null);
                case '&':
                    if(Lookahead == '&')
                    {
                        m_position += 2;
                        return new SyntaxToken(SyntaxKind.AmpersandAmpersandToken, start, "&&", null);
                    }

                    break;
                case '|':
                    if(Lookahead == '|')
                    {
                        m_position += 2;
                        return new SyntaxToken(SyntaxKind.PipePipeToken, start, "||", null);
                    }

                    break;
                case '=':
                    if(Lookahead == '=')
                    {
                        m_position += 2;
                        return new SyntaxToken(SyntaxKind.EqualsEqualsToken, start, "==", null);
                    }

                    break;
                case '!':
                    if(Lookahead == '=')
                    {
                        m_position += 2;
                        return new SyntaxToken(SyntaxKind.BangEqualsToken, start, "!=", null);
                    }
                    else
                    {
                        m_position += 1;
                        return new SyntaxToken(SyntaxKind.BangToken, start, "!", null);
                    }
            }

            m_diagnostics.ReportBadCharacter(m_position, Current);
            return new SyntaxToken(SyntaxKind.BadToken, m_position++, m_text.Substring(m_position - 1, 1), null);
        }
    }
}
