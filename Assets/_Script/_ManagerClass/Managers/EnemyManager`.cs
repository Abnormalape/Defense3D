using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    /// <summary>
    /// Same As Ally.
    /// </summary>
    public class EnemyManager_ : MonoBehaviour, IManagerClass, IManagerFactory<EnemyBaseStatus>
    {
        public GameManager_ OwnerManager { get; set; }
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; } = new();
        public Dictionary<Enum, EnemyBaseStatus> BaseDataDictionary { get; set; } = new();

        public static int Enemy_ID { get; private set; }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            OnInitializeManager_Factory();
        }

        public void OnInitializeManager_Factory()
        {
            InitializeBaseData();
            FindAllInScene();
        }

        public void InitializeBaseData()
        {

        }

        public void FindAllInScene()
        {
            //AllyController_[] allysInMap = FindObjectsByType<AllyController_>(FindObjectsSortMode.None);

            //foreach (AllyController_ e in allysInMap)
            //{
            //    ((IController)e).ControllerInitializer(this); //Controller Initialize
            //    e.AllyControllerInitializer(BaseDataDictionary[e.AllyEnum_]); //Ally Controller Initialize

            //    e.Ally_ID = Ally_ID;
            //    Ally_ID++;

            //    AddSummoned(e.Ally_ID, e as IController);
            //}

            //Debug.Log($"Found, Set, Registet Complete.\n{allysInMap.Length} Allys in map.");

            AddSummoned(Enemy_ID, null); //Todo:
        }

        public void SummonGameObject(GameObject prefab, Transform summonPoint)
        {

        }

        public void AddSummoned(int summoned_ID, IController summonedAttachedController)
        {
            ID_ControllerDictionary.Add(summoned_ID, summonedAttachedController);
            Enemy_ID++;
        }


        public void DestroyGameObject(GameObject prefabInstance)
        {
            Destroy(prefabInstance);
        }

        public void RemoveSummoned(int summoned_ID)
        {
            ID_ControllerDictionary.Remove(summoned_ID);
        }
    }

    public struct EnemyBaseStatus
    {
        public EnemyBaseStatus(
            string Type,
            int blood,
            int bloodScaling,
            int mental,
            int mentalScaling,
            int physical,
            int physicalScaling,
            int special,
            int specialScaling,
            int physicalPower,
            int physicalPowerScaling,
            int specialPower,
            int specialPowerScaling,
            int physicalResist,
            int physicalResistScaling,
            int specialResist,
            int specialResistScaling,
            int speed,
            int speedScaling,
            int reactSpeed,
            int reactSpeedScaling,
            int maxLevel,
            string[] traits)
        {
            this.Type = Type;
            this.Blood = blood;
            this.BloodScaling = bloodScaling;
            this.Mental = mental;
            this.MentalScaling = mentalScaling;
            this.Physical = physical;
            this.PhysicalScaling = physicalScaling;
            this.Special = special;
            this.SpecialScaling = specialScaling;
            this.PhysicalPower = physicalPower;
            this.PhysicalPowerScaling = physicalPowerScaling;
            this.SpecialPower = specialPower;
            this.SpecialPowerScaling = specialPowerScaling;
            this.PhysicalResist = physicalResist;
            this.PhysicalResistScaling = physicalResistScaling;
            this.SpecialResist = specialResist;
            this.SpecialResistScaling = specialResistScaling;
            this.Speed = speed;
            this.SpeedScaling = speedScaling;
            this.ReactSpeed = reactSpeed;
            this.ReactSpeedScaling = reactSpeedScaling;
            this.MaxLevel = maxLevel;
            this.Traits = traits;
        }

        public string Type { get; private set; }
        public int Blood { get; private set; }
        public int BloodScaling { get; private set; }
        public int Mental { get; private set; }
        public int MentalScaling { get; private set; }
        public int Physical { get; private set; }
        public int PhysicalScaling { get; private set; }
        public int Special { get; private set; }
        public int SpecialScaling { get; private set; }
        public int PhysicalPower { get; private set; }
        public int PhysicalPowerScaling { get; private set; }
        public int SpecialPower { get; private set; }
        public int SpecialPowerScaling { get; private set; }
        public int PhysicalResist { get; private set; }
        public int PhysicalResistScaling { get; private set; }
        public int SpecialResist { get; private set; }
        public int SpecialResistScaling { get; private set; }
        public int Speed { get; private set; }
        public int SpeedScaling { get; private set; }
        public int ReactSpeed { get; private set; }
        public int ReactSpeedScaling { get; private set; }
        public int MaxLevel { get; private set; }
        public string[] Traits { get; private set; }
    }
}
