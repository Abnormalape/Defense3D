using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Data;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DataManager_ : MonoBehaviour, IManagerClass
    {
        public GameManager_ GameManager { get; set; }

        public PlayerData PlayerData { get; private set; } //Data can only be changed in this class.
        public Dictionary<int, RoomBuildData> ID_RoomBuildData { get; private set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;

            InitializeGameData();
            SetBuffBaseDatas(); //Todo:
            SetNpcStatData(); //Todo:
        }

        #region SetGameData
        private const string USER_SAVE_DATA_PATH = "UserSaveData/UserSaveData"; //Json
        private TextAsset userSaveData { get { return Resources.Load(USER_SAVE_DATA_PATH) as TextAsset; } }

        private const string ROOM_BUILD_DATA_PATH = "GameData/RoomBuildData"; //Json
        private TextAsset roomBuildData { get { return Resources.Load(ROOM_BUILD_DATA_PATH) as TextAsset; } }


        private const string ALLY_DATA_PATH = "GameData/AllyData"; //Json
        private const string ENEMY_DATA_PATH = "GameData/EnemyData"; //Json
        private const string TRAIT_DATA_PATH = "GameData/TraitData"; //Json
        private const string DEFAULT_MAP_DATA_PATH = "GameData/MapData/DefaultDungeonMap"; //Csv



        public AllyBaseStatus AllyStatDatas { get; private set; }
        public List<NpcBaseStatus> AllyBaseStatus { get; private set; } = new();
        public EnemyBaseStatus EnemyStatDatas { get; private set; }
        public List<NpcBaseStatus> EnemyBaseStatus { get; private set; } = new();
        public TraitDataWrapper TraitDataWrapper { get; private set; }
        public List<TraitBaseData> TraitDatas { get; private set; } = new();
        private void SetNpcStatData()
        {
            string tempAllyStat = (Resources.Load(ALLY_DATA_PATH) as TextAsset).text;
            string tempEnemyStat = (Resources.Load(ENEMY_DATA_PATH) as TextAsset).text;
            string tempTraitData = (Resources.Load(TRAIT_DATA_PATH) as TextAsset).text;

            AllyStatDatas = JsonUtility.FromJson<AllyBaseStatus>(tempAllyStat);
            EnemyStatDatas = JsonUtility.FromJson<EnemyBaseStatus>(tempEnemyStat);
            TraitDataWrapper = JsonUtility.FromJson<TraitDataWrapper>(tempTraitData);

            foreach (var e in AllyStatDatas.AllyStatusList)
            {
                AllyBaseStatus.Add(e);
            }
            foreach (var e in EnemyStatDatas.EnemyStatusList)
            {
                EnemyBaseStatus.Add(e);
            }
            foreach (var e in TraitDataWrapper.TraitDatas)
            {
                TraitDatas.Add(e);
            }

            Debug.Log($"AllyCount: {AllyBaseStatus.Count}");
            Debug.Log($"EnemyyCount: {EnemyBaseStatus.Count}");
            Debug.Log($"TraitCount: {TraitDatas.Count}");
        }

        private const string BUFF_DATA_PATH = "GameData/BuffBaseDatas";
        public BuffBaseDataWrapper BuffBaseDataWrapper { get; private set; } = new();
        public List<BuffBaseData> BuffBaseDatas { get; private set; } = new();
        private void SetBuffBaseDatas()
        {
            TextAsset buffBaseDatas = Resources.Load(BUFF_DATA_PATH) as TextAsset;
            BuffBaseDataWrapper = JsonUtility.FromJson<BuffBaseDataWrapper>(buffBaseDatas.text);
            foreach (var data in BuffBaseDataWrapper.BuffBaseDatas)
            {
                BuffBaseDatas.Add(data);
            }
            Debug.Log($"Imported Buff Data Count: {BuffBaseDatas.Count}");
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
            //allyBaseTextAsset = Resources.Load(ALLY_DATA_PATH) as TextAsset;
            //enemyBaseTextAsset = Resources.Load(ENEMY_DATA_PATH) as TextAsset;
            defaultMapTextAsset = Resources.Load(DEFAULT_MAP_DATA_PATH) as TextAsset;
        }

        private void SetBaseData()
        {
            //ParseCsvToDictionaryWithHeader(allyBaseTextAsset.text, out allyBaseData);
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
