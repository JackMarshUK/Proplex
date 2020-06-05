//  Proplex
//  Copyright © 2019-2020 MJ Quinn Ltd. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using Proplex.Core.Syntax;

namespace Proplex.Core.Evaluator
{
    internal sealed class DiagnosticBag : IEnumerable<Diagnostic>
    {
        private readonly List<Diagnostic> m_diagnostic = new List<Diagnostic>();

        /// <inheritdoc />
        public IEnumerator<Diagnostic> GetEnumerator() => m_diagnostic.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public DiagnosticBag()
        {

        }

        public void AddRange(DiagnosticBag diagnostics)
        {
            m_diagnostic.AddRange(diagnostics.m_diagnostic);
        }


        private void Report(TextSpan span, string message)
        {
            var diagnostic = new Diagnostic(span, message);
            m_diagnostic.Add(diagnostic);
        }



        internal void ReportInvalidNumber(TextSpan span, string text, Type type)
        {
            var message = $"The number {text} isn't valid {type}.";
            Report(span, message);
        }

        public void ReportBadCharacter(int position, char character)
        {
            var message = $"Bad character input: '{character}'.";
            var span = new TextSpan(position, 1);
            Report(span, message);
        }

        public void ReportUnexpectedToken(TextSpan currentSpan, SyntaxKind actualKind, SyntaxKind expectedKind)
        {
            var message = $"Unexpected token: <{actualKind}>, Expected: <{expectedKind}>.";
        }

        public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
        {
            var message = $"Unary operator '{operatorText}' is not defined for type '{operandType}'.";
            Report(span, message);
        }

        public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, Type leftType, Type rightType)
        {
            var message = $"Binary operator '{operatorText}' is not defined for types '{leftType}' and '{rightType}'.";
            Report(span, message);
        }
    }
}
