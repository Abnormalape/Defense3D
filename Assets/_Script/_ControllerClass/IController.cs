using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.Controller
{
    public interface IController
    {
        public IManagerClass OwnerManager { get; set; }


        public void ControllerInitializer(IManagerClass ownerManager);
    }
}