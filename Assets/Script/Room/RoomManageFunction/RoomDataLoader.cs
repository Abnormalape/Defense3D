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
            (List<Dictionary<string, string>> battleRoomDictionary,
             string battleRoomType) //Todo: battleRoomType을 enum으로 받을지 string으로 받을지...
        {
            Dictionary<string, string> DictionaryOut = new Dictionary<string,string>();
            List<Dictionary<string, string>> tempBattleRoomDictionary = battleRoomDictionary;

            for (int ix = 0; ix < tempBattleRoomDictionary.Count; ix++)
            {
                if (tempBattleRoomDictionary[ix]["BattleRoomType"].Equals(battleRoomType))
                { DictionaryOut = tempBattleRoomDictionary[ix]; break; }
            }

            return DictionaryOut;
        }
    }
}
