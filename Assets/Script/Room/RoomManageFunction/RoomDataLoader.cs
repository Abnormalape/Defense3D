using System.Collections.Generic;
using UnityEditor.AssetImporters;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// DungeonRoomBattle이 자신이 가진 BattleRoomType을 통하여 정보를 불러오는 기능을 수행.
    /// </summary>
    static class RoomDataLoader
    {
        public static Dictionary<string,string> FindBattleRoomFromBattleRoomDictionary
            (string battleRoomType) //Todo: 받을 매개변수의 값을 무엇으로 할까? 1.String 2.Enum
        {
            Dictionary<string, string> DictionaryOut = new Dictionary<string,string>();
            List<Dictionary<string, string>> tempBattleRoomDictionary 
                = GetRoomDataFromCSV.dungeonRoomBattleData;

            for (int ix = 0; ix < tempBattleRoomDictionary.Count; ix++)
            {
                if (tempBattleRoomDictionary[ix]["BattleRoomType"].Equals(battleRoomType))
                { DictionaryOut = tempBattleRoomDictionary[ix]; break; }
            }

            return DictionaryOut;
        }

        //Todo: battleRoom뿐이 아니라 다른 Room들의 데이터도 찾을 수 있어야 한다.
    }
}
