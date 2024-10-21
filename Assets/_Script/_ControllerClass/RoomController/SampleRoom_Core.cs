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

        public override void OnRoomClicked()
        {
            Debug.Log("Room Clicked");
        }
    }
}
