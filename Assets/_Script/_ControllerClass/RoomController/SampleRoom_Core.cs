using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class SampleRoom_Core : RoomController, IController
    {
        public IManagerClass OwnerManager { get; set; }

        [SerializeField] private Collider ClickableFloor;


        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;

#if UNITY_EDITOR
            if (ClickableFloor == null)
                Debug.LogAssertion("No Floor.");
# endif
        }
    }
}
