using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    [System.Serializable]
    public class NpcBaseStatus
    {
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Blood;
                    case 1: return BloodScaling;
                    case 2: return Mental;
                    case 3: return MentalScaling;
                    case 4: return Physical;
                    case 5: return PhysicalScaling;
                    case 6: return Special;
                    case 7: return SpecialScaling;
                    case 8: return PhysicalPower;
                    case 9: return PhysicalPowerScaling;
                    case 10: return SpecialPower;
                    case 11: return SpecialPowerScaling;
                    case 12: return PhysicalResist;
                    case 13: return PhysicalResistScaling;
                    case 14: return SpecialResist;
                    case 15: return SpecialResistScaling;
                    case 16: return Speed;
                    case 17: return SpeedScaling;
                    case 18: return ReactSpeed;
                    case 19: return ReactSpeedScaling;
                }
                Debug.LogError("Incorrect Stat ID");
                return 0;
            }
        }

        public int NPCID;
        public string Race;
        public int MaxLevel;
        public int[] TraitIDs;

        public float Blood { get; private set; }
        public float BloodScaling { get; private set; }
        public float Mental { get; private set; }
        public float MentalScaling { get; private set; }
        public float Physical { get; private set; }
        public float PhysicalScaling { get; private set; }
        public float Special { get; private set; }
        public float SpecialScaling { get; private set; }
        public float PhysicalPower { get; private set; }
        public float PhysicalPowerScaling { get; private set; }
        public float SpecialPower { get; private set; }
        public float SpecialPowerScaling { get; private set; }
        public float PhysicalResist { get; private set; }
        public float PhysicalResistScaling { get; private set; }
        public float SpecialResist { get; private set; }
        public float SpecialResistScaling { get; private set; }
        public float Speed { get; private set; }
        public float SpeedScaling { get; private set; }
        public float ReactSpeed { get; private set; }
        public float ReactSpeedScaling { get; private set; }
    }

    public class AllyBaseStatus
    {
        public NpcBaseStatus this[int AllyId]
        {
            get { return AllyStatusList[AllyId]; }
        }

        public List<NpcBaseStatus> AllyStatusList;
    }

    public class EnemyBaseStatus
    {
        public NpcBaseStatus this[int EnemyId]
        {
            get { return EnemyStatusList[EnemyId]; }
        }

        public List<NpcBaseStatus> EnemyStatusList;
    }

    public enum NpcStat
    {
        None = -1, //Error
        Blood = 0,
        BloodScaling,
        Mental,
        MentalScaling,
        Physical,
        PhysicalScaling,
        Special,
        SpecialScaling,
        PhysicalPower,
        PhysicalPowerScaling,
        SpecialPower,
        SpecialPowerScaling,
        PhysicalResist,
        PhysicalResistScaling,
        SpecialResist,
        SpecialResistScaling,
        Speed,
        SpeedScaling,
        ReactSpeed,
        ReactSpeedScaling,
    }
}
