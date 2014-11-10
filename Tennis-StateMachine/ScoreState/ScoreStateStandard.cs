using System.Linq;

namespace Tennis_StateMachine
{
    public class ScoreStateStandard : IScoreState
    {
        public Game Game { get; set; }

        public ScoreStateStandard(Game game)
        {
            Game = game;
        }

        public void PointScored(Player player)
        {  
            player.Score++;
            StateChangeCheck();
        }

        public void StateChangeCheck()
        {
            if (Game.Players.Any(x => x.Score == Score.Win))
            {
                Game.ScoreState = new ScoreStateGameOver(this);
                return;
            }

            var player1 = Game.Players[0];
            var player2 = Game.Players[1];

            if (player1.Score == Score.Forty && player2.Score == Score.Forty)
            {
                Game.ScoreState = new ScoreStateDeuce(this);
            }

        }
    }
}
