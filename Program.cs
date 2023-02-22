namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            foreach (string line in System.IO.File.ReadLines(@"./question.txt"))
            {
                Console.WriteLine($"Question: {line}");
                Console.WriteLine();
                Console.WriteLine($"Answer: {Calculate(line)}");
                Console.WriteLine();
                counter++;
            }
        }

        private static double Calculate(string n)
        {
            var ary = n.ToCharArray();
            var combine = "";
            for (var i = 0; i <= ary.Length - 1; i++)
            {
                var charAry = ary[i];
                //ignore whitespace
                if (char.IsWhiteSpace(charAry) != true)
                {
                    combine += charAry;
                }

            }
            Console.WriteLine(combine);

            return 0;
        }


        private static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

    }

}