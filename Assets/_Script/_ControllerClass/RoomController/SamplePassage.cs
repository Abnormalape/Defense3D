using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SamplePassage : RoomController, IController
    {
        public IManagerClass OwnerManager { get; set; }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
        }
    }
}
