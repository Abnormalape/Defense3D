namespace BHSSolo.DungeonDefense.Controller
{
    public class NpcStatModifier
    {
        public NpcStatModifier(NpcStat npcStat, float statModifyValue, StatModifierType statModifierType)
        {
            NpcStat = npcStat;
            StatModifyValue = statModifyValue;
            StatModifierType = statModifierType;
        }


        public StatModifierType StatModifierType { get; private set; }
        public NpcStat NpcStat { get; private set; }
        public float StatModifyValue { get; private set; }
    }

    public enum StatModifierType
    {
        Adder,
        Multiplier,
    }
}
