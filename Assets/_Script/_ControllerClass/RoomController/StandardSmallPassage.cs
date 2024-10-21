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
    }
}
