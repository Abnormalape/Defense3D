using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class AllyManager_ : MonoBehaviour, IManagerClass, IManagerFactory<AllyBaseStatus>
    {
        public GameManager_ GameManager { get; set; }
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; } = new();
        public AllyBaseStatus BaseDataDictionary { get; set; }

        private DataManager_ DataManager_;
        public static int Ally_ID { get; private set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
            DataManager_ = GameManager.DataManager_;
            OnInitializeManager_Factory();
        }


        public void OnInitializeManager_Factory()
        {
            InitializeBaseData();
            FindAllInScene();
        }

        public void InitializeBaseData()
        {
            BaseDataDictionary = this.DataManager_.AllyStatDatas;//Pull AllyStatData
        }


        /// <summary>
        /// Find ally Already in Scene.
        /// May Not Use... 
        /// </summary>
        public void FindAllInScene()
        {
            AllyController_[] allysInMap = FindObjectsByType<AllyController_>(FindObjectsSortMode.None);

            foreach (AllyController_ e in allysInMap)
            {
                ((IController)e).InitializeController(this); //Controller Initializer Should Runs all Initializer.

                e.Ally_ID = Ally_ID;
                Ally_ID++;

                AddSummoned(e.Ally_ID, e as IController);
            }

            Debug.Log($"Found, Set, Register Complete.\n{allysInMap.Length} Allys in map.");
        }

        public void SummonGameObject(int allyId, Transform summonPoint)
        {
            string summoningRace = BaseDataDictionary[allyId].Race;
            GameObject tempPrefab = Resources.Load($"Prefabs/Ally/{summoningRace}") as GameObject;

            AllyController_ tempAllyController = Instantiate(tempPrefab, summonPoint.position, Quaternion.identity, null)
                .GetComponent<AllyController_>();

            tempAllyController.Ally_ID = Ally_ID;
            AddSummoned(tempAllyController.Ally_ID, tempAllyController as IController);
        }

        public void AddSummoned(int summoned_ID, IController summonedAttachedController)
        {
            ID_ControllerDictionary.Add(summoned_ID, summonedAttachedController);
            Ally_ID++;
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
}