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
    }
}