using BHSSolo.DungeonDefense.Player;
using BHSSolo.DungeonDefense.Singleton;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace BHSSolo.DungeonDefense.InteractableObject
{
    public class InteractableObjectManager : SingletonMono<InteractableObjectManager>
    {
        private InteractableObject_Base nowInteractable = null;
        private PlayerController playerController;
        private Dictionary<InteractableObject_Base, GameObject> interactableObjectDictionary;


        public InteractableObject_Base NowInteractable
        {
            get => nowInteractable;
            set
            {
                if (nowInteractable == null || nowInteractable != value)
                {
                    if (nowInteractable != null)
                        nowInteractable.DeactivateInteraction();

                    nowInteractable = value;
                    nowInteractable.ActivateInteraction();
                }
            }
        }


        public void InitiateInteractableObjectManager(
            PlayerController playerController)
        {
            this.playerController = playerController;
            interactableObjectDictionary = new(50);
            InitiateInteractableObjectDictionary();
        }

        public void InitiateInteractableObjectDictionary()
        {
            InteractableObject_Base[] tempInteractables = FindObjectsOfType<InteractableObject_Base>();

            foreach (InteractableObject_Base e in tempInteractables)
            {
                interactableObjectDictionary.Add(e, e.gameObject);
            }
        }

        public void SetNowInteractableTarget(InteractableObject_Base foundInteractable)
        {
            if (interactableObjectDictionary.ContainsKey(foundInteractable))
            {
                NowInteractable = foundInteractable;
            }
            else
            {
                Debug.Log("No InteractableObjects Found");
            }
        }
    }
}