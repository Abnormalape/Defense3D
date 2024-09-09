using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DataManager_ : MonoBehaviour, IManagerClass
    {
        public List<IController> ListOfController { get; set; }
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; }
        public Dictionary<Enum, IController> DictionaryEnumController { get; set; }
        public GameManager_ OwnerManager { get; set; }


        private readonly string dataPath = "GameData";
        private Dictionary<string, Dictionary<string, object>> GameDatas;


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            LoadGameData();
        }

        /// <summary>
        /// Use certain path to load all game data.
        /// </summary>
        private void LoadGameData()
        {
            TextAsset[] tempGameDatas = Resources.LoadAll<TextAsset>(dataPath);

            foreach (TextAsset tempGameData in tempGameDatas)
            {

                Debug.Log(tempGameData.name);
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

        public void FindAllAppropriateControllers()
        {
            
        }

        //=========================
        private readonly string ALLYDATAPATH = "GameData/AllyData";
        private TextAsset allyBaseTextAsset;
        private Dictionary<string, Dictionary<string, string>> allyBaseData;
        public Dictionary<string, Dictionary<string, string>> AllyBaseData { get => allyBaseData; }


        public void InitializeGameData()
        {
            LoadAllTextAsset();
            SetBaseData();
        }

        private void LoadAllTextAsset()
        {
            allyBaseTextAsset = Resources.Load(ALLYDATAPATH) as TextAsset;
        }

        private void SetBaseData()
        {
            ParseTextAsset(allyBaseTextAsset.text, out allyBaseData);
        }

        private void ParseTextAsset(string inputText, out Dictionary<string, Dictionary<string, string>> doubleDictionary)
        {
            string[] rows = inputText.Replace("\r", "").Trim('\n').Split('\n');

            string[] keys = rows[0].Split(',');

            Dictionary<string, Dictionary<string, string>> tempDoubleDictionary = new();

            if (rows.Length == 0)
            {
                doubleDictionary = tempDoubleDictionary;
                return;
            }

            for (int ix = 1; ix < rows.Length; ix++)
            {
                Dictionary<string, string> tempSingleDictionary = new();

                string[] values = rows[ix].Split(',');

                string doubleDictionarykey = values[0];


                for (int iy = 1; iy < keys.Length; iy++)
                {
                    if (!string.IsNullOrEmpty(values[iy]))
                        tempSingleDictionary.Add(keys[iy], values[iy]);
                    else
                        tempDoubleDictionary.Add(keys[iy], null);
                }

                tempDoubleDictionary.Add(doubleDictionarykey, tempSingleDictionary);
            }

            doubleDictionary = tempDoubleDictionary;
        }
    }
}
