using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Data;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class AllyManager_ : MonoBehaviour, IManagerClass
    {
        public List<IController> ListOfController { get; set; }
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; }
        public Dictionary<Enum, IController> DictionaryEnumController { get; set; }
        public GameManager_ OwnerManager { get; set; }


        private Dictionary<Ally_enum, AllyBaseStatus> TypeBasedStatus = new();


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            Array array = Enum.GetValues(typeof(Ally_enum));
            Dictionary<string, Ally_enum> tempNameTypePair = new();

            foreach (Ally_enum value in array)
            {
                tempNameTypePair.Add(value.ToString(),value);
            }

            foreach(var e in GameData.AllyBaseData)
            {
                Debug.Log("Setting...");

                TypeBasedStatus.Add(
                    tempNameTypePair[e.Key],
                    new AllyBaseStatus(
                        Convert.ToInt32(GameData.AllyBaseData[e.Key]["HP"]),
                        Convert.ToInt32(GameData.AllyBaseData[e.Key]["MP"])
                        ));
            }

            Debug.Log("ADF");
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

        public void FindAllAppropriateControllers()
        {

        }
    }

    public enum Ally_enum
    {
        Sample,
        Goblin,
        Oak,
        Oger,
        Dragon,
    }

    public struct AllyBaseStatus //Todo: More Fields.
    {
        public AllyBaseStatus(int hp, int mp)
        {
            HP = hp;
            MP = mp;
        }


        public int HP { get; private set; }
        public int MP { get; private set; }
    }
}
