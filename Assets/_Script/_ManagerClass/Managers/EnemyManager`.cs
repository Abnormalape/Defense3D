using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class EnemyManager_ : MonoBehaviour, IManagerClass, IManagerFactory<List<NpcBaseStatus>>
    {
        public GameManager_ GameManager { get; set; }
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; } = new();
        public List<NpcBaseStatus> BaseDataDictionary { get; set; } = new();

        private DataManager_ DataManager_;
        public static int Enemy_ID { get; private set; }

        private const string ENEMY_PREFAB_PATH = "Prefabs/Enemy";

        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
            DataManager_ = GameManager.DataManager_;
            OnInitializeManager_Factory();

            //Todo:
            string tempName = $"{ENEMY_PREFAB_PATH}/{BaseDataDictionary[1].Race}";
            GameObject tempNPC = Resources.Load(tempName) as GameObject;
            GameObject ttempNPC = Instantiate(tempNPC, new Vector3(250, 0, 0), Quaternion.identity);
            ttempNPC.GetComponent<EnemyController_>().InitializeController(this);
        }

        public void OnInitializeManager_Factory()
        {
            InitializeBaseData();
            FindAllInScene();
        }

        public void InitializeBaseData()
        {
            BaseDataDictionary = this.DataManager_.EnemyBaseStatus; //Pull EnemyStatData
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
                ((IController)e).InitializeController(this); //Controller Initializer Should Runs all Initializer.

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




        public List<NPCController_> AllEnemy = new();
        public delegate void EnemyManagerEvent(List<NPCController_> updatingEnemy);
        public event EnemyManagerEvent OnAddAllEnemyList;
        public event EnemyManagerEvent OnRemoveAllEnemyList;

        private void AddEnemyToList(List<NPCController_> addedEnemy)
        {
            AllEnemy.AddRange(addedEnemy);
            OnAddAllEnemyList(addedEnemy);
        }
        private void RemoveEnemyFromList(List<NPCController_> removedEnemy)
        {
            foreach (NPCController_ item in removedEnemy)
            {
                AllEnemy.Remove(item);
            }
            OnRemoveAllEnemyList(removedEnemy);
        }
    }
}
