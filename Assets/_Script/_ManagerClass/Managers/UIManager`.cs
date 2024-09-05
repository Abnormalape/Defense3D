using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class UIManager_ : MonoBehaviour, IManagerClass
    {
        public List<IController> ListOfController { get; set; }
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; }
        public Dictionary<Enum, IController> DictionaryEnumController { get; set; } = new();
        public GameManager_ OwnerManager { get; set; }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
        }

        public void FindAllAppropriateControllers()
        {
            UIController_[] uis
                = FindObjectsByType<UIController_>(FindObjectsSortMode.None);

            foreach (UIController_ ui in uis)
            {
                AddGameObejctToControllerDictionary( //Todo:
                    ui as IController, //Very Very Dangerous.
                    ui.gameObject);

                //DictionaryEnumController.Add(ui.UIType, ui as IController); //Todo: Master Piece

                (ui as IController)?.ControllerInitializer(this);
            }
        }

        public void AddToDictionary(IController controller)
        {

        }

        public void AddGameObejctToControllerDictionary(IController controller, GameObject controllerGameObject)
        {

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

    public enum UI_enum
    {
        None = 0,
        ChatBox = 1,
        YesOrNo = 2,
    }
}
