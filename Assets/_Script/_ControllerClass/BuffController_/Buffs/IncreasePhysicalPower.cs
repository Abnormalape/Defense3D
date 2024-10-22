using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class IncreasePhysicalPower : BuffController
    {
        public override int BuffID { get; set; } = 1;

        public override void ExecuteBuff()
        {
            Debug.Log("Buff Executed.");
        }

        public override void ExecuteBuff(NpcBaseStat CurrentStat)
        {
            Debug.Log("Buff Executed.");
        }

        public override void ExecuteBuff(IBuffHolder trigger, IBuffHolder opponent)
        {
            Debug.Log("Buff Executed.");
        }

        public override void ExecuteBuff(NPCController_ actor, NPCController_ opponent)
        {
            Debug.Log("Buff Executed.");
        }
    }
}

