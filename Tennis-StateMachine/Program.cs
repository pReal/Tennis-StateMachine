using System;


namespace Tennis_StateMachine
{
    class Program
    {
        static void Main(string[] args)
        {

            var game = new Game();
            var player1 = game.Players[0];
            var player2 = game.Players[1];

            Console.WriteLine("Welcome To Tennis: {0} vs {1}", player1.Name, player2.Name);

            while (game.ScoreState.GetType() != typeof(ScoreStateGameOver))
            {
                DisplayConsole();

                var userAction = Console.ReadLine();

                switch (userAction)
                {
                    case "1":
                        game.PointScored(player1);
                        break;
                    case "2":
                        game.PointScored(player2);
                        break;
                    default:
                        Console.WriteLine("Not a valid action.");
                        break;
                }

            }

            Console.WriteLine("Game Over!");
            Console.ReadLine();

        }

        static void DisplayConsole()
        {
            Console.WriteLine("{0}1.) Player 1 Score    2.) Player 2 Score",Environment.NewLine);
        }
    }
}
