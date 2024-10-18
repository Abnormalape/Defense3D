using BHSSolo.DungeonDefense.ManagerClass;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    /// <summary>
    /// Must be In Interactable Layer
    /// </summary>
    public class InteractableGameObjectSample : InteractableController, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override bool CanInteract { get; set; }
        public override float InteractRange { get; set; }
        public override InteractableManager_ InteractableManager { get; set; }


        private UIManager_ UIManager_;
        private CustomInputManager InputManager_;
        private SceneManager_ SceneManager_;


        public void InitializeController(IManagerClass owenerManager)
        {
            OwnerManager = owenerManager;
            UIManager_ = OwnerManager.GameManager.UIManager_;
            InputManager_ = OwnerManager.GameManager.CustomInputManager_;
            SceneManager_ = OwnerManager.GameManager.SceneManager_;
        }

        public override void OnInteract()
        {
            Debug.Log($"{this.gameObject.name} Runs");
            SceneManager_.OrderChangeScene("TESTROOM2");
            //UIManager_.Open(UI_enum.Sample);
            //InputManager_.ChangeController(InputState_enum.Sample2);
        }

        public override void OnInteractable()
        {
            Debug.Log($"{this.gameObject.name} is now Interactable");
        }

        public override void OnNonInteractable()
        {
            Debug.Log($"{this.gameObject.name} is now Not Interactable");
        }
    }
}
