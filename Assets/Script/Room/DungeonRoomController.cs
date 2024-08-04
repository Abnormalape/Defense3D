using BHSSolo.DungeonDefense.NPCs;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    public class DungeonRoomController : MonoBehaviour
    {
        private List<DungeonRoomEntrance> _dungeonRoomEntrance = new(4);

        public List<EnemyController> EnteredEnemys { get; private set; }

        public List<AllyController> PlacedAlly { get; private set; }

        public TerrainFeatureController TerrainFeatureController { get; private set; }

        public TerrainEffectController TerrainEffectController { get; private set; }

        public TerrainAddonController TerrainAddonController { get; private set; }


        private void Awake()
        {
            TerrainFeatureController = new TerrainFeatureController();
            TerrainEffectController = new TerrainEffectController();
            TerrainAddonController = new TerrainAddonController();
        }

        private void Start()
        {
            OnEnemyEnter += AddEnteredEnemyToList;
            OnEnemyExit += RemoveExitedEnemyFromList;
        }

        private void Update()
        {

        }

        [SerializeField] GameObject entrance; //Todo: Delete
        private void MakeEntrace()
        {
            DungeonRoomEntrance tempEntrance
                = Instantiate(entrance).GetComponent<DungeonRoomEntrance>(); //Todo: Position and Rotation.

            tempEntrance.OwnerRoom = this; //Todo: hmmmmmm.
            _dungeonRoomEntrance.Add(tempEntrance);

            tempEntrance.OnEnemyEnterRoom += AddEnteredEnemyToList;
            tempEntrance.OnEnemyExitRoom += RemoveExitedEnemyFromList;
        }

        private void AddEnteredEnemyToList(EnemyController enteredEnemy)
        {
            EnteredEnemys.Add(enteredEnemy);
            OnEnteredEnemyChanged();
        }

        private void RemoveExitedEnemyFromList(EnemyController exitedEnemy)
        {
            EnteredEnemys.Remove(exitedEnemy);
            OnEnteredEnemyChanged();
        }

        public delegate void EnemyEnter(EnemyController enteredEnemy);
        public delegate void EnemyExit(EnemyController exitedEnemy);
        public delegate void EnemyDead();
        public delegate void AllyDead();
        public delegate void EnteredEnemyChanged();

        public event EnemyEnter OnEnemyEnter; //OnCollision or OnTrigger
        public event EnemyExit OnEnemyExit; //OnCollision or OnTrigger
        public event EnemyDead OnEnemyDead;
        public event AllyDead OnAllyDead;
        public event EnteredEnemyChanged OnEnteredEnemyChanged;
    }
}
