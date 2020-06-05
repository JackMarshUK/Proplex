//  Proplex

using System.Collections.Generic;
using System.Linq;

namespace Proplex.Core.Evaluator
{
    public sealed class EvaluationResult
    {
        public object Value
        {
            get;
        }

        private readonly IEnumerable<Diagnostic> m_diagnostics;

        public EvaluationResult(IEnumerable<Diagnostic> diagnostics, object value)
        {
            this.Value = value;
            m_diagnostics = diagnostics;
        }

        public IReadOnlyList<Diagnostic> Diagnostics => m_diagnostics.ToArray();
    }
}