using System.Collections;
using UnityEngine;

namespace BHSSolo.DungeonDefense.InteractableObject
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInteractableObject
    {
        bool canInteract { get; set; }

        public void OnInteract();

        public void ActivateInteraction();

        public void DeactivateInteraction();

        public IEnumerator WaitForInteract();
    }
}
