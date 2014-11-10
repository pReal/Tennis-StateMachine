using System;

namespace Tennis_StateMachine
{
    public class Game 
    {
        public Player[] Players { get; private set; }
        public Player Server { get; private set; }
        public IScoreState ScoreState { get; set; }
        
        public Game()
        {
            Players = new[]
            {
                new Player("Player1"), 
                new Player("Player2")
            } ;

            ScoreState = new ScoreStateStandard(this);
        }

        public void PointScored(Player player)
        {
            ScoreState.PointScored(player);
            DisplayScore();
            DisplayScoreState();
        }

        public void DisplayScore()
        {
            Console.WriteLine("{0}: {1}    {2}: {3}", Players[0].Name, Players[0].Score, Players[1].Name, Players[1].Score);
        }

        public void DisplayScoreState()
        {
            Console.WriteLine("ScoreState: {0}", this.ScoreState.GetType());
        }

    }

    public enum Score
    {
        Love,
        Fifteen,
        Thirty,
        Forty,
        Win
    }
}
