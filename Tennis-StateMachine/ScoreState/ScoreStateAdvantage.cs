using System.Linq;

namespace Tennis_StateMachine
{
    public class ScoreStateAdvantage : IScoreState
    {
        public Game Game { get; set; }
        private bool _gameIsOver;

        public ScoreStateAdvantage(Game game)
        {
            Game = game;
        }

        public ScoreStateAdvantage(IScoreState scoreState)
        {
            Game = scoreState.Game;
        }

        public void PointScored(Player player)
        {
            if (player.Advantage)
            {
                player.Score = Score.Win;
                _gameIsOver = true;
            }
            else
            {
                player.Advantage = false;

                var opposingPlayer = Game.Players.Single(x => !ReferenceEquals(x, player));
                opposingPlayer.Advantage = false;
            }

            StateChangeCheck();
        }

        public void StateChangeCheck()
        {
            if (_gameIsOver)
            {
                Game.ScoreState = new ScoreStateGameOver(this);
            }
            else
            {
                Game.ScoreState = new ScoreStateDeuce(this);
            }
        }

    }
}
