using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.Controller
{
    public class AllySample : AllyController_, IController
    {
        
        public IManagerClass OwnerManager { get; set; }
        public override Ally_enum AllyEnum_ { get; set; }
        public override AllyStatus_ AllyStatus_ { get; set; }
        public override AllyManager_ AllyManager_ { get; set; }
        public override int level { get; set; }

        public override void AllyControllerInitializer(AllyStatus_ statusFound)
        {
            
        }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
        }
    }
}
