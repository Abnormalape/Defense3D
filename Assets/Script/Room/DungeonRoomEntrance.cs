using BHSSolo.DungeonDefense.NPCs;
using Unity.VisualScripting;
using UnityEngine;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    class DungeonRoomEntrance : MonoBehaviour
    {
        private DungeonRoomController _ownerRoom;
        public DungeonRoomController OwnerRoom { get => _ownerRoom; set => _ownerRoom = value; }


        private void OnTriggerEnter(Collider other) //Todo: Only For Enemy
        {
            GameObject enteredThing = other.gameObject;
            EnemyController enteredEnemy = enteredThing.GetComponent<EnemyController>();

            if (enteredThing.layer == LayerMask.NameToLayer("Enemy"))
            {
                OnEnemyEnterRoom(enteredEnemy);
            }
        }

        private void OnTriggerExit(Collider other) //Todo: Only For Enemy
        {
            GameObject exitedThing = other.gameObject;
            EnemyController exitedEnemy = exitedThing.GetComponent<EnemyController>();

            if (exitedThing.layer == LayerMask.NameToLayer("Enemy"))
            {
                OnEnemyExitRoom(exitedEnemy);
            }
        }


        public delegate void EnemyEnterRoom(EnemyController enteredEnemy);
        public delegate void EnemyExitRoom(EnemyController exitedEnemy);

        public event EnemyEnterRoom OnEnemyEnterRoom;
        public event EnemyExitRoom OnEnemyExitRoom;
    }
}
