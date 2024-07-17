using BHSSolo.DungeonDefense.Singleton;

namespace BHSSolo.DungeonDefense.Management
{
    public static class GameManager
    {
        //Time flows as
        // DayStarted
        // => ManagePhaseStarted => Action => ManagePhaseFinished
        // => BattlePhaseStarted => Action => BattlePhaseFinished
        // => ManagePhaseStarted => Action => ManagePhaseFinished 
        // => DayFinished

        public delegate void ManagePhaseStarted();
        public delegate void ManagePhaseFinished();
        public delegate void BattlePhaseStarted();
        public delegate void BattlePhaseFinished();
        public delegate void DayStarted();
        public delegate void DayFinished();

        public static event ManagePhaseStarted OnManagePhaseStarted;
        public static event ManagePhaseFinished OnManagePhaseFinished;
        public static event BattlePhaseStarted OnBattlePhaseStarted;
        public static event BattlePhaseFinished OnBattlePhaseFinished;
        public static event DayStarted OnDayStarted;
        public static event DayFinished OnDayFinished;

    }
}