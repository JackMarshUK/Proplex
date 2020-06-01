using System.Collections.Generic;

namespace Proplex.Lexer.Nodes
{
    public abstract class SyntaxNode
    {
        public abstract SyntaxKind Kind
        {
            get;
        }

        public abstract IEnumerable<SyntaxNode> GetChildren();
    }
}
