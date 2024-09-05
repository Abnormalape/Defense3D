using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.Controller
{
    public class UIControllerSample : UIController_, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override UIManager_ UIManager_ { get; set; }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            UIManager_ = (UIManager_)OwnerManager;
        }
    }
}