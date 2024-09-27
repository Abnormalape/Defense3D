namespace BHSSolo.DungeonDefense.ManagerClass
{
    public interface IGameState
    {
        GameStateManager_ GameStateManager_ { get; set; }

        public GameState GameState { get; set; }

        public void InitialzieGameState();
    }
}
