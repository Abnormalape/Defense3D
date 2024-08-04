//using BHSSolo.DungeonDefense.NPCs;
//using System.Collections.Generic;
//using System.Numerics;

//namespace BHSSolo.DungeonDefense.DungeonRoom
//{
//    /// <summary>
//    /// 기능룸은 주변의 Room들을 강화시키거나.
//    /// 던전 전체에 이로운, 해로운, 기타 효과를 부여한다.
//    /// </summary>
//    class DungeonRoomFunction : DungeonRoom
//    {
//        public FunctionRoomType FunctionRoomType { get; private set; }
//        public DungeonRoomFunction(FunctionRoomType functionRoom, Vector3 functionRoomPosition)
//        {
//            InitFunctionRoom(functionRoom, functionRoomPosition);
//        }

//        public DungeonRoomFunction(FunctionRoomType functionRoom, Vector3 functionRoomPosition, int level)
//        {
//            roomLevel = level;
//            InitFunctionRoom(functionRoom, functionRoomPosition);
//        }

//        private void InitFunctionRoom(FunctionRoomType functionRoom, Vector3 roomBuildPosition)
//        {
//            //방의 크기와 위치.
//            roomPosition = roomBuildPosition;
//            roomSize = new Vector3(10, 4, 10);
//            //방의 종류와 기능방 종류
//            roomType = RoomType.Function;
//            FunctionRoomType = functionRoom;
//        }
//    }

//    public enum FunctionRoomType
//    {
        
//    }
//}
