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
        public float Blood;
        public float BloodScaling;
        public float Mental;
        public float MentalScaling;
        public float Physical;
        public float PhysicalScaling;
        public float Special;
        public float SpecialScaling;
        public float PhysicalPower;
        public float PhysicalPowerScaling;
        public float SpecialPower;
        public float SpecialPowerScaling;
        public float PhysicalResist;
        public float PhysicalResistScaling;
        public float SpecialResist;
        public float SpecialResistScaling;
        public float Speed;
        public float SpeedScaling;
        public float ReactSpeed;
        public float ReactSpeedScaling;
    }

    [System.Serializable]
    public class AllyBaseStatus
    {
        public NpcBaseStatus this[int AllyId]
        {
            get { return AllyStatusList[AllyId]; }
        }

        public List<NpcBaseStatus> AllyStatusList;
    }

    [System.Serializable]
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
