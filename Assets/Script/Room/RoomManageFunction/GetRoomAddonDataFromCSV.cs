using BHSSolo.DungeonDefense.StaticFunction;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// CSV파일을 분해해서 List<Dictionary<>>형태로 들고있는 static class
    /// 1. List<dictionary<>> Installation: 설치물의 정보.
    /// </summary>
    static class GetRoomAddonDataFromCSV
    {
        [SerializeField] static TextAsset AddonDataCSV;
        public static List<Dictionary<string, string>> AddonData { get; private set; }
         = ParseCSV.ParseCsvFile(AddonDataCSV.text);
    }
}
