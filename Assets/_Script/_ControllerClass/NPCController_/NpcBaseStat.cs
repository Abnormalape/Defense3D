using Unity.VisualScripting;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class NpcBaseStat
    {
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 1: return Blood;
                    case 2: return PersonalBloodScaling;
                    case 3: return Mental;
                    case 4: return PersonalMentalScaling;
                    case 5: return Physical;
                    case 6: return PersonalPhysicalScaling;
                    case 7: return Special;
                    case 8: return PersonalSpecialScaling;
                    case 9: return PhysicalPower;
                    case 10: return PersonalPhysicalPowerScaling;
                    case 11: return SpecialPower;
                    case 12: return PersonalSpecialPowerScaling;
                    case 13: return PhysicalResist;
                    case 14: return PersonalPhysicalResistScaling;
                    case 15: return SpecialResist;
                    case 16: return PersonalSpecialResistScaling;
                    case 17: return Speed;
                    case 18: return PersonalSpeedScaling;
                    case 19: return ReactSpeed;
                    case 20: return PersonalReactSpeedScaling;
                }
                return 0;
            }
            set
            {
                switch (index)
                {
                    case 1: Blood = value; return;
                    case 2: PersonalBloodScaling = value; return;
                    case 3: Mental = value; return;
                    case 4: PersonalMentalScaling = value; return;
                    case 5: Physical = value; return;
                    case 6: PersonalPhysicalScaling = value; return;
                    case 7: Special = value; return;
                    case 8: PersonalSpecialScaling = value; return;
                    case 9: PhysicalPower = value; return;
                    case 10: PersonalPhysicalPowerScaling = value; return;
                    case 11: SpecialPower = value; return;
                    case 12: PersonalSpecialPowerScaling = value; return;
                    case 13: PhysicalResist = value; return;
                    case 14: PersonalPhysicalResistScaling = value; return;
                    case 15: SpecialResist = value; return;
                    case 16: PersonalSpecialResistScaling = value; return;
                    case 17: Speed = value; return;
                    case 18: PersonalSpeedScaling = value; return;
                    case 19: ReactSpeed = value; return;
                    case 20: PersonalReactSpeedScaling = value; return;
                }
                Debug.LogError("Incorrect Stat ID");
            }
        }

        public NpcBaseStat(NpcBaseStatus npcBaseStatus, int currentLevel = 1)
        {
            CurrentLevel = currentLevel;
            //개체치 적용.

            for (int i = 1; i <= 20; i += 2)
            {
                this[i] = npcBaseStatus[i];
            }

            for (int i = 2; i <= 20; i += 2)
            {
                float personal = Random.Range(0.8f, 1.2f);
                this[i] = npcBaseStatus[i] * personal;
            }

            for (int i = 1; i <= 20; i += 2)
            {
                this[i] = this[i] + (this[i + 1] * CurrentLevel);
            }
        }

        public int NPCID;
        public string Race;
        public int CurrentLevel;

        public float Blood;
        public float PersonalBloodScaling;
        public float Mental;
        public float PersonalMentalScaling;
        public float Physical;
        public float PersonalPhysicalScaling;
        public float Special;
        public float PersonalSpecialScaling;
        public float PhysicalPower;
        public float PersonalPhysicalPowerScaling;
        public float SpecialPower;
        public float PersonalSpecialPowerScaling;
        public float PhysicalResist;
        public float PersonalPhysicalResistScaling;
        public float SpecialResist;
        public float PersonalSpecialResistScaling;
        public float Speed;
        public float PersonalSpeedScaling;
        public float ReactSpeed;
        public float PersonalReactSpeedScaling;
    }
}
