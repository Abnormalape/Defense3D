namespace BHSSolo.DungeonDefense.ManagerClass
{
    public interface IManagerClass
    {
        public GameManager_ GameManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_);
    }
}
