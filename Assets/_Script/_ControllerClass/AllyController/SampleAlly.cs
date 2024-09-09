using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SampleAlly : AllyController_, IController, IAllyStatusModifier
    {
        public override AllyStatus_ AllyStatus_ { get; set; }
        public override Ally_enum AllyEnum_ { get; set; } = Ally_enum.Sample; //Manager use this???
        public IManagerClass OwnerManager { get; set; }
        public AllyStatusModifier AllyStatusModifier { get; set; }
        public override AllyManager_ AllyManager_ { get; set; }
        public override int level { get; set; }


        /// <summary>
        /// Manager runs this.
        /// </summary>
        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            AllyManager_ = OwnerManager as AllyManager_;
        }

        /// <summary>
        /// Manager runs this.
        /// </summary>
        public override void AllyControllerInitializer(AllyStatus_ statusFound)
        {
            AllyStatusModifier = new(this);
            AllyStatus_ = statusFound;
        }
    }
}
