using System;

namespace Checkers_Logic
{
    public class Program
    {
        public static void Main()
        {
            ConsoleInterface checkersGame = new ConsoleInterface();
            checkersGame.CreateNewGame();
        }
    }
}
