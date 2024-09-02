using BHSSolo.DungeonDefense.InteractableObject;
using BHSSolo.DungeonDefense.Management;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Player
{
    public class PlayerController : MonoBehaviour
    {
        Vector3 playerPosition { get => transform.position; set => transform.position = value; }
        GameManager gameManager { get; set; }


        private void Awake()
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }

        private void Start()
        {
            StartCoroutine(SearchInteractableObject());
        }

        private IEnumerator SearchInteractableObject()
        {
            InteractableObject_Base interactableObject = null;

            while (true)
            {
                InteractableObject_Base tempInteractable;

                Debug.Log("Finding Interactable Object");

                Collider[] colliders = Physics.OverlapSphere(playerPosition, 1f);

                if (colliders.Length != 0)
                {
                    SearchInteractableObjectInArray(colliders, out tempInteractable);

                    if (interactableObject == null && tempInteractable == null)
                    { }
                    else if (interactableObject == null && tempInteractable != null)
                    {
                        interactableObject = tempInteractable;
                        interactableObject.ActivateInteraction();

                        Debug.Log(interactableObject.name); //Todo:
                    }
                    else if (interactableObject != null && tempInteractable == null)
                    {
                        interactableObject.DeactivateInteraction();
                        interactableObject = null;
                    }
                    else if (interactableObject != null && tempInteractable != null)
                    {
                        if (interactableObject == tempInteractable)
                        {
                            Debug.Log("Same Object"); //Todo:
                        }
                        else
                        {
                            interactableObject.DeactivateInteraction();
                            interactableObject = tempInteractable;
                            interactableObject.ActivateInteraction();
                            Debug.Log(interactableObject.name); //Todo:
                        }
                    }
                }
                yield return null;
            }
        }

        private void SearchInteractableObjectInArray(Collider[] foundColliders, out InteractableObject_Base objectToInteract)
        {
            List<GameObject> InteractableObjects = new(10);
            GameObject selectedInteractabelObject = null;
            float distance = 0f;

            foreach (Collider collider in foundColliders)
            {
                if (collider.gameObject.GetComponent<InteractableObject_Base>())
                    InteractableObjects.Add(collider.gameObject);
            }

            if (InteractableObjects.Count <= 0)
            {
                objectToInteract = null;
                return;
            }

            foreach (GameObject gameObject in InteractableObjects)
            {
                if (selectedInteractabelObject == null)
                {
                    selectedInteractabelObject = gameObject;
                    distance = Vector3.Distance(playerPosition, selectedInteractabelObject.transform.position);
                    continue;
                }

                float compareDistance = Vector3.Distance(playerPosition, selectedInteractabelObject.transform.position);

                if (distance >= compareDistance)
                {
                    selectedInteractabelObject = gameObject;
                }
                else
                    continue;
            }

            objectToInteract = selectedInteractabelObject.GetComponent<InteractableObject_Base>();
        }
    }
}