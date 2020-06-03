//  Proplex

using Proplex.Binding;
using Proplex.Errors;

namespace Proplex.Evaluator
{
    internal class Evaluator
    {
        private readonly BoundExpression m_root;

        public Evaluator(BoundExpression root)
        {
            m_root = root;
        }

        public object Evaluate()
        {
            return EvaluateExpression(m_root);
        }

        private static object EvaluateExpression(BoundExpression node)
        {
            if(node is BoundLiteralExpression n)
            {
                return n.Value;
            }
            if(node is BoundUnaryExpression u)
            {
                var operand = EvaluateExpression(u.Operand);

                if(u.OperatorKind == BoundUnaryOperatorKind.Identity)
                {
                    return (int)operand;
                }
                else if(u.OperatorKind == BoundUnaryOperatorKind.Negation)
                {
                    return -(int)operand;
                }
                else if(u.OperatorKind == BoundUnaryOperatorKind.LogicalNegation)
                {
                    return !(bool)operand;
                }

                throw new InvalidSyntaxKindException($"Error 104: Unexpected unary operator {u.OperatorKind}");
            }

            if(!(node is BoundBinaryExpression b))
            {
                throw new InvalidSyntaxNodeException($"Error 105: Unexpected node {node.Type}");
            }

            var left =  EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            switch(b.OperatorKind)
            {
                case BoundBinaryOperatorKind.Addition:
                    return (int)left + (int)right;
                case BoundBinaryOperatorKind.Subtraction:
                    return (int)left - (int)right;
                case BoundBinaryOperatorKind.Multiplication:
                    return (int)left * (int)right;
                case BoundBinaryOperatorKind.Division:
                    return (int)left / (int)right;
                case BoundBinaryOperatorKind.LogicalAnd:
                    return (bool)left && (bool)right;
                case BoundBinaryOperatorKind.LogicalOr:
                    return (bool)left || (bool)right;
                default:
                    throw new InvalidSyntaxKindException($"Error 104: Unexpected binary operator {b.OperatorKind}");
            }

            
        }
    }
}
