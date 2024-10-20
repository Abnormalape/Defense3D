using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class RoomController : MonoBehaviour, IBuffHolder, IController
    {
        protected List<RoomInstallationController_> RoomInstallations { get; set; } = new();
        public List<NPCController_> EnteredAllys { get; protected set; } = new();
        public List<NPCController_> EnteredEnemys { get; protected set; } = new();

        public abstract int Room_ID { get; set; }
        public Dictionary<int, BuffController> HoldingBuffs { get; set; } = new();

        public BuffManager BuffManager { get; set; }
        public List<NpcStatModifier> StatModifiers { get; set; } = new();
        public IManagerClass OwnerManager { get; set; }

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
        }

        public virtual void RoomControllerInitializer()
        {
            //Debug.Log("Room Initialize.");
            OnAllyEnter += OnAllyEnterEvent;
            OnAllyExit += OnAllyExitEvent;
            OnEnemyEnter += OnEnemyEnterEvent;
            OnEnemyExit += OnEnemyExitEvent;
        }

        public const string ALLY_LAYER = "Ally";
        public const string ENEMY_LAYER = "Enemy";

        protected abstract void OnAllyEnterEvent(List<IBuffHolder> enteredAlly);
        protected abstract void OnAllyExitEvent(List<IBuffHolder> exitedAlly);
        protected abstract void OnEnemyEnterEvent(List<IBuffHolder> enteredEnemy);
        protected abstract void OnEnemyExitEvent(List<IBuffHolder> exitedEnemy);

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            Collider tempCollider = other;

            if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ALLY_LAYER))
            {
                AllyController_ tempAlly = tempCollider.GetComponent<AllyController_>();
                List<IBuffHolder> tempBuffHolder = new() { (tempAlly as IBuffHolder) };
                OnAllyEnter?.Invoke(tempBuffHolder);
            }
            else if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
            {
                EnemyController_ tempEnemy = tempCollider.GetComponent<EnemyController_>();
                List<IBuffHolder> tempBuffHolder = new() { (tempEnemy as IBuffHolder) };
                OnEnemyEnter?.Invoke(tempBuffHolder);
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
                List<IBuffHolder> tempBuffHolder = new() { (tempAlly as IBuffHolder) };
                OnAllyExit?.Invoke(tempBuffHolder);
            }
            else if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
            {
                EnemyController_ tempEnemy = tempCollider.GetComponent<EnemyController_>();
                List<IBuffHolder> tempBuffHolder = new() { (tempEnemy as IBuffHolder) };
                OnEnemyExit?.Invoke(tempBuffHolder);
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

        public delegate void RoomEvent(List<IBuffHolder> Npcs);
        public event RoomEvent OnEnemyEnter;
        public event RoomEvent OnEnemyExit;
        public event RoomEvent OnAllyEnter;
        public event RoomEvent OnAllyExit;
        public event RoomEvent OnEnemyDead;
        public event RoomEvent OnAllyDead;


        public void AddAlly(List<NPCController_> enteredAllys)
        {
            OnAddAllyList(enteredAllys);
        }
        public void RemoveAlly(List<NPCController_> exitedAllys)
        {
            OnRemoveAllyList(exitedAllys);
        }
        public void AddEnemy(List<NPCController_> enteredEnemies)
        {
            OnAddEnemyList(enteredEnemies);
        }
        public void RemoveEnemy(List<NPCController_> exitedEnemies)
        {
            OnRemoveEnemyList(exitedEnemies);
        }

        public delegate void RoomMemberEvent(List<NPCController_> npcs);
        public event RoomMemberEvent OnAddAllyList;
        public event RoomMemberEvent OnRemoveAllyList;
        public event RoomMemberEvent OnAddEnemyList;
        public event RoomMemberEvent OnRemoveEnemyList;

        public void InitializeBuffHolder()
        {
        }
        public void AddBuff(int buffID)
        {
        }
        public void RemoveBuff(int buffID)
        {
        }

        
    }
}
