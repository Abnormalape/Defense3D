using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.NPCs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.Rendering.Universal;
using BHSSolo.DungeonDefense.Enums;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class temp_AllyController : MonoBehaviour
    {
        public abstract int AllyID { get; protected set; } //Set in Child Class
        public int AllyObjectID { get; protected set; } //identifier
        public int AllyLevel { get; protected set; }
        public int AllyMaxLevel { get; protected set; }
        public AllyStatusBlock allyCurrentStatus { get; protected set; }
        public NpcBaseStatus allyStatusBase { get; protected set; }
        public RoomController currentRoom { get; protected set; }
        public List<NPCEquipment> allyEquipments { get; protected set; }
        public List<NPCTrait> allyTraits { get; protected set; }
        public List<NPCBuff> allyBuffs { get; protected set; }
        public List<AllyStatusBlock> allyStatusModifiers { get; protected set; }
        public AllyManager_ AllyManager { get; protected set; }
        public BattleManager_ BattleManager { get; protected set; }
        public DataManager_ DataManager { get; protected set; }


        public void InitializeAllyController(AllyManager_ allyManager, int level = 1)
        {
            AllyLevel = level;
            AllyManager = allyManager;
            BattleManager = AllyManager.OwnerManager.BattleManager_;
            DataManager = AllyManager.OwnerManager.DataManager_;
            SetBaseStatus(); //DataManager.SetBaseStatus(AllyID, AllyLevel);
        }

        public void SendAttackToOpponent(Enums.NPCType opponentType, int opponentID)
        {
            BattleManager.SendAttack(opponentType, opponentID, this);
        }

        public AllyStatusBlock CalculateFinalAllyStatus()
        {
            AllyStatusBlock tempStatusBlock = new(allyCurrentStatus);
            foreach (var e in allyStatusModifiers)
            {
                tempStatusBlock += e;
            }
            return tempStatusBlock;
        }

        public void SetBaseStatus()
        {
            allyStatusBase = DataManager.NpcStatData[AllyID]; //Todo: 이제 여기를 손볼 차례
            allyCurrentStatus = SetCurrentStatus(allyStatusBase, AllyLevel);
            AllyMaxLevel = allyStatusBase.MaxLevel;
        }

        public void AdjustAllyLevel(int setLevel, bool levelAdd)
        {
            if (setLevel <= 0) { Debug.Log("Can't Drop Level."); return; }

            int targetLevel;
            if (levelAdd) { targetLevel = AllyLevel + setLevel; }//LevelUp
            else { targetLevel = setLevel; }//SetLevel

            if (targetLevel > AllyMaxLevel) { targetLevel = AllyMaxLevel; }

            AllyLevel = targetLevel;
            SetCurrentStatus(allyStatusBase, AllyLevel);
        }

        public AllyStatusBlock SetCurrentStatus(NpcBaseStatus statusBase, int level)
        {
            AllyStatusBlock tempStatusBlock = new AllyStatusBlock(
                statusBase.Blood,
                statusBase.Mental,
                statusBase.Physical,
                statusBase.Special,
                statusBase.PhysicalPower,
                statusBase.SpecialPower,
                statusBase.PhysicalResist,
                statusBase.SpecialResist,
                statusBase.Speed,
                statusBase.ReactSpeed);

            AllyStatusBlock tempLevelStatusBlock = new AllyStatusBlock(
                statusBase.BloodScaling,
                statusBase.MentalScaling,
                statusBase.PhysicalScaling,
                statusBase.SpecialScaling,
                statusBase.PhysicalPowerScaling,
                statusBase.SpecialPowerScaling,
                statusBase.PhysicalResistScaling,
                statusBase.SpecialResistScaling,
                statusBase.SpeedScaling,
                statusBase.ReactSpeedScaling) * level;

            tempStatusBlock += tempLevelStatusBlock;

            return tempStatusBlock;
        }
    }

    public class AllyStatusBlock //Todo:
    {
        public AllyStatusBlock(
            float blood,
            float mental,
            float physical,
            float special,
            float physicalPower,
            float specialPower,
            float physicalResist,
            float specialResist,
            float speed,
            float reactSpeed)
        {
            Blood = blood;
            Mental = mental;
            Physical = physical;
            Special = special;
            PhysicalPower = physicalPower;
            SpecialPower = specialPower;
            PhysicalResist = physicalResist;
            SpecialResist = specialResist;
            Speed = speed;
            ReactSpeed = reactSpeed;
        }
        public AllyStatusBlock(AllyStatusBlock copy)
        {
            Blood = copy.Blood;
            Mental = copy.Mental;
            Physical = copy.Physical;
            Special = copy.Special;
            PhysicalPower = copy.PhysicalPower;
            SpecialPower = copy.SpecialPower;
            PhysicalResist = copy.PhysicalResist;
            SpecialResist = copy.SpecialResist;
            Speed = copy.Speed;
            ReactSpeed = copy.ReactSpeed;
        }

        public float Blood { get; private set; }
        public float Mental { get; private set; }
        public float Physical { get; private set; }
        public float Special { get; private set; }
        public float PhysicalPower { get; private set; }
        public float SpecialPower { get; private set; }
        public float PhysicalResist { get; private set; }
        public float SpecialResist { get; private set; }
        public float Speed { get; private set; }
        public float ReactSpeed { get; private set; }


        public static AllyStatusBlock operator +(AllyStatusBlock a, AllyStatusBlock b)
        {
            return new AllyStatusBlock(
                a.Blood + b.Blood,
                a.Mental + b.Mental,
                a.Physical + b.Physical,
                a.Special + b.Special,
                a.PhysicalPower + b.PhysicalPower,
                a.SpecialPower + b.SpecialPower,
                a.PhysicalResist + b.PhysicalResist,
                a.SpecialResist + b.SpecialResist,
                a.Speed + b.Speed,
                a.ReactSpeed + b.ReactSpeed
            );
        }

        public static AllyStatusBlock operator *(AllyStatusBlock a, float multiplier)
        {
            return new AllyStatusBlock(
                a.Blood * multiplier,
                a.Mental * multiplier,
                a.Physical * multiplier,
                a.Special * multiplier,
                a.PhysicalPower * multiplier,
                a.SpecialPower * multiplier,
                a.PhysicalResist * multiplier,
                a.SpecialResist * multiplier,
                a.Speed * multiplier,
                a.ReactSpeed * multiplier
            );
        }
    }
}
