using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tennis_StateMachine;

namespace TennisTests
{
    [TestClass]
    public class GameTests
    {
        private Game _game;
        private Player _player1;
        private Player _player2;

        [TestInitialize]
        public void Initialize()
        {
            _game = new Game();
            _player1 = _game.Players[0];
            _player2 = _game.Players[1];
        }

        [TestMethod]
        public void GameInstantiated_Has2Players()
        {
            Assert.AreEqual(2, _game.Players.Length);
        }

        [TestMethod]
        public void GameInstantiated_ConfirmPlayer1Name()
        {
            Assert.AreEqual("Player1", _player1.Name);
        }

        [TestMethod]
        public void GameInstantiated_ConfirmPlayer2Name()
        {
            Assert.AreEqual("Player2", _player2.Name);
        }

        [TestMethod]
        public void PointScored_Player1Love_Player2Love_Score15Love()
        {
            _game.PointScored(_player1);

            Assert.AreEqual(Score.Fifteen, _player1.Score);
        }

        [TestMethod]
        public void PointScored_Player115_Player2Love_Score30Love()
        {
            _player1.Score = Score.Fifteen;
            _game.PointScored(_player1);

            Assert.AreEqual(Score.Thirty, _player1.Score);
        }

        [TestMethod]
        public void PointScored_Player130_Player2Love_Score40Love()
        {
            _player1.Score = Score.Thirty;
            _game.PointScored(_player1);

            Assert.AreEqual(Score.Forty, _player1.Score);
        }

        [TestMethod]
        public void PointScored_Player140_Player2Love_Player1Win()
        {
            _player1.Score = Score.Forty;
            _game.PointScored(_player1);

            Assert.AreEqual(Score.Win, _player1.Score);
            Assert.IsInstanceOfType(_game.ScoreState, typeof(ScoreStateGameOver));
        }

        [TestMethod]
        public void PointScored_30_40_Player1Scores_ScoreStateIsDeuce()
        {
            _player1.Score = Score.Thirty;
            _player2.Score = Score.Forty;

            _game.PointScored(_player1);

            Assert.IsInstanceOfType(_game.ScoreState,typeof(ScoreStateDeuce));
        }

        [TestMethod]
        public void PointScored_40_40_Player1Scores_ScoreStateAdvantage()
        {
            Setup_Game_ScoreStateDeuce();

            _game.PointScored(_player1);

            Assert.IsInstanceOfType(_game.ScoreState, typeof(ScoreStateAdvantage));
            Assert.AreEqual(true,_player1.Advantage);
        }

        [TestMethod]
        public void PointScored_Advantage_40_Player1Scores_ScoreStateGameOver()
        {
            _player1.Score = Score.Forty;
            _player1.Advantage = true;

            _player2.Score = Score.Forty;
            
            _game.ScoreState = new ScoreStateAdvantage(_game);

            _game.PointScored(_player1);

            Assert.IsInstanceOfType(_game.ScoreState,typeof(ScoreStateGameOver));
            Assert.AreEqual(Score.Win,_player1.Score);
            
        }

        [TestMethod]
        public void PointScored_Advantage_Scenarios()
        {
            Setup_Game_ScoreStateDeuce();

            var Player1 = _game.Players[0];
            var Player2 = _game.Players[1];

            //Player 1 Scores
            _game.PointScored(Player1);

            //Score State = Advantage
            Assert.IsInstanceOfType(_game.ScoreState,typeof(ScoreStateAdvantage));

            //Player 1 Advantage == true
            Assert.AreEqual(true,Player1.Advantage);

            //Player 2 Advantage == false
            Assert.AreEqual(false, Player2.Advantage);

            //Player 1 <40> Player 2 <40>
            ValidateScores(player1Score: Score.Forty, player2Score: Score.Forty);

            //Player 2 Scores
            _game.PointScored(_player2);

            //Score State = Deuce
            Assert.IsInstanceOfType(_game.ScoreState, typeof(ScoreStateDeuce));

            //Player 1 Advantage == false
            Assert.AreEqual(false, _player1.Advantage);

            //Player 2 Advantage == false
            Assert.AreEqual(false, _player2.Advantage);

            //Player 1 <40> Player 2 <40>
            ValidateScores(player1Score: Score.Forty, player2Score:Score.Forty);

            //Player 2 Scores
            _game.PointScored(_player2);

            //Score State == Advantage
            Assert.IsInstanceOfType(_game.ScoreState, typeof(ScoreStateAdvantage));

            //Player 1 Advantage == false
            Assert.AreEqual(false, _player1.Advantage);

            //Player 2 Advantage == true
            Assert.AreEqual(true, _player2.Advantage);

            //Player 1 <40> Player 2 <40>
            ValidateScores(player1Score: Score.Forty, player2Score: Score.Forty);

            //Player 1 Scores
            _game.PointScored(_player1);

            //Score State = Deuce
            Assert.IsInstanceOfType(_game.ScoreState, typeof(ScoreStateDeuce));

            //Player 1 Advantage == false
            Assert.AreEqual(false, _player1.Advantage);

            //Player 2 Advantage == false
            Assert.AreEqual(false, _player2.Advantage);

            //Player 1 <40> Player 2 <40>
            ValidateScores(player1Score: Score.Forty, player2Score: Score.Forty);

            //Player 1 Scores
            _game.PointScored(_player1);

            //Player 1 Advantage == false
            Assert.AreEqual(true, _player1.Advantage);

            //Player 2 Advantage == false
            Assert.AreEqual(false, _player2.Advantage);

            //Score State == Advantage
            Assert.IsInstanceOfType(_game.ScoreState, typeof(ScoreStateAdvantage));

            //Player 1 <40> Player 2 <40>
            ValidateScores(player1Score: Score.Forty, player2Score: Score.Forty);

            //Player 1 Scores
            _game.PointScored(_player1);

            //Score State = GameOver
            Assert.IsInstanceOfType(_game.ScoreState, typeof(ScoreStateGameOver));

            //Player 1 <Win> Player 2 <40>
            ValidateScores(player1Score: Score.Win, player2Score: Score.Forty);
        }

        private void ValidateScores(Score player1Score, Score player2Score)
        {
            Assert.AreEqual(player1Score, _player1.Score);
            Assert.AreEqual(player2Score, _player2.Score);
        }

        private void Setup_Game_ScoreStateDeuce()
        {
            _game.Players[0].Score = Score.Forty;
            _game.Players[1].Score = Score.Forty;
            _game.ScoreState = new ScoreStateDeuce(_game);
        }
    }
}
