using UnityEngine;
using BHSSolo.DungeonDefense.StaticFunction;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// CSV파일을 분해해서 List<Dictionary<>>형태로 들고있는 static class
    /// 1. List<dictionary<>> dungeonRoomBattleData: 배틀룸의 정보.
    /// </summary>
    public static class GetRoomDataFromCSV
    {
        //Todo: CSV파일을 경로로 불러오기.
        [SerializeField] static TextAsset DungeonRoomBattleDataCSV;
        public static List<Dictionary<string, string>> dungeonRoomBattleData { get; private set; }
         = ParseCSV.ParseCsvFile(DungeonRoomBattleDataCSV.text);
    }
}