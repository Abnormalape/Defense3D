using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.Controller
{
    public class AllySample : AllyController__, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override AllyManager_ AllyManager_ { get; set; }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            AllyManager_ = OwnerManager as AllyManager_;
        }
    }
}
