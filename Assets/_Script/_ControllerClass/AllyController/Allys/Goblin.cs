
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class Goblin : AllyController_
    {
        public override int Ally_ID { get; set; } = 1;

        private void Start()
        {
            Debug.Log($"Goblin HP: {NpcBaseStat[NpcStat.Blood]}");
        }
    }
}
