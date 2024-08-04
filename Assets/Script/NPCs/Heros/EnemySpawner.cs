using BHSSolo.DungeonDefense.Management;
using UnityEngine;

namespace BHSSolo.DungeonDefense.NPCs.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        private EnemyManager _ownerEnemyManager;

        public EnemySpawner(EnemyManager owner)
        {
            _ownerEnemyManager = owner;
        }

        EnemyController asdf; //Todo: Delete

        /// <summary>
        /// When defense phase starts, enemy spawn starts.
        /// Enemys spawn at spawnpoints.
        /// Spawn details depend on several parameters.
        /// </summary>
        public void StartSpawnInDefensePhase() //Todo:
        {
            for (int ix = 0; ix < 100; ix++)
            {
                EnemyController fdas = Instantiate(asdf).GetComponent<EnemyController>();
                OnEnemySpawn(fdas);
            }
        }

        /// <summary>
        /// When offense phase starts, enemy spawn starts.
        /// Enemys spawn at spawnpoints.
        /// Spawn details depend on several parameters.
        /// </summary>
        public void StartSpawnInOffensePhase() //Todo:
        {

        }

        public delegate void EnemySpawn(EnemyController summonedEnemy);


        public event EnemySpawn OnEnemySpawn;
    }
}
