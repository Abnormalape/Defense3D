using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;


namespace BHSSolo.DungeonDefense.Data
{
    public static class GameData
    {
        //Todo: Find CSV File By Code. but now use serializefield
        [SerializeField] static TextAsset buffDataCsv;
        static public Dictionary<string, Dictionary<string, string>> BuffData = InitDictionary(buffDataCsv.text);

        [SerializeField] static TextAsset NPCTraitDataCsv;
        static public Dictionary<string, Dictionary<string, string>> NPCTraitData = InitDictionary(NPCTraitDataCsv.text);

        [SerializeField] static TextAsset NPCHeroDataCsv;
        static public Dictionary<string, Dictionary<string, string>> NPCHeroData = InitDictionary(NPCHeroDataCsv.text);

        [SerializeField] static TextAsset NPCMonsterDataCsv;
        static public Dictionary<string, Dictionary<string, string>> NPCMonsterData = InitDictionary(NPCMonsterDataCsv.text);

        [SerializeField] static TextAsset DungeonRoomEffectDataCsv;
        static public Dictionary<string, Dictionary<string, string>> DungeonRoomEffectData = InitDictionary(DungeonRoomEffectDataCsv.text);

        [SerializeField] static TextAsset DungeonRoomDataCsv;
        static public Dictionary<string, Dictionary<string, string>> DungeonRoomData = InitDictionary(DungeonRoomDataCsv.text);

        [SerializeField] static TextAsset EquipmentDataCsv;
        static public Dictionary<string, Dictionary<string, string>> EquipmentData = InitDictionary(EquipmentDataCsv.text);

        [SerializeField] static TextAsset PrologueDialoguesCSV; //Todo:
        static public List<Dictionary<string, string>> PrologueDialogueData = InitDialogueDictionary(EquipmentDataCsv.text);



        /// <summary>
        /// This method should be used when CSVfile's first key equals "NAME".
        /// </summary>
        /// <param name="CSVFile"></param>
        /// <returns></returns>
        private static Dictionary<string, Dictionary<string, string>> InitDictionary
            (string CSVFile)
        {
            Dictionary<string, Dictionary<string, string>> outData = new Dictionary<string, Dictionary<string, string>>();

            // 줄 단위로 분리.
            string[] lines = CSVFile.Split('\n');

            // 첫 열을 키로 사용.
            if (lines.Length <= 1) { return outData; } // 데이터가 없을 경우 빈 리스트를 반환.

            // 첫 열을 쉼표로 나누어 키로 저장.
            string[] keys = lines[0].Split(',');

            // 각 줄마다 데이터를 쪼개고 저장.
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue; // 빈 줄은 건너뜁니다.

                // 쉼표로 필드를 구분. 큰따옴표 안의 쉼표는 무시.
                string[] value = Regex.Split(lines[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                Dictionary<string, string> entry = new Dictionary<string, string>();
                string firstKeyValue = "";

                for (int j = 0; j < keys.Length; j++)
                {
                    // 각 값의 큰따옴표를 제거하고, 공백을 제거.
                    if (j == value.Length)
                    {
                        break;
                    }
                    if (j == 0) { firstKeyValue = value[j]; }
                    entry[keys[j].Trim()] = value[j].Trim('"').Trim();
                }

                outData.Add(firstKeyValue, entry);
            }
            return outData;
        }

        private static List<Dictionary<string, string>> InitDialogueDictionary(string CSVFile)
        {
            List<Dictionary<string, string>> outData = new List<Dictionary<string, string>>();

            // 줄 단위로 분리.
            string[] lines = CSVFile.Split('\n');

            // 첫 열을 키로 사용.
            if (lines.Length <= 1) { return outData; } // 데이터가 없을 경우 빈 리스트를 반환.

            // 첫 열을 쉼표로 나누어 키로 저장.
            string[] keys = lines[0].Split(',');

            // 각 줄마다 데이터를 쪼개고 저장.
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue; // 빈 줄은 건너뜁니다.

                // 쉼표로 필드를 구분. 큰따옴표 안의 쉼표는 무시.
                string[] value = Regex.Split(lines[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

                Dictionary<string, string> entry = new Dictionary<string, string>();
                

                for (int j = 0; j < keys.Length; j++)
                {
                    // 각 값의 큰따옴표를 제거하고, 공백을 제거.
                    if (j == value.Length)
                    {
                        break;
                    }
                    entry[keys[j].Trim()] = value[j].Trim('"').Trim();
                }
                outData.Add(entry);
            }
            return outData;
        }
    }
}
