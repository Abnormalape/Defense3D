//using BHSSolo.DungeonDefense.NPCs;
//using System.Collections.Generic;
//using System.Numerics;

//namespace BHSSolo.DungeonDefense.DungeonRoom
//{
//    /// <summary>
//    /// 함정룸을 Monster를 배치할수 없다.
//    /// Hero들은 수용할 수 있다.
//    /// </summary>
//    class DungeonRoomTrap : DungeonRoom
//    {
//        public TrapRoomType TrapRoomType{ get; private set; }
//        private int HeroCapacity = 3;
//        private List<Hero> roomHero = new List<Hero>(3);

//        public DungeonRoomTrap(TrapRoomType trapRoomType, Vector3 trapRoomPosition)
//        {

//        }
//        public DungeonRoomTrap(TrapRoomType trapRoomType, Vector3 trapRoomPosition, int level)
//        {
//            roomLevel = level;
//        }

//        private void InitTrapRoom(TrapRoomType trapRoom, Vector3 roomBuildPosition)
//        {
//            //방의 크기와 위치.
//            roomPosition = roomBuildPosition;
//            roomSize = new Vector3(10, 4, 10);
//            //방의 종류와 기능방 종류
//            roomType = RoomType.Trap;
//            TrapRoomType = trapRoom;
//        }
//    }
//    public enum TrapRoomType
//    {

//    }
//}
