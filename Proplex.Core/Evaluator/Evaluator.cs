//  Proplex

using Proplex.Core.Binding;
using Proplex.Errors;

namespace Proplex.Core.Evaluator
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

                if(u.Op.Kind == BoundUnaryOperatorKind.Identity)
                {
                    return (int)operand;
                }
                else if(u.Op.Kind == BoundUnaryOperatorKind.Negation)
                {
                    return -(int)operand;
                }
                else if(u.Op.Kind == BoundUnaryOperatorKind.LogicalNegation)
                {
                    return !(bool)operand;
                }

                throw new InvalidSyntaxKindException($"Error 104: Unexpected unary operator {u.Op.Kind}");
            }

            if(!(node is BoundBinaryExpression b))
            {
                throw new InvalidSyntaxNodeException($"Error 105: Unexpected node {node.Type}");
            }

            var left =  EvaluateExpression(b.Left);
            var right = EvaluateExpression(b.Right);

            switch(b.Op.Kind)
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
                case BoundBinaryOperatorKind.Equals:
                    return Equals(left, right);
                case BoundBinaryOperatorKind.NotEquals:
                    return !Equals(left, right);
                default:
                    throw new InvalidSyntaxKindException($"Error 104: Unexpected binary operator {b.Op}");
            }

            
        }
    }
}
