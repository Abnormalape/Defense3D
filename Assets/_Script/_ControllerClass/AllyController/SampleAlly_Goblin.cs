using BHSSolo.DungeonDefense.ManagerClass;
using System.Diagnostics;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SampleAlly_Goblin : AllyController_, IController, IAllyStatusModifier
    {
        public override AllyStatus_ AllyStatus_ { get; set; }
        public override AllyBaseStatus AllyBaseStatus_ { get; set; }
        public override AllyType AllyEnum_ { get; set; } = AllyType.Goblin;
        public IManagerClass OwnerManager { get; set; }
        public AllyStatusModifier AllyStatusModifier { get; set; }
        public override AllyManager_ AllyManager_ { get; set; }
        public override int level { get; set; }
        public override int Ally_ID { get; set; }


        /// <summary>
        /// Manager runs this.
        /// </summary>
        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            AllyManager_ = OwnerManager as AllyManager_;
            UnityEngine.Debug.Log("I am Goblin");
        }

        /// <summary>
        /// Ally should have its status Data when initialize
        /// </summary>
        /// <param name="statusFound">Data which each ally need.</param>
        public override void AllyControllerInitializer(AllyBaseStatus statusFound)
        {
            AllyStatusModifier = new(this);
            AllyBaseStatus_ = statusFound; //Base Status, Please Initiate CurrentStatus
        }

        private float passedTime =0f;
        private void Update()
        {

        }
    }
}
