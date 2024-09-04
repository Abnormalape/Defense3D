using BHSSolo.DungeonDefense.Controller;
using System.Collections.Generic;
using UnityEngine;
using BHSSolo.DungeonDefense.State;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class PlayerManager_ : MonoBehaviour, IManagerClass
    {
        public List<IController> ListOfController { get; set; } = new();
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; } = new ();
        public GameManager_ OwnerManager { get; set; }
        public StateMachineBehaviour_ StateMachineBehaviour_ { get; private set; }

        private void Awake()
        {
            Debug.Log("Awake PlayerManager");
            StateMachineBehaviour_ = new();
        }

        private void Start()
        {
        }

        private void Update()
        {
            StateMachineBehaviour_.OnStateUpdate();
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
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
    }

    public enum PlayerState_
    {
        Player2D,
        Player3D,
    }
}