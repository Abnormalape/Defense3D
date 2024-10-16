using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SampleRoom_Core : RoomController, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override int Room_ID { get; set; }

        [SerializeField] private Collider ClickableFloor;


        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;

#if UNITY_EDITOR
            if (ClickableFloor == null)
                Debug.LogAssertion("No Floor.");
# endif
        }

        public override void RoomControllerInitializer()
        {
            base.RoomControllerInitializer();
            Debug.Log("CoreRoom override parent class's method");
        }

        protected override void OnAllyEnterEvent(AllyController_ enteredAlly)
        {
            Debug.Log("Core_Sample_Room : Ally_Enter");
        }

        protected override void OnAllyExitEvent(AllyController_ exitedAlly)
        {
            Debug.Log("Core_Sample_Room : Ally_Exit");
        }

        protected override void OnEnemyEnterEvent(EnemyController_ enteredEnemy)
        {
            Debug.Log("Core_Sample_Room : Enemy_Enter");
        }

        protected override void OnEnemyExitEvent(EnemyController_ exitedEnemy)
        {
            Debug.Log("Core_Sample_Room : Enemy_Exit");
        }

        public override void OnRoomClicked()
        {
            Debug.Log("Sample CoreRoom");
        }
    }
}
