namespace BHSSolo.DungeonDefense.Controller
{
    public interface IStatHolder
    {
        public int maxLevel { get; protected set; }
        public int level { get; protected set; }
        public NpcBaseStat NpcBaseStat { get; protected set; }
    }

    public class NpcBaseStat { } //Todo: 옮겨야 함
}