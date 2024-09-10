using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class AllyManager_ : MonoBehaviour, IManagerClass, IManagerFactory<AllyBaseStatus>
    {
        public GameManager_ OwnerManager { get; set; }
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; }
        public Dictionary<Enum, AllyBaseStatus> BaseDataDictionary { get; set; }

        private DataManager_ DataManager_;

        public static int Ally_ID { get; private set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            DataManager_ = OwnerManager.DataManager_;
            ID_ControllerDictionary = new();
            BaseDataDictionary = new();
            OnInitializeManager_Factory();
        }


        public void OnInitializeManager_Factory()
        {
            InitializeBaseData();
            FindAllInScene();
        }

        public void InitializeBaseData()
        {
            Array array = Enum.GetValues(typeof(AllyType));
            Dictionary<string, AllyType> tempNameTypePair = new();

            foreach (AllyType value in array)
            {
                tempNameTypePair.Add(value.ToString(), value);
            }

            foreach (var e in DataManager_.AllyBaseData)
            {
                Debug.Log("Setting...");

                string[] traits = e.Value["Traits"].Split('/');

                BaseDataDictionary.Add(
                    tempNameTypePair[e.Key],
                    new AllyBaseStatus(
                        e.Key,
                        Convert.ToInt32(e.Value["Blood"]),
                        Convert.ToInt32(e.Value["BloodScaling"]),
                        Convert.ToInt32(e.Value["Mental"]),
                        Convert.ToInt32(e.Value["MentalScaling"]),
                        Convert.ToInt32(e.Value["Physical"]),
                        Convert.ToInt32(e.Value["PhysicalScaling"]),
                        Convert.ToInt32(e.Value["Special"]),
                        Convert.ToInt32(e.Value["SpecialScaling"]),
                        Convert.ToInt32(e.Value["PhysicalPower"]),
                        Convert.ToInt32(e.Value["PhysicalPowerScaling"]),
                        Convert.ToInt32(e.Value["SpecialPower"]),
                        Convert.ToInt32(e.Value["SpecialPowerScaling"]),
                        Convert.ToInt32(e.Value["PhysicalResist"]),
                        Convert.ToInt32(e.Value["PhysicalResistScaling"]),
                        Convert.ToInt32(e.Value["SpecialResist"]),
                        Convert.ToInt32(e.Value["SpecialResistScaling"]),
                        Convert.ToInt32(e.Value["Speed"]),
                        Convert.ToInt32(e.Value["SpeedScaling"]),
                        Convert.ToInt32(e.Value["ReactSpeed"]),
                        Convert.ToInt32(e.Value["ReactSpeedScaling"]),
                        Convert.ToInt32(e.Value["MaxLevel"]),
                        traits));
            }
        }

        public void FindAllInScene()
        {
            //Todo:
            AllyController_[] allysInMap = FindObjectsOfType<AllyController_>();

            foreach (AllyController_ e in allysInMap)
            {
                ((IController)e).ControllerInitializer(this); //Controller Initialize
                e.AllyControllerInitializer(BaseDataDictionary[e.AllyEnum_]); //Ally Controller Initialize

                e.Ally_ID = Ally_ID;
                Ally_ID++;

                AddSummoned(e.Ally_ID, e as IController);
            }

            Debug.Log($"Found, Set, Registet Complete.\n{allysInMap.Length} Allys in map.");
        }

        public void SummonGameObject(GameObject prefab, Transform summonPoint)
        {
            //Todo:
            AllyController_ tempAllyController = Instantiate(prefab, summonPoint.position, Quaternion.identity, null)
                .GetComponent<AllyController_>();

            tempAllyController.Ally_ID = Ally_ID;
            Ally_ID++;
            AddSummoned(tempAllyController.Ally_ID, tempAllyController as IController);
        }

        public void AddSummoned(int summoned_ID, IController summonedAttachedController)
        {
            //Todo:
            ID_ControllerDictionary.Add(summoned_ID, summonedAttachedController);
        }

        public void DestroyGameObject(GameObject prefabInstance)
        {
            //Todo:
            Destroy(prefabInstance);
        }

        public void RemoveSummoned(int summoned_ID)
        {
            //Todo:
            ID_ControllerDictionary.Remove(summoned_ID);
        }
    }

    public struct AllyBaseStatus
    {
        public AllyBaseStatus(
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