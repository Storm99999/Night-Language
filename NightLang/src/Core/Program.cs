// See https://github.com/Storm9999/NightLang for more info 
using System;

namespace NightL
{
    class Program
    {
        public static Lexer lexer;
        public static Parser parser;
        public static Evaluator evaluator;
        public static Binder binder;
        public static bool ShowTree = false;
        public static void Main(string[] args)
        {
            while (true)
            {
                 Console.WriteLine(">>>");
                 var str = Console.ReadLine();
                 if (string.IsNullOrWhiteSpace(str))
                   return;
                   
                if (str == "-settings parser enableTree")
                {
                    Console.WriteLine("[LOG]: Enabled tree view.");
                    ShowTree = true;
                    continue;
                }
                if (str == "-settings parser disableTree")
                {
                    Console.WriteLine("[LOG]: Disabled tree view.");
                    ShowTree = false;
                    continue;
                }
                if (str == "-con clear")
                {
                    Console.Clear();
                    continue;
                }
                
                   parser = new Parser(str);
                   lexer = new Lexer(str);
                   binder = new Binder();
                   var syntaxTree = parser.Parse();
                   var boundexpress = binder.Bind(syntaxTree.rootex);

                   Print(syntaxTree.rootex);
                   var errorCollection = syntaxTree.errors.Concat(binder.ErrorEnum).ToArray();
                   if (!errorCollection.Any())
                   {
                       evaluator = new Evaluator(boundexpress);
                       var res = evaluator.Evaluate();
                       Console.WriteLine(res);
                   }
                   else
                   {
                       Console.ForegroundColor = ConsoleColor.DarkRed;
                       foreach (var errors in syntaxTree.errors)
                       {
                           Console.WriteLine(errors);
                       }
                       Console.ResetColor();
                   }
            }
        }
        static void Print(SyntaxNode node, string indent = "", bool isLast = true)
        {
            if (ShowTree == true)
            {
            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);

            if (node is SyntaxToken t && t.Val != null)
            {
                Console.Write(" ");
                Console.Write(t.Val);
            }
            // indent += "    ";
            Console.WriteLine();
            indent += isLast ?  "    " : "│    ";
            
            var lastExpress = node.GetChilds().LastOrDefault();
            
            foreach (var child in node.GetChilds())
            {
                Print(child, indent, child == lastExpress);
            }
        }
    }
  }
}