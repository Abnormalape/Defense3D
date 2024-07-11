using System.Collections.Generic;
using System.Numerics;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// 던전의 방들이 공통적으로 가지는 특성.
    /// roomType: 방의 타입.
    /// roomPosition: 방의 위치.
    /// roomSize: 방의 크기.
    /// roomInstallation: 방에 설치된 설치물.
    /// roomLevel: 방의 레벨.
    /// roomData: 방의 정보(방이 기본적으로 가지는 효과 목록). => 즉 방의 정보는 바로 방의 효과를 변화시킨다.
    /// roomEffect[]: 방의 효과(버프 디버프).
    /// </summary>
    class DungeonRoom
    {
        protected Vector3 roomPosition;
        protected Vector3 roomSize;
        protected RoomType roomType;
        protected List<RoomAddon> roomAddon = new List<RoomAddon>(3); // 방의 설치물을 3개를 넘지 못한다.
        protected int roomLevel = 1;
        protected Dictionary<string, string> roomData;
        protected List<RoomEffect> roomEffectAll = new List<RoomEffect>(10); // 방의 효과는 10개가 넘지 않을듯 싶다.
        protected DungeonRoomInstanceEventManager dungeonRoomInstanceEventManager = new DungeonRoomInstanceEventManager();
        //Todo: 방마다 지닌것이 아닌, 모든 방의 이벤트를 총괄하는 총괄하는 총괄자를 만들자.



        protected List<RoomEffect> roomEffectHeroEnter = new List<RoomEffect>(4);
        protected List<RoomEffect> roomEffectHeroDead = new List<RoomEffect>(4);
        protected List<RoomEffect> roomEffectHeroExit = new List<RoomEffect>(4);
        protected List<RoomEffect> roomEffectMonsterPlace = new List<RoomEffect>(4);
        protected List<RoomEffect> roomEffectMonsterDead = new List<RoomEffect>(4);
        protected List<RoomEffect> roomEffectDayStart = new List<RoomEffect>(4);
        protected List<RoomEffect> roomEffectDayEnd = new List<RoomEffect>(4);


        //protected void DevideRoomEffectWithItsCondition()
        //{   //룸에 애드온이 추가되거나, 룸이 생성될때 사용하는 method.
        //    int effectCount = roomEffectAll.Count;
        //    for (int ix = 0; ix < effectCount; ix++)
        //    {
        //        switch (roomEffectAll[ix].effectWhen)
        //        {
        //            case "HeroEnter":
        //                roomEffectHeroEnter.Add(roomEffectAll[ix]);
        //                break;
        //            case "HeroDead":
        //                roomEffectHeroDead.Add(roomEffectAll[ix]);
        //                break;
        //            case "HeroExit":
        //                roomEffectHeroExit.Add(roomEffectAll[ix]);
        //                break;
        //            case "MonsterPlace":
        //                roomEffectMonsterPlace.Add(roomEffectAll[ix]);
        //                break;
        //            case "MonsterDead":
        //                roomEffectMonsterDead.Add(roomEffectAll[ix]);
        //                break;
        //            case "DayStart":
        //                roomEffectDayStart.Add(roomEffectAll[ix]);
        //                break;
        //            case "DayEnd":
        //                roomEffectDayEnd.Add(roomEffectAll[ix]);
        //                break;

        //            default:
        //                //Todo:
        //                break;
        //        }
        //    }
        //}
    }
    public enum RoomType
    {
        Battle,
        Trap,
        Function,
        Spawn,
        Reward,
    }
}