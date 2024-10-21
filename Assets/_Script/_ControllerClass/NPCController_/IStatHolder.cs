namespace BHSSolo.DungeonDefense.Controller
{
    public interface IStatHolder
    {
        public int maxLevel { get; set; }
        public int level { get; set; }
        public NpcBaseStat NpcBaseStat { get; set; }

        public NpcBaseStat CurrentResourceStat { get; set; }
        public NpcBaseStat CurrentFinalStat { get; set; }

        public delegate void ResourceStatEvent(NpcBaseStat CurrentResourceStat); //현재 체력 등을 넣고 실행
        public delegate void AbilityStatEvent(NpcBaseStat CurrentFinalStat); //현재 스탯을 넣고 실행
        public event ResourceStatEvent OnCurrentResourceStatModified;
        public event AbilityStatEvent OnFinalAbilityStatModified;
    }

}