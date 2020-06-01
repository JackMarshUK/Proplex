using Proplex.Core.Nodes;
using Proplex.Errors;

namespace Proplex.Core.Evaluator
{
    public class Evaluator
    {
        private readonly ExpressionSyntax m_root;

        public Evaluator(ExpressionSyntax root)
        {
            m_root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(m_root);
        }

        private static int EvaluateExpression(ExpressionSyntax node)
        {
            if(node is NumberExpressionSyntax n)
            {
                return (int)n.NumberToken.Value;
            }

            if(node is ParenthesizedExpressionSyntax p)
            { 
                return EvaluateExpression(p.Expression);
            }

            if(!(node is BinaryExpressionSyntax b))
            {
                throw new InvalidSyntaxNodeException($"Error 105: Unexpected node {node.Kind}");
            }

            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            switch(b.OperatorToken.Kind)
            {
                case SyntaxKind.PlusToken:
                    return left + right;
                case SyntaxKind.MinusToken:
                    return left - right;
                case SyntaxKind.StarToken:
                    return left * right;
                case SyntaxKind.SlashToken:
                    return left / right;
                default:
                    throw new InvalidSyntaxKindException($"Error 104: Unexpected binary operator {b.OperatorToken.Kind}");
            }

            
        }
    }
}
