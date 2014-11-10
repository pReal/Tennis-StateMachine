namespace Tennis_StateMachine
{
    public class Player
    {
        public string Name { get; private set; }
        public Score Score { get; set; }
        public bool Advantage { get; set; }
        
        public Player(string name)
        {
            Name = name;
            Score = Score.Love;
            Advantage = false;
        }

    }
}
