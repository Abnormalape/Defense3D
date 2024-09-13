using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class InteractableManager_ : MonoBehaviour, IManagerClass, IManagerFactory<InteractableStatus>
    {
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; } = new();
        public GameManager_ OwnerManager { get; set; }
        public Dictionary<int, IController> ID_ControllerDictionary { get; set; }
        public Dictionary<Enum, InteractableStatus> BaseDataDictionary { get; set; }


        private InteractableController targetInteractableController = null;
        private GameStateManager_ GameStateManager_;
        private SceneManager_ SceneManager;
        private static int Interactable_ID;
        private bool isPlayerIdleGameState = false;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F) && isPlayerIdleGameState) //Todo: InputSystem
            {
                targetInteractableController?.OnInteract();
            }
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            this.SceneManager = OwnerManager.SceneManager_;
            this.GameStateManager_ = OwnerManager.GameStateManager_;
            this.GameStateManager_.OnGameStateChanged += Set_IsPlayerIdleGameState;

            OnInitializeManager_Factory();
        }

        private void Set_IsPlayerIdleGameState(GameState gameState)
        {
            if (gameState == GameState.Player_IdleState)
                isPlayerIdleGameState = true;
            else
                isPlayerIdleGameState = false;
        }

        public void SetTargetInteractableGameObject(InteractableController foundInteractable)
        {
            if (targetInteractableController == foundInteractable)
                return;

            targetInteractableController?.OnNonInteractable();
            targetInteractableController = foundInteractable;
            targetInteractableController?.OnInteractable();
        }
        //===========================================================
        public void OnInitializeManager_Factory()
        {
            ID_ControllerDictionary = new();
            InitializeBaseData();
            FindAllInScene();
        }

        public void InitializeBaseData()
        {
            //nothing?
        }

        public void FindAllInScene()
        {
            InteractableController[] interactables
                = FindObjectsByType<InteractableController>(FindObjectsSortMode.None);

            foreach (var interactable in interactables)
            {
                AddSummoned(Interactable_ID, interactable as IController);
                (interactable as IController)?.ControllerInitializer(this);
                Interactable_ID++;
            }

            Debug.Log($"Interactables found : {ID_ControllerDictionary.Count}");
        }

        public void SummonGameObject(GameObject prefab, Transform summonPoint)
        {
            InteractableController tempInteractable
                = Instantiate(prefab, summonPoint.position, Quaternion.identity, null)
                .GetComponent<InteractableController>();

            AddSummoned(Interactable_ID, tempInteractable as IController);
        }

        public void AddSummoned(int summoned_ID, IController summonedAttachedController)
        {
            ID_ControllerDictionary.Add(summoned_ID, summonedAttachedController);
        }

        public void DestroyGameObject(GameObject prefabInstance)
        {
            Destroy(prefabInstance);
        }

        public void RemoveSummoned(int summoned_ID)
        {
            ID_ControllerDictionary.Remove(summoned_ID);
        }

    }
    public enum InteractableObjectType
    {
        d1, d2, d3, d4,
    }

    public struct InteractableStatus
    {
        public InteractableStatus(bool canInteract) //Todo:
        {
            CanInteract = canInteract;
        }

        public bool CanInteract { get; private set; }
    }
}
