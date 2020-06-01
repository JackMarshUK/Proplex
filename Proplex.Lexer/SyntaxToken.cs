using System.Collections.Generic;
using System.Linq;
using Proplex.Lexer.Nodes;

namespace Proplex.Lexer
{
    public class SyntaxToken : SyntaxNode
    {

        public int Position
        {
            get;
        }

        public string Text
        {
            get;
        }

        public object Value
        {
            get;
        }

        public SyntaxToken(SyntaxKind kind, int position, string text, object value)
        {
            this.Kind = kind;
            this.Position = position;
            this.Text = text;
            this.Value = value;
        }

        public SyntaxToken(SyntaxKind kind, int position, string text)
        {
            this.Kind = kind;
            this.Position = position;
            this.Text = text;
        }

        /// <inheritdoc />
        public override SyntaxKind Kind
        {
            get;
        }

        /// <inheritdoc />
        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return Enumerable.Empty<SyntaxNode>();
        }
    }
}