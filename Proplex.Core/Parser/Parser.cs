//  Proplex

using System.Collections.Generic;
using System.Diagnostics;
using Proplex.Core.Nodes;
using Proplex.Errors;

namespace Proplex.Core.Parser
{
    public sealed class Parser
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
                token = lexer.Lex();

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

        private SyntaxToken MatchToken(SyntaxKind kind)
        {
            return this.Current.Kind == kind ? NextToken() : throw new InvalidTokenException($"ERROR 102: Unexpected token: <{this.Current.Kind}>, Expected: <{kind}>");
        }


        public SyntaxTree Parse()
        {
            return new SyntaxTree(this.Diagnostics, ParseExpression(), MatchToken(SyntaxKind.EndOfFileToken));
        }

        private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
        {
           var left = ParsePrimaryExpression();

           while(true)
           {
               var precedence = GetBinaryOperatorPrecedence(Current.Kind);
               if(precedence == 0 || precedence <= parentPrecedence)
                   break;

               var operatorToken = NextToken();
               var right = ParseExpression(precedence);
               left = new BinaryExpressionSyntax(left, operatorToken, right);
           }

           return left;
        }

        private static int GetBinaryOperatorPrecedence(SyntaxKind kind)
        {
            switch(kind)
            {
                case SyntaxKind.StarToken:
                case SyntaxKind.SlashToken:
                    return 2;

                case SyntaxKind.PlusToken:
                case SyntaxKind.MinusToken:
                    return 1;

                default:
                    return 0;
            }
        }



        private ExpressionSyntax ParsePrimaryExpression()
        {
            if(this.Current.Kind == SyntaxKind.OpenParenthesisToken)
            {
                var left = NextToken();
                var expression = ParseExpression();
                var right = MatchToken(SyntaxKind.CloseParenthesisToken);
                return new ParenthesizedExpressionSyntax(left, expression, right);
            }

            var numberToken = MatchToken(SyntaxKind.NumberToken);
            return new LiteraExpressionSyntax(numberToken);
        }

        public IEnumerable<string> Diagnostics => m_diagnostics;

        private SyntaxToken Current => Peek(0);
    }
}
