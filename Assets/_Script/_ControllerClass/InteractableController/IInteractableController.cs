using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.Controller
{
    public interface IInteractableController
    {
        public bool CanInteract {  get; set; } //Some object can only interact on ceratin event.
        public float InteractRange { get; set; } //Some object's interact range is smaller than player's detect range.
        public InteractableManager_ InteractableManager { get; set; }


        public void OnInteractable(); //When Interactable.

        public void OnInteract(); //When Interact.

        public void OnNonInteractable(); //When not Intractable.
    }
}