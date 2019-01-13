using System;

namespace EightQueensGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new Controller();
            controller.Progress = PrintBoard;
            var winner = controller.Start(typeof(Human));
            PrintResult(winner);
        }

        private IPlayer DecideFirstPlayer()
        {
            var b = Confirm("Are you first?");
            if (b)
            {
                return new Human();
            }
            else
            {
                return new Computer();
            }
        }

        private static bool Confirm(string message)
        {
            Console.Write(message);
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            try
            {
                while (true)
                {
                    var key = Console.ReadKey();
                    if (key.KeyChar == 'y')
                        return true;
                    if (key.KeyChar == 'n')
                        return false;
                    Console.CursorLeft = left;
                    Console.CursorTop = top;
                }
            }
            finally
            {
                Console.WriteLine();
            }
        }


        private static void PrintResult(IPlayer winner)
        {
            if (winner is Human)
                Console.WriteLine("You are winner");
            else
                Console.WriteLine("you are loser");
            Console.ReadKey();
        }


        private static void PrintBoard(Board board)
        {
            Console.Clear();
            //Console.WriteLine("  1 2 3 4 5 6 7 8 ");
            //Console.WriteLine("  ----------------");
            for (int y = 1; y <= 8; y++)
            {
                Console.Write($"{y}|");
                for (int x = 1; x <= 8; x++)
                {
                    var val = board[x, y].Value;
                    if (val == '_')
                        val = '.';
                    Console.Write(val + " ");
                }
                Console.WriteLine();
            }
        }

    }
}
