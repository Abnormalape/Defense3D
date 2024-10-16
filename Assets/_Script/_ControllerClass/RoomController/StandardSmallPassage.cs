using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class StandardSmallPassage : RoomController, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override int Room_ID { get; set; }

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            RoomControllerInitializer();
        }

        public override void RoomControllerInitializer()
        {
            base.RoomControllerInitializer();

            //Debug.Log("Passage Made");
        }

        protected override void OnAllyEnterEvent(AllyController_ enteredAlly)
        {
        }

        protected override void OnAllyExitEvent(AllyController_ exitedAlly)
        {

        }

        protected override void OnEnemyEnterEvent(EnemyController_ enteredEnemy)
        {
            Debug.Log("Enemy Enter Standard Small Passage");
        }

        protected override void OnEnemyExitEvent(EnemyController_ exitedEnemy)
        {
            Debug.Log("Enemy Exit Standard Small Passage");
        }

        public override void OnRoomClicked()
        {
            Debug.Log("Standard Samll Passage Clicked.");
        }
    }
}
