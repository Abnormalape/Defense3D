using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DataManager_ : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            InitializeGameData();
        }

        #region SetGameData
        private readonly string ALLY_DATA_PATH = "GameData/AllyData";
        private readonly string ENEMY_DATA_PATH = "GameData/EnemyData";
        private TextAsset allyBaseTextAsset;
        private TextAsset enemyBaseTextAsset;
        private Dictionary<string, Dictionary<string, string>> allyBaseData;
        private Dictionary<string, Dictionary<string, string>> enemyBaseData;
        public Dictionary<string, Dictionary<string, string>> AllyBaseData { get => allyBaseData; }
        public Dictionary<string, Dictionary<string, string>> EnemyBaseData { get => enemyBaseData; }


        public void InitializeGameData()
        {
            LoadAllTextAsset();
            SetBaseData();
        }

        private void LoadAllTextAsset()
        {
            allyBaseTextAsset = Resources.Load(ALLY_DATA_PATH) as TextAsset;
            //enemyBaseTextAsset = Resources.Load(ENEMY_DATA_PATH) as TextAsset;
        }

        private void SetBaseData()
        {
            ParseTextAsset(allyBaseTextAsset.text, out allyBaseData);
            //ParseTextAsset(enemyBaseTextAsset.text, out enemyBaseData);
        }

        #endregion
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
