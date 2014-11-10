using System.Collections.Generic;
using System.Linq;

namespace Tennis_StateMachine
{
    public class ScoreStateDeuce : IScoreState
    {
        public Game Game { get; set; }
        private IDictionary<WinLose,Player> _players = new Dictionary<WinLose, Player>();

        public ScoreStateDeuce(Game game)
        {
            Game = game;
        }

        public ScoreStateDeuce(IScoreState scoreState)
        {
            Game = scoreState.Game;
        }
       
        public void PointScored(Player player)
        {
            _players.Add(WinLose.Win,player);

            var opposingPlayer = Game.Players.Single(x => !ReferenceEquals(x, player));
            _players.Add(WinLose.Lose, opposingPlayer);
            
            player.Advantage = true;
            _players[WinLose.Lose].Advantage = false;

            StateChangeCheck();
        }

        public void StateChangeCheck()
        {
            Game.ScoreState = new ScoreStateAdvantage(this);
        }

        private enum WinLose
        {
            Lose,
            Win
        }
    }


}
