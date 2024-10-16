using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SamplePassage : RoomController, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override int Room_ID { get; set; }

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            RoomControllerInitializer();
        }

        public override void OnRoomClicked()
        {
            Debug.Log("Sample Passage");
        }

        public override void RoomControllerInitializer()
        {
            base.RoomControllerInitializer();
            Debug.Log("SSamplePassage override parent class's method");
        }

        protected override void OnAllyEnterEvent(AllyController_ enteredAlly)
        {
            Debug.Log("SSample Passage Event : AllyEnter");
        }

        protected override void OnAllyExitEvent(AllyController_ exitedAlly)
        {
            Debug.Log("SSample Passage Event : AllyExit");
        }

        protected override void OnEnemyEnterEvent(EnemyController_ enteredEnemy)
        {
            Debug.Log("SSample Passage Event : EnemyEnter");
        }

        protected override void OnEnemyExitEvent(EnemyController_ exitedEnemy)
        {
            Debug.Log("SSample Passage Event : EnemyExit");
        }
    }
}