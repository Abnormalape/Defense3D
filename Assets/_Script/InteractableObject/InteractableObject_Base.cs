using System.Collections;
using UnityEngine;

namespace BHSSolo.DungeonDefense.InteractableObject
{
    public class InteractableObject_Base : MonoBehaviour, IInteractableObject
    {
        public bool canInteract { get; set; } = true;


        public void ActivateInteraction()
        {
            if (!canInteract)
                return;

            Debug.Log($"Now {transform.root.gameObject.name} can interact");
            StartCoroutine(WaitForInteract());
        }

        public void DeactivateInteraction()
        {
            StopCoroutine(WaitForInteract());
        }

        public IEnumerator WaitForInteract()
        {
            Debug.Log("Wait For Key [F]");
            yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.F));
            OnInteract();
        }

        public virtual void OnInteract()
        {
            Debug.Log("Interaction Start");
        }
    }
}
