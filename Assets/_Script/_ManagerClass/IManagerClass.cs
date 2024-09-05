using BHSSolo.DungeonDefense.Controller;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public interface IManagerClass
    {
        public List<IController> ListOfController { get; set; }

        public Dictionary<IController, GameObject> DictionaryOfController { get; set; }

        public GameManager_ OwnerManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_);

        public void FindAllAppropriateControllers();

        public void AddToList(IController controller);

        public void RemoveFronList(IController controller);

        public void AddToDictionary(IController controller);

        public void AddGameObejctToControllerDictionary(IController controller, GameObject controllerGameObject);

        public void RemoveFromDictionary(IController controller);

        public void EventLoudSpeaker();
    }
}
