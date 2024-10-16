using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class EnemyManager_ : MonoBehaviour, IManagerClass, IManagerFactory<NpcBaseStatus, EnemyBaseStats>
    {
        public GameManager_ OwnerManager { get; set; }
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; } = new();
        public EnemyBaseStats BaseDataDictionary { get; set; }

        private DataManager_ DataManager_;
        public static int Enemy_ID { get; private set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            DataManager_ = OwnerManager.DataManager_;
            OnInitializeManager_Factory();
        }

        public void OnInitializeManager_Factory()
        {
            InitializeBaseData();
            FindAllInScene();
        }

        public void InitializeBaseData()
        {
            BaseDataDictionary = this.DataManager_.EnemyStatDatas; //Pull EnemyStatData
        }

        /// <summary>
        /// Find enemy Already in Scene.
        /// May Not Use...
        /// </summary>
        public void FindAllInScene()
        {
            EnemyController_[] enemies = FindObjectsByType<EnemyController_>(FindObjectsSortMode.None);

            foreach (EnemyController_ e in enemies)
            {
                ((IController)e).ControllerInitializer(this); //Controller Initializer Should Runs all Initializer.

                e.Enemy_ID = Enemy_ID;
                Enemy_ID++;

                AddSummoned(e.Enemy_ID, e as IController);
            }

            Debug.Log($"Found, Set, Registet Complete.\n{enemies.Length} Enemies in map.");
        }

        public void SummonGameObject(int enemyId, Transform summonPoint)
        {
            string summoningRace = BaseDataDictionary[enemyId].Race;
            GameObject tempPrefab = Resources.Load($"Prefabs/Enemy/{summoningRace}") as GameObject;

            EnemyController_ tempEnemyController = Instantiate(tempPrefab, summonPoint.position, Quaternion.identity, null)
                .GetComponent<EnemyController_>();

            tempEnemyController.Enemy_ID = Enemy_ID;
            AddSummoned(tempEnemyController.Enemy_ID, tempEnemyController as IController);
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
}
