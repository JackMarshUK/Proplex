using System;
using System.Linq;
using Proplex.Core.Binding;
using Proplex.Core.Syntax;

namespace Proplex.Core.Evaluator
{
    public sealed class Compilation
    {
        public Compilation(SyntaxTree syntax)
        {
            this.Syntax = syntax;
        }

        public SyntaxTree Syntax { get; }

        public EvaluationResult Evaluate()
        {
            var binder = new Binder();
            var boundExpression = binder.BindExpression(this.Syntax.Root);

            var diagnostics = this.Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();

            if(diagnostics.Any())
            {
                return new EvaluationResult(diagnostics, null);
            }

            var evaluator = new Evaluator(boundExpression);
            var value = evaluator.Evaluate();
            return new EvaluationResult(Array.Empty<Diagnostic>(), value);

        }
    }
}