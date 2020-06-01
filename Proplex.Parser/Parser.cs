using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Proplex.Lexer;
using Proplex.Lexer.Nodes;

namespace Proplex.Parser
{
    public class Parser
    {
        private SyntaxToken[] m_tokens;
        private int m_position;

        public Parser(string text)
        {
            var tokens = new List<SyntaxToken>();
            var lexer = new Lexer.Lexer(text);
            SyntaxToken token;
            do
            {
                token = lexer.NextToken();

                if(token.Kind != SyntaxKind.WhiteSpaceToken)
                {
                    tokens.Add(token);
                }

            } while(token.Kind != SyntaxKind.EndOfFileToken);

            m_tokens = tokens.ToArray();
        }

        private SyntaxToken Peek(int offset)
        {
            var index = m_position + offset;
            return index >= m_tokens.Length ? m_tokens[m_tokens.Length - 1] : m_tokens[index];
        }

        private SyntaxToken Current => Peek(0);

        private SyntaxToken NextToken()
        {
            var current = Current;
            m_position++;
            return current;
        }


        private SyntaxToken Match(SyntaxKind kind)
        {
            return this.Current.Kind == kind ? NextToken() : new SyntaxToken(kind, this.Current.Position, null);
        }

        public ExpressionSyntax Parse()
        {
            var left = ParsePrimaryExpression();

            while(Current.Kind == SyntaxKind.PlusToken || Current.Kind == SyntaxKind.PlusToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            var numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }
    }
}
