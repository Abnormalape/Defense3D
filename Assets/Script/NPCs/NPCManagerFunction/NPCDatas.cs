using BHSSolo.DungeonDefense.StaticFunction;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.NPCs
{
    static class NPCDatas
    {
        //NPC Equipment Data. Todo: 빼야할 요소.
        [SerializeField] static TextAsset NPCEquipmentDataCSV;
        public static List<Dictionary<string, string>> NPCEquipmentData { get; private set; }
         = ParseCSV.ParseCsvFile(NPCEquipmentDataCSV.text);

        //NPC Hero Data.
        [SerializeField] static TextAsset NPCHeroDataCSV;
        public static List<Dictionary<string, string>> NPCHeroData { get; private set; }
         = ParseCSV.ParseCsvFile(NPCHeroDataCSV.text);

        //NPC Monster Data.
        [SerializeField] static TextAsset NPCMonsterDataCSV;
        public static List<Dictionary<string, string>> NPCMonsterData { get; private set; }
         = ParseCSV.ParseCsvFile(NPCMonsterDataCSV.text);

        //NPC Attribute Data.
        [SerializeField] static TextAsset NPCAttributeDataCSV;
        public static List<Dictionary<string, string>> NPCAttributeData { get; private set; }
        = ParseCSV.ParseCsvFile(NPCAttributeDataCSV.text);

        //NPC Buff Data.
        [SerializeField] static TextAsset NPCBuffDataCSV;
        public static List<Dictionary<string, string>> NPCBuffData { get; private set; }
        = ParseCSV.ParseCsvFile(NPCBuffDataCSV.text);
    }
}
