using System;
using System.Linq;
using System.Reflection.Metadata;
using Proplex.Lexer;
using Proplex.Lexer.Nodes;

namespace Proplex
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine(">");
                var line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                    return;

                var parser = new Parser.Parser(line);
                var expression = parser.Parse();

                var colour = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGray;

                PrettyPrint(expression);

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

