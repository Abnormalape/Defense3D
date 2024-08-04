using BHSSolo.DungeonDefense.NPCs;
using BHSSolo.DungeonDefense.NPCs.Enemy;
using BHSSolo.DungeonDefense.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Management
{
    public class EnemyManager : SingletonMono<EnemyManager>
    {
        [SerializeField] private GameManager GameManager;

        private EnemySpawner _enemySpawner;

        private List<EnemyController> _summonedEnemy = new(100); //Summoned enemy during play phase.
        private List<EnemyController> _savedEnemy = new(20); //Extra enemy which satisfy certain conditions.

        private readonly string _enemyPrefbaPath = "";

        private void Awake()
        {
            if (GameManager == null) { GameManager = GetComponent<GameManager>(); }

            if (_enemySpawner == null) { _enemySpawner = new(this); } //Todo:
            _enemySpawner.OnEnemySpawn += AddSummonedEnemyToList;

            ModifyAllEnemyPrefabDatas(); //Todo: Same to ally, room, mawang etc...
        }

        /// <summary>
        /// When game starts, get all enemy prefabs, and set their data.
        /// Data imported from CSV.
        /// </summary>
        private void ModifyAllEnemyPrefabDatas() //Todo:
        {

        }

        private void AddSummonedEnemyToList(EnemyController summonedEnemy)
        {
            _summonedEnemy.Add(summonedEnemy);
            summonedEnemy.OnDead += RemoveSummonedEnemyFromList;
        }

        private void RemoveSummonedEnemyFromList(EnemyController deadEnemy)
        {
            _summonedEnemy?.Remove(deadEnemy);
        }

        private void AddSavedEnemyToList(EnemyController summonedEnemy) //Todo: 
        {

        }

        private void RemoveSavedEnemyFromList(EnemyController summonedEnemy) //Todo: 
        {

        }
    }
}
