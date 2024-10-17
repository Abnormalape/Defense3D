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

        public readonly int NPCID;
        public readonly string Race;
        public readonly int MaxLevel;
        public readonly int[] TraitIDs;

        public readonly float Blood;
        public readonly float BloodScaling;
        public readonly float Mental;
        public readonly float MentalScaling;
        public readonly float Physical;
        public readonly float PhysicalScaling;
        public readonly float Special;
        public readonly float SpecialScaling;
        public readonly float PhysicalPower;
        public readonly float PhysicalPowerScaling;
        public readonly float SpecialPower;
        public readonly float SpecialPowerScaling;
        public readonly float PhysicalResist;
        public readonly float PhysicalResistScaling;
        public readonly float SpecialResist;
        public readonly float SpecialResistScaling;
        public readonly float Speed;
        public readonly float SpeedScaling;
        public readonly float ReactSpeed;
        public readonly float ReactSpeedScaling;
    }

    public class AllyBaseStatus
    {
        public NpcBaseStatus this[int AllyId]
        {
            get { return AllyStats[AllyId]; }
        }

        private List<NpcBaseStatus> AllyStats;
    }

    public class EnemyBaseStatus
    {
        public NpcBaseStatus this[int EnemyId]
        {
            get { return EnemyStats[EnemyId]; }
        }

        private List<NpcBaseStatus> EnemyStats;
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
