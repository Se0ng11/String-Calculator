using System.Data;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input value");
            var value = Console.ReadLine();

            if (value != null)
            {
                Console.WriteLine(" this is something " + Calculate(value));
            }
            else
            {
                Console.WriteLine("value cannot be empty");
            }

        }

        private static double Calculate(string n)
        {

            var ary = n.ToCharArray();

            for (var i = 0; i <= ary.Length - 1; i++)
            {
                var charAry = ary[i];
                if (char.IsWhiteSpace(charAry) != true){
                    Console.Write(charAry);
                }

            }


            return 0;
        }

        private static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

    }

}