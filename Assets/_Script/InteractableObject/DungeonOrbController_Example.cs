using BHSSolo.DungeonDefense.DungeonCamera;
using BHSSolo.DungeonDefense.UI;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.InteractableObject
{
    /// <summary>
    /// Example (DungeonOrb)
    /// </summary>
    public class DungeonOrbController_Example : InteractableObject_Base
    {
        private DungeonCameraController _dungeonCameraController;
        public DungeonManageUI_Example ManageUIExample { get; private set; }


        private void Awake()
        {
            _dungeonCameraController = Camera.main.GetComponent<DungeonCameraController>();
            ManageUIExample = GetComponentInChildren<DungeonManageUI_Example>(); //Todo: Call UI Manager.
        }

        private void Start()
        {
            ManageUIExample.Hide();
        }

        public override void OnInteract()
        {
            base.OnInteract();

            _dungeonCameraController.OnInteractDungeonOrb(
                transform.position, 
                ManageUIExample.Show);
        }
    }
}