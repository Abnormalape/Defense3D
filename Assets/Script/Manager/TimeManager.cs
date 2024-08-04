using UnityEngine;
using BHSSolo.DungeonDefense.Singleton;

namespace BHSSolo.DungeonDefense.Management
{
    public class TimeManager : Singleton<TimeManager>
    {
        float passedTime;
        float minute;
        float hour;
        float date;


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