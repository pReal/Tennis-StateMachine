namespace Tennis_StateMachine
{
    public interface IScoreState
    {
        Game Game { get; set; }
        
        void PointScored(Player player);

        void StateChangeCheck();

    }
}
