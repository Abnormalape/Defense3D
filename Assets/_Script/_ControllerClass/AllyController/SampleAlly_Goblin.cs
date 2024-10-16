using BHSSolo.DungeonDefense.ManagerClass;
using System.Diagnostics;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SampleAlly_Goblin : AllyController_, IController, IAllyStatusModifier
    {
        public override NPCCurrentStatus AllyStatus_ { get; set;}
        public override NpcBaseStatus AllyBaseStatus_ { get; set;}
        public override AllyType AllyEnum_ { get; set;}
        public override AllyManager_ AllyManager_ { get; set;}
        public override int level { get; set;}
        public override int Ally_ID { get; set;}
        public IManagerClass OwnerManager { get; set;}
        public AllyStatusModifier AllyStatusModifier { get; set; }
        public override BattleManager_ BattleManager_ { get; set; }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            
        }

        public override void InitializeAllyController()
        {

        }
    }
}
