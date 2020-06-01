//  Proplex

using Proplex.Core.Evaluator;
using System;
using System.Linq;
using System.Reflection.Metadata;
using Proplex.Core.Nodes;

namespace Proplex
{
    class Program
    {
        static void Main(string[] args)
        {
            bool showTree = true;
            while(true)
            {
                Console.WriteLine(">");
                var line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                    return;

                if(line == "#st")
                {
                    showTree = !showTree;
                    continue;
                }

                if(line == "#cls")
                {
                    Console.Clear();
                    continue;
                }

                var syntaxTree = SyntaxTree.Parse(line);

                var colour = Console.ForegroundColor;

                if(showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ForegroundColor = colour;
                }

                if(!syntaxTree.Diagnostics.Any())
                {
                    var evaluator = new Evaluator(syntaxTree.Root);
                    var result = evaluator.Evaluate();
                    Console.WriteLine(result);
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;

                foreach(string error in syntaxTree.Diagnostics)
                {
                    Console.WriteLine(error);
                }

                Console.ForegroundColor = colour;
            }
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

