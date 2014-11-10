using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis_StateMachine
{
    public class ScoreStateGameOver : IScoreState
    {
        public Game Game { get; set; }

        public ScoreStateGameOver(Game game)
        {
            Game = game;
        }

        public ScoreStateGameOver(IScoreState scoreState)
        {
            Game = scoreState.Game;
        }

        public void PointScored(Player player)
        {
            
        }

        public void StateChangeCheck()
        {
            
        }
    }
}
