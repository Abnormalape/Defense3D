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
                    case 1: return Blood;
                    case 2: return BloodScaling;
                    case 3: return Mental;
                    case 4: return MentalScaling;
                    case 5: return Physical;
                    case 6: return PhysicalScaling;
                    case 7: return Special;
                    case 8: return SpecialScaling;
                    case 9: return PhysicalPower;
                    case 10: return PhysicalPowerScaling;
                    case 11: return SpecialPower;
                    case 12: return SpecialPowerScaling;
                    case 13: return PhysicalResist;
                    case 14: return PhysicalResistScaling;
                    case 15: return SpecialResist;
                    case 16: return SpecialResistScaling;
                    case 17: return Speed;
                    case 18: return SpeedScaling;
                    case 19: return ReactSpeed;
                    case 20: return ReactSpeedScaling;
                }
                Debug.LogError("Incorrect Stat ID");
                return 0;
            }
        }
        public NpcBaseStatus()//NpcBaseStatus inputStatus)
        {
            //this.NPCID = inputStatus.NPCID;
            //this.Race = inputStatus.Race;
            //this.Blood = inputStatus.Blood;
            //this.BloodScaling = inputStatus.BloodScaling;
            //this.Mental = inputStatus.Mental;
            //this.MentalScaling = inputStatus.MentalScaling;
            //this.Physical = inputStatus.Physical;
            //this.PhysicalScaling = inputStatus.PhysicalScaling;
            //this.Special = inputStatus.Special;
            //this.SpecialScaling = inputStatus.SpecialScaling;
            //this.PhysicalPower = inputStatus.PhysicalPower;
            //this.PhysicalPowerScaling = inputStatus.PhysicalPowerScaling;
            //this.SpecialPower = inputStatus.SpecialPower;
            //this.SpecialPowerScaling = inputStatus.SpecialPowerScaling;
            //this.PhysicalResist = inputStatus.PhysicalResist;
            //this.PhysicalResistScaling = inputStatus.PhysicalResistScaling;
            //this.SpecialResist = inputStatus.SpecialResist;
            //this.SpecialResistScaling = inputStatus.SpecialResistScaling;
            //this.Speed = inputStatus.Speed;
            //this.SpeedScaling = inputStatus.SpeedScaling;
            //this.ReactSpeed = inputStatus.ReactSpeed;
            //this.ReactSpeedScaling = inputStatus.ReactSpeedScaling;
            //this.MaxLevel = inputStatus.MaxLevel;

            //this.TraitIDs = inputStatus.TraitIDs;
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
        None = 0, //Error
        Blood,
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
