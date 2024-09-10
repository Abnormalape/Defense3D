using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private SceneManager_ SceneManager;


        private void Awake()
        {
            Dictionary<Enum, string> aa = new();
            aa.Add(asdfasdf.d4,"asdf");
        }

        private void Start()
        {
            FindAllAppropriateControllers();
        }

        bool ss = false; //Todo: Delete
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F)) //Todo: Delete
                ss = true; //Todo: Delete

            if (ss) //Todo: Delete
            {
                targetInteractableController?.OnInteract();
                ss = false;
            }
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            this.SceneManager = OwnerManager.SceneManager_;
        }

        public void FindAllAppropriateControllers()
        {
            InteractableController[] interactables
                = FindObjectsByType<InteractableController>(FindObjectsSortMode.None);

            foreach (InteractableController interactable in interactables)
            {
                AddGameObejctToControllerDictionary( //Todo:
                    interactable as IController, //Very Very Dangerous.
                    interactable.gameObject);

                (interactable as IController)?.ControllerInitializer(this);
            }

            Debug.Log(DictionaryOfController.Count);
        }


        public void AddToDictionary(IController controller)
        {

        }

        public void AddGameObejctToControllerDictionary(IController controller, GameObject controllerGameObject)
        {
            DictionaryOfController.Add(controller, controllerGameObject);
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
            InitializeBaseData();
            FindAllInScene();
        }

        public void InitializeBaseData()
        {
        }

        public void FindAllInScene() //Todo:
        {
            InteractableController[] interactables
                = FindObjectsByType<InteractableController>(FindObjectsSortMode.None);

            //foreach (InteractableController interactable in interactables)
            //{
            //    AddGameObejctToControllerDictionary( //Todo:
            //        interactable as IController,
            //        interactable.gameObject);

            //    (interactable as IController)?.ControllerInitializer(this);
            //}

            Debug.Log(DictionaryOfController.Count);
        }

        public void SummonGameObject(GameObject prefab, Transform summonPoint)
        {
        }

        public void AddSummoned(int summoned_ID, IController summonedAttachedController)
        {
        }

        public void DestroyGameObject(GameObject prefabInstance)
        {
        }

        public void RemoveSummoned(int summoned_ID)
        {
        }

    }
    public enum asdfasdf
    {
        d1,d2,d3,d4,
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
