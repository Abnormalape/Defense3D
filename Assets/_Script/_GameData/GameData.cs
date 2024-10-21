using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Data
{
    public static class GameData
    {
        private static TextAsset allyBaseTextAsset;
        private static Dictionary<string, Dictionary<string, string>> allyBaseData;
        public static Dictionary<string, Dictionary<string, string>> AllyBaseData { get => allyBaseData; }


        public static void InitializeGameData() //Todo: Remove
        {
            //LoadAllTextAsset();
            //SetBaseData();
        }

        private static void LoadAllTextAsset()
        {
            allyBaseTextAsset = Resources.Load("GameData/AllyData") as TextAsset;
        }

        private static void SetBaseData()
        {
            ParseTextAsset(allyBaseTextAsset.text, out allyBaseData);
        }

        private static void ParseTextAsset(string inputText, out Dictionary<string, Dictionary<string, string>> doubleDictionary)
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
