using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.Controller
{
    public interface IPlayerController
    {
        public PlayerManager_ playerManager_ { get; set; }


        public void SetPlayerManager();

        public void ReactToInput();
    }
}
