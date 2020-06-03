//  Proplex

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Proplex.Binding;
using Proplex.Syntax;

namespace Proplex
{
    class Program
    {
        static void Main(string[] args)
        {
            var showTree = true;
            while(true)
            {
                Console.WriteLine(">");
                var line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                    return;

                if(Commands(line, ref showTree))
                {
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);
                var binder = new Binder();
                var boundExpression = binder.BindExpression(syntaxTree.Root);

                var diagnostics = syntaxTree.Diagnostics.Concat(binder.Diagnostics);

                if(showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                
                if(!diagnostics.Any())
                {
                    var evaluator = new Evaluator.Evaluator(boundExpression);
                    var result = evaluator.Evaluate();
                    Console.WriteLine(result);
                    continue;
                }

                PrintErrors(syntaxTree);
            }
        }

        private static bool Commands(string line, ref bool showTree)
        {
            if(line == "#st")
            {
                showTree = !showTree;
                return true;
            }

            if(line != "#cls")
            {
                return false;
            }

            Console.Clear();
            return true;

        }

        private static void PrintErrors(SyntaxTree syntaxTree)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            foreach(string error in syntaxTree.Diagnostics)
            {
                Console.WriteLine(error);
            }

            Console.ResetColor();
        }

        static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if(node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }

            Console.WriteLine();

            indent += isLast ? "    " : "│    ";

            if (!node.GetChildren().Any())
            {
                return;
            }

            var lastChild = node.GetChildren().LastOrDefault();

            foreach(var child in node.GetChildren())
            {
                PrettyPrint(child, indent,child == lastChild);
            }
        }
    }
}

