using System.Text.RegularExpressions;

namespace HelloWorld
{
    class Program
    {
        private static string[] _oprAry = {"+", "-", "*", "/"};
        static void Main(string[] args)
        {
            int counter = 0;
            foreach (string line in System.IO.File.ReadLines(@"./question.txt"))
            {
                Console.WriteLine($"Question: {line}");
                Console.WriteLine($"Answer: {Test(line)}");
                Console.WriteLine();
                counter++;
            }
        }

        private static double Calculate(string n)
        {
            var ary = n.Replace(" ", "");
            return StartProcess(ary);
        }

        private static double StartProcess(string str)
        {
            List<string> arr = Regex
                .Matches(str, @"\d+\.?\d*|\+|\-|\*|\/|\(|\)")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();

            while (arr.Contains("("))
            {
                int start = arr.LastIndexOf("(");
                int end = arr.IndexOf(")", start);

                List<string> subArr = arr.GetRange(start + 1, end - start - 1);

                string subResult =  Evaluate(Parse(subArr)).ToString();

                arr.RemoveRange(start, end - start + 1);
                arr.Insert(start, subResult);
            }

            return  Evaluate(Parse(arr));
        }

        static double Evaluate(List<string> tokens)
        {
            Stack<double> operands = new Stack<double>();
            Stack<string> operators = new Stack<string>();
            foreach (var token in tokens)
            {
                if (double.TryParse(token, out double num))
                {
                    operands.Push(num);
                }
                else if (_oprAry.Contains(token))
                {
                    while (operators.Count != 0 && Precedence(operators.Peek()) >= Precedence(token))
                    {
                        applyOp(operands, operators);
                    }
                    operators.Push(token);
                }
                else
                {
                    throw new Exception("Invalid token: " + token);
                }
            }
            while (operators.Count != 0)
            {
                applyOp(operands, operators);
            }
            return operands.Pop();
        }
        static List<string> Parse(List<string> tokens)
        {
            List<string> parsedTokens = new List<string>();
            int i = 0;
            while (i < tokens.Count)
            {
                if (double.TryParse(tokens[i], out double num))
                {
                    parsedTokens.Add(num.ToString());
                    i++;
                }
                else if (_oprAry.Contains(tokens[i]))
                {
                    parsedTokens.Add(tokens[i]);
                    i++;
                }
                else
                {
                    i++;
                }
            }

            return parsedTokens;

        }

        static int Precedence(string op)
        {
            switch (op)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                default:
                    throw new Exception("Invalid operator: " + op);
            }
        }

        static void applyOp(Stack<double> operands, Stack<string> operators)
        {
            double b = operands.Pop();
            double a = operands.Pop();
            string op = operators.Pop();

            switch (op)
            {
                case "+":
                    operands.Push(a + b);
                    break;
                case "-":
                    operands.Push(a - b);
                    break;
                case "*":
                    operands.Push(a * b);
                    break;
                case "/":
                    if (b == 0)
                    {
                        throw new Exception("Cannot divide by zero.");
                    }
                    operands.Push(a / b);
                    break;
                default:
                    throw new Exception("Invalid operator: " + op);
            }
        }
    }
}