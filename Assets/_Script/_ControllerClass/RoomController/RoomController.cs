using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class RoomController : MonoBehaviour, IBuffHolder, IController
    {
        protected List<RoomInstallationController_> RoomInstallations { get; set; } = new();
        public List<NPCController_> EnteredAllys { get; protected set; } = new();
        public List<NPCController_> EnteredEnemies { get; protected set; } = new();

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

        public virtual void OnAllyEnterEvent(List<NPCController_> enteredAlly)
        {
            AddAlly(enteredAlly);
        }
        public virtual void OnAllyExitEvent(List<NPCController_> exitedAlly)
        {
            RemoveAlly(exitedAlly);
        }
        public virtual void OnEnemyEnterEvent(List<NPCController_> enteredEnemy)
        {
            AddEnemy(enteredEnemy);
        }
        public virtual void OnEnemyExitEvent(List<NPCController_> exitedEnemy)
        {
            RemoveEnemy(exitedEnemy);
        }

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            Collider tempCollider = other;

            if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ALLY_LAYER))
            {
                AllyController_ tempAlly = tempCollider.GetComponent<AllyController_>();
                List<NPCController_> tempAllyList = new() { tempAlly };
                OnAllyEnter?.Invoke(tempAllyList);
            }
            else if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
            {
                EnemyController_ tempEnemy = tempCollider.GetComponent<EnemyController_>();
                List<NPCController_> tempBuffHolder = new() { tempEnemy };
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
                List<NPCController_> tempBuffHolder = new() { tempAlly };
                OnAllyExit?.Invoke(tempBuffHolder);
            }
            else if (tempCollider.gameObject.layer == LayerMask.NameToLayer(ENEMY_LAYER))
            {
                EnemyController_ tempEnemy = tempCollider.GetComponent<EnemyController_>();
                List<NPCController_> tempBuffHolder = new() { tempEnemy };
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

        public delegate void RoomEvent(List<NPCController_> Npcs);
        public event RoomEvent OnEnemyEnter;
        public event RoomEvent OnEnemyExit;
        public event RoomEvent OnAllyEnter;
        public event RoomEvent OnAllyExit;
        public event RoomEvent OnEnemyDead;
        public event RoomEvent OnAllyDead;


        public void AddAlly(List<NPCController_> enteredAllys)
        {
            EnteredAllys.AddRange(enteredAllys);
            OnAddAllyList(enteredAllys);
        }
        public void RemoveAlly(List<NPCController_> exitedAllys)
        {
            foreach (var e in exitedAllys)
            {
                EnteredAllys.Remove(e);
            }
            OnRemoveAllyList(exitedAllys);
        }
        public void AddEnemy(List<NPCController_> enteredEnemies)
        {
            EnteredEnemies.AddRange(enteredEnemies);
            OnAddEnemyList(enteredEnemies);
        }
        public void RemoveEnemy(List<NPCController_> exitedEnemies)
        {
            foreach (var e in exitedEnemies)
            {
                EnteredEnemies.Remove(e);
            }
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
