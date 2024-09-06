﻿using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class AllyManager_ : MonoBehaviour, IManagerClass
    {
        public List<IController> ListOfController { get; set; }
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; }
        public Dictionary<Enum, IController> DictionaryEnumController { get; set; }
        public GameManager_ OwnerManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
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

        private Dictionary<Enum, GameObject> dictionaryEnumPrefab;
        private void SummonAlly(Ally_enum toSummon)
        {
            GameObject summoned = Instantiate(dictionaryEnumPrefab[toSummon]);
            summoned.GetComponent<AllyController_>().AllyStatus_.ModifyStatus();
            ListOfController.Add(summoned.GetComponent<AllyController_>() as IController);
        }//Todo: Remove

        public void FindAllAppropriateControllers()
        {
            //Find Ally Name and find in path use with name
            //then add dictionary name and gameobject
            Resources.Load($"Prefabs/Ally/{"aa"}");
            throw new System.NotImplementedException();
        }
    }

    public enum Ally_enum
    {
        Sample,
    }
}
