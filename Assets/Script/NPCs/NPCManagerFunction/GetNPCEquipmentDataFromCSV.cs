using BHSSolo.DungeonDefense.StaticFunction;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.NPCs
{
    /// <summary>
    /// NPC의 장비에 관한 정보를 들고 있는 Class
    /// 최초 한번 불러오면 더 건드릴 일이 없다.
    /// </summary>
    class GetNPCEquipmentDataFromCSV
    {
        [SerializeField] static TextAsset NPCEquipmentDataCSV;

        public static List<Dictionary<string, string>> NPCEquipmentData { get; private set; }
         = ParseCSV.ParseCsvFile(NPCEquipmentDataCSV.text);
    }
}
