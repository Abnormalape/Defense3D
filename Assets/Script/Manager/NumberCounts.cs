namespace BHSSolo.DungeonDefense.Management
{
    public static class NumberCounts
    {
        private static int _monsterID = 1;
        public static int MonsterID { get => _monsterID; set => _monsterID++; }
    }
}
