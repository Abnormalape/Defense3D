using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class RoomController : MonoBehaviour
    {
        protected List<RoomInstallationController_> RoomInstallations { get; set; } = new();
        protected List<AllyController_> EnteredAllys { get; set; } = new();
        protected List<EnemyController_> EnteredEnemys { get; set; } = new();

        public abstract int Room_ID { get; set; }

        public virtual void RoomControllerInitializer()
        {
            Debug.Log("Room Initialize.");
            OnAllyEnter += OnAllyEnterEvent;
            OnAllyExit += OnAllyExitEvent;
            OnEnemyEnter += OnEnemyEnterEvent;
            OnEnemyExit += OnEnemyExitEvent;
        }

        public const string ALLY_LAYER = "Ally";
        public const string ENEMY_LAYER = "Enemy";

        public delegate void AllyEnter(AllyController_ enteredAlly);
        public delegate void AllyExit(AllyController_ exitedAlly);
        public delegate void EnemyEnter(EnemyController_ enteredEnemy);
        public delegate void EnemyExit(EnemyController_ exitedEnemy);

        public event AllyEnter OnAllyEnter;
        public event AllyExit OnAllyExit;
        public event EnemyEnter OnEnemyEnter;
        public event EnemyExit OnEnemyExit;

        protected abstract void OnAllyEnterEvent(AllyController_ enteredAlly);
        protected abstract void OnAllyExitEvent(AllyController_ exitedAlly);
        protected abstract void OnEnemyEnterEvent(EnemyController_ enteredEnemy);
        protected abstract void OnEnemyExitEvent(EnemyController_ exitedEnemy);

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            Collider tempCollider = other;

            if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ALLY_LAYER))
            {
                AllyController_ tempAlly = tempCollider.GetComponent<AllyController_>();
                OnAllyEnter?.Invoke(tempAlly);
            }
            else if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
            {
                EnemyController_ tempEnemy = tempCollider.GetComponent<EnemyController_>();
                OnEnemyEnter?.Invoke(tempEnemy);
            }
            else
            {
                Debug.Log("Incorrect Layer Found.");
                Debug.Log(LayerMask.LayerToName(tempCollider.gameObject.layer));
            }
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            Collider tempCollider = other;

            if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ALLY_LAYER))
            {
                AllyController_ tempAlly = tempCollider.GetComponent<AllyController_>();
                OnAllyExit?.Invoke(tempAlly);
            }
            else if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
            {
                EnemyController_ tempEnemy = tempCollider.GetComponent<EnemyController_>();
                OnEnemyExit?.Invoke(tempEnemy);
            }
            else
            {
                Debug.Log("Incorrect Layer Found.");
                Debug.Log(LayerMask.LayerToName(tempCollider.gameObject.layer));
            }
        }

        protected virtual void ChangeRoomShape()
        {

        }

        public abstract void OnRoomClicked();
    }
}
