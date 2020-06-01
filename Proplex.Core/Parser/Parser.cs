//  Proplex

using System.Collections.Generic;
using Proplex.Core.Nodes;
using Proplex.Errors;

namespace Proplex.Core.Parser
{
    public class Parser
    {
        private readonly SyntaxToken[] m_tokens;
        private int m_position;
        private List<string> m_diagnostics = new List<string>();

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

        private SyntaxToken NextToken()
        {
            var current = this.Current;
            m_position++;
            return current;
        }

        private SyntaxToken Match(SyntaxKind kind)
        {
            return this.Current.Kind == kind ? NextToken() : throw new InvalidTokenException($"ERROR 102: Unexpected token: <{this.Current.Kind}>, Expected: <{kind}>");
        }

        private ExpressionSyntax ParseExpression()
        {
            return ParseTerm();
        }

        public SyntaxTree Parse()
        {
            
            return new SyntaxTree(this.Diagnostics, ParseTerm(), Match(SyntaxKind.EndOfFileToken));
        }

        private ExpressionSyntax ParseTerm()
        {
            var left = ParseFactor();

            while (this.Current.Kind == SyntaxKind.PlusToken 
                   || this.Current.Kind == SyntaxKind.MinusToken)
            {
                var operatorToken = NextToken();
                var right = ParseFactor();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParseFactor()
        {
            var left = ParsePrimaryExpression();

            while ( this.Current.Kind == SyntaxKind.StarToken
                   || this.Current.Kind == SyntaxKind.SlashToken)
            {
                var operatorToken = NextToken();
                var right = ParsePrimaryExpression();
                left = new BinaryExpressionSyntax(left, operatorToken, right);
            }

            return left;
        }

        private ExpressionSyntax ParsePrimaryExpression()
        {
            if(this.Current.Kind == SyntaxKind.OpenParenthesisToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = Match(SyntaxKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(left, expression, right);
            }

            var numberToken = Match(SyntaxKind.NumberToken);
            return new NumberExpressionSyntax(numberToken);
        }

        public IEnumerable<string> Diagnostics => m_diagnostics;

        private SyntaxToken Current => Peek(0);
    }
}
