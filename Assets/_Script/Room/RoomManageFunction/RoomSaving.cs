using UnityEngine;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    static class RoomSaving
    {

    }

    /// <summary>
    /// 방을 저장할때 사용하는 class.
    /// roomId: 저장하는 방의 일련번호. 이것을 기준으로 데이터를 save & load.
    /// roomPosition: 방의 위치.
    /// </summary>
    public class RoomSavings : MonoBehaviour
    {
        public int roomId;
        public Vector3 roomPosition;
    }

    /// <summary>
    /// monsterID: 저장하는 몬스터의 ID
    /// </summary>
    public class RoomSavingBattle : RoomSavings
    {
        public int[] monsterID;
    }
}
