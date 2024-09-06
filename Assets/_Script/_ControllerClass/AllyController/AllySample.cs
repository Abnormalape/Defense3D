using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.Controller
{
    public class AllySample : AllyController_, IController
    {
        public override AllyStatus_ AllyStatus_ { get; set; } = new();
        public IManagerClass OwnerManager { get; set; }
        public override Ally_enum AllyEnum_ { get; set; }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
        }
    }
}
