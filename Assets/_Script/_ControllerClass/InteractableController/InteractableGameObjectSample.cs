using BHSSolo.DungeonDefense.ManagerClass;
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

        public void ControllerInitializer(IManagerClass owenerManager)
        {
            
        }

        public override void OnInteract()
        {
            Debug.Log($"{this.gameObject.name} Runs");
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
