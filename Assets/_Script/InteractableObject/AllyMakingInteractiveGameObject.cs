using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.InteractableObject
{
    public class AllyMakingInteractiveGameObject : InteractableController, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override bool CanInteract { get; set; }
        public override float InteractRange { get; set; }
        public override InteractableManager_ InteractableManager { get; set; }

        private AllyManager_ allyManager_ { get; set; }
        private AllyType allyType { get; set; } = AllyType.Goblin;

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            allyManager_ = OwnerManager.GameManager.AllyManager_;
        }

        public override void OnInteract()
        {
            Debug.Log("You make Ally.");
        }

        public override void OnInteractable()
        {
            Debug.Log("You can make Ally.");
        }

        public override void OnNonInteractable()
        {
            Debug.Log("You can not make Ally.");
        }
    }
}
