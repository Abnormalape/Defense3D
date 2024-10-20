namespace BHSSolo.DungeonDefense.Controller
{
    public interface IStatHolder
    {
        public int maxLevel { get; set; }
        public int level { get; set; }
        public NpcBaseStat NpcBaseStat { get; set; }


        public delegate void ResourceStatEvent(NpcStatModifier resourceStatModifier);
        public delegate void AbilityStatEvent(NpcStatModifier abilityStatModifier);
        public event ResourceStatEvent OnCurrentResourceStatModified;
        public event AbilityStatEvent OnFinalAbilityStatModified;
    }

}