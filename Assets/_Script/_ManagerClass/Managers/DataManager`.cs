using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DataManager_ : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }

        public PlayerData PlayerData { get; private set; } //Data can only be changed in this class.
        public Dictionary<int, RoomBuildData> ID_RoomBuildData { get; private set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            InitializeGameData();
        }

        #region SetGameData
        private const string USER_SAVE_DATA_PATH = "UserSaveData/UserSaveData"; //Json
        private TextAsset userSaveData { get { return Resources.Load(USER_SAVE_DATA_PATH) as TextAsset; } }

        private const string ROOM_BUILD_DATA_PATH = "GameData/RoomBuildData"; //Json
        private TextAsset roomBuildData { get { return Resources.Load(ROOM_BUILD_DATA_PATH) as TextAsset; } }


        private const string ALLY_DATA_PATH = "GameData/AllyData"; //Csv
        private const string ENEMY_DATA_PATH = "GameData/EnemyData"; //NONE
        private const string DEFAULT_MAP_DATA_PATH = "GameData/MapData/DefaultDungeonMap"; //Csv


        public AllyBaseStats AllyStatDatas { get; private set; } //Todo:
        public EnemyBaseStats EnemyStatDatas { get; private set; } //Todo:
        private void SetNpcStatData(TextAsset allyStat, TextAsset enemyStat) //Todo:
        {
            string tempAllyStat = allyStat.text;
            string tempEnemyStat = enemyStat.text;

            AllyStatDatas = JsonUtility.FromJson<AllyBaseStats>(tempAllyStat);
            EnemyStatDatas = JsonUtility.FromJson<EnemyBaseStats>(tempEnemyStat);
        }


        public TextAsset allyBaseTextAsset { get; private set; }
        public TextAsset enemyBaseTextAsset { get; private set; }
        public TextAsset defaultMapTextAsset { get; private set; }
        //private Dictionary<string, Dictionary<string, string>> allyBaseData; //Todo: Remove
        //private Dictionary<string, Dictionary<string, string>> enemyBaseData; //Todo: Remove
        private Dictionary<string, Dictionary<string, string>> defaultMapData;
        //public Dictionary<string, Dictionary<string, string>> AllyBaseData { get => allyBaseData; }
        //public Dictionary<string, Dictionary<string, string>> EnemyBaseData { get => enemyBaseData; }
        public Dictionary<string, Dictionary<string, string>> DefaultMapData { get => defaultMapData; }


        public void InitializeGameData()
        {
            InitializeClassWithJson();
            LoadAllTextAsset();
            SetBaseData();
        }

        private void InitializeClassWithJson()
        {
            InitializePlayerSaveData();
            InitializeRoomBuildData();
        }

        private void InitializePlayerSaveData()
        {
            string tempSaveDataText = userSaveData.text;
            this.PlayerData = JsonUtility.FromJson<PlayerData>(tempSaveDataText);
        }

        private void InitializeRoomBuildData()
        {
            string tempRoomBuildDataText = roomBuildData.text;
            RoomBuildDataWrapper wrapper
                = JsonUtility.FromJson<RoomBuildDataWrapper>(tempRoomBuildDataText);
            ID_RoomBuildData = new(wrapper.RoomBuildData.Count);
            foreach (var item in wrapper.RoomBuildData)
            {
                ID_RoomBuildData.Add(item.RoomID, item);
            }
        }

        private void LoadAllTextAsset()
        {
            allyBaseTextAsset = Resources.Load(ALLY_DATA_PATH) as TextAsset;
            //enemyBaseTextAsset = Resources.Load(ENEMY_DATA_PATH) as TextAsset;
            defaultMapTextAsset = Resources.Load(DEFAULT_MAP_DATA_PATH) as TextAsset;
        }

        private void SetBaseData()
        {
            ParseCsvToDictionaryWithHeader(allyBaseTextAsset.text, out allyBaseData);
            //ParseTextAsset(enemyBaseTextAsset.text, out enemyBaseData);
            //ParseCsvToDictionaryWithHeader(defaultMapTextAsset.text, out defaultMapData); //Map Data don't need parse with header.
        }

        #endregion
        private void ParseCsvToDictionaryWithHeader(string inputText, out Dictionary<string, Dictionary<string, string>> doubleDictionary)
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

        private void SetJsonDataToClass()
        {

        }
    }
}
