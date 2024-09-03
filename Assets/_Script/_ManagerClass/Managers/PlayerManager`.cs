using BHSSolo.DungeonDefense.Controller;
using System.Collections.Generic;
using UnityEngine;
using BHSSolo.DungeonDefense.State;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class PlayerManager_ : MonoBehaviour, IManagerClass
    {
        private IPlayerController currentPlayerController;
        public List<IController> ListOfController { get; set; }
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; }
        public GameManager_ OwnerManager { get; set; }
        public StateMachineBehaviour_ StateMachineBehaviour_ { get; private set; }


        private void Awake()
        {
            StateMachineBehaviour_ = new();
        }

        private void Update()
        {
            
        }

        public void SetCurrentController(IPlayerController inputController)
        {
            currentPlayerController = inputController;
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
        }

        public void AddToDictionary(IController controller)
        {

        }

        public void AddToDictionary(IController controller, GameObject controllerGameObject)
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
}
