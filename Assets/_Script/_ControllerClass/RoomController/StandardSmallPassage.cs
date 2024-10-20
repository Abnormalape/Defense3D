using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
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


        public override void OnRoomClicked()
        {
            Debug.Log("Standard Samll Passage Clicked.");
        }

        protected override void OnAllyEnterEvent(List<IBuffHolder> enteredAlly)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnAllyExitEvent(List<IBuffHolder> exitedAlly)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnEnemyEnterEvent(List<IBuffHolder> enteredEnemy)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnEnemyExitEvent(List<IBuffHolder> exitedEnemy)
        {
            throw new System.NotImplementedException();
        }
    }
}
