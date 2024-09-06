using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class InteractableManager_ : MonoBehaviour, IManagerClass
    {
        public List<IController> ListOfController { get; set; } = new();
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; } = new();
        public Dictionary<Enum, IController> DictionaryEnumController { get; set; }
        public GameManager_ OwnerManager { get; set; }


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

        public void AddToList(IController controller)
        {

        }

        public void RemoveFromDictionary(IController controller)
        {

        }

        public void RemoveFronList(IController controller)
        {

        }

        public void EventLoudSpeaker()
        {

        }

        public void SetTargetInteractableGameObject(InteractableController foundInteractable)
        {
            if (targetInteractableController == foundInteractable)
                return;

            targetInteractableController?.OnNonInteractable();
            targetInteractableController = foundInteractable;
            targetInteractableController?.OnInteractable();
        }
    }
    public enum asdfasdf
    {
        d1,d2,d3,d4,
    }
}
