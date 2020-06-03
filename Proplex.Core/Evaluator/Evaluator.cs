//  Proplex

using Proplex.Core.Binding;
using Proplex.Core.Syntax;
using Proplex.Errors;

namespace Proplex.Core.Evaluator
{
    public class Evaluator
    {
        private readonly BoundExpression m_root;

        public Evaluator(BoundExpression root)
        {
            m_root = root;
        }

        public int Evaluate()
        {
            return EvaluateExpression(m_root);
        }

        private static int EvaluateExpression(BoundExpression node)
        {
            if(node is BoundLiteralExpression n)
            {
                return (int)n.Value;
            }
            if(node is BoundUnaryExpression u)
            {
                var operand = EvaluateExpression(u.Operand);

                if(u.OperatorKind == BoundUnaryOperatorKind.Identity)
                {
                    return operand;
                }
                if(u.OperatorKind == BoundUnaryOperatorKind.Negation)
                {
                    return -operand;
                }

                throw new InvalidSyntaxKindException($"Error 104: Unexpected unary operator {u.OperatorKind}");
            }

            if(!(node is BoundBinaryExpression b))
            {
                throw new InvalidSyntaxNodeException($"Error 105: Unexpected node {node.Type}");
            }

            var left = EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            switch(b.OperatorKind)
            {
                case BoundBinaryOperatorKind.Addition:
                    return left + right;
                case BoundBinaryOperatorKind.Subtraction:
                    return left - right;
                case BoundBinaryOperatorKind.Multiplication:
                    return left * right;
                case BoundBinaryOperatorKind.Division:
                    return left / right;
                default:
                    throw new InvalidSyntaxKindException($"Error 104: Unexpected binary operator {b.OperatorKind}");
            }

            
        }
    }
}
