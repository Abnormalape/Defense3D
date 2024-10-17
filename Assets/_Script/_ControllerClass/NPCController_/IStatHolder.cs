namespace BHSSolo.DungeonDefense.Controller
{
    public interface IStatHolder
    {
        public int maxLevel { get; set; }
        public int level { get; set; }
        public NpcBaseStat NpcBaseStat { get; set; }
    }

}