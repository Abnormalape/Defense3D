using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class InteractableController : MonoBehaviour
    {

        public abstract bool CanInteract { get; set; } //Some object can only interact on ceratin event.
        public abstract float InteractRange { get; set; } //Some object's interact range is smaller than player's detect range.
        public abstract InteractableManager_ InteractableManager { get; set; }


        public abstract void OnInteractable(); //When Interactable.

        public abstract void OnInteract(); //When Interact.

        public abstract void OnNonInteractable(); //When not Intractable.
    }
}
