namespace BHSSolo.DungeonDefense.Controller
{
    public class NpcStatModifier
    {
        public NpcStatModifier(NpcStat npcStat, float statModifyValue)
        {
            NpcStat = npcStat;
            StatModifyValue = statModifyValue;
        }


        public NpcStat NpcStat { get; private set; }
        public float StatModifyValue { get; private set; }
    }
}
