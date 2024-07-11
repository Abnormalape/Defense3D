using UnityEngine;
using BHSSolo.DungeonDefense.StaticFunction;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// CSV������ �����ؼ� List<Dictionary<>>���·� ����ִ� static class
    /// 1. List<dictionary<>> dungeonRoomBattleData: ��Ʋ���� ����.
    /// </summary>
    public static class GetRoomDataFromCSV
    {
        //Todo: CSV������ ��η� �ҷ�����.
        [SerializeField] static TextAsset DungeonRoomBattleDataCSV;
        public static List<Dictionary<string, string>> dungeonRoomBattleData { get; private set; }
         = ParseCSV.ParseCsvFile(DungeonRoomBattleDataCSV.text);
    }
}