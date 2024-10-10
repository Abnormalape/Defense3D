using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class StandardSmallRoom : RoomController, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override int Room_ID { get; set; }

        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            RoomControllerInitializer();
        }

        public override void RoomControllerInitializer()
        {
            base.RoomControllerInitializer();

            Debug.Log("Small Room Made");
        }

        protected override void OnAllyEnterEvent(AllyController_ enteredAlly)
        {
        }

        protected override void OnAllyExitEvent(AllyController_ exitedAlly)
        {
        }

        protected override void OnEnemyEnterEvent(EnemyController_ enteredEnemy)
        {
        }

        protected override void OnEnemyExitEvent(EnemyController_ exitedEnemy)
        {
        }
    }
}
