using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Numerics;
using BHSSolo.DungeonDefense.NPCs;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// 배틀룸의 생성자.
    /// 1. 생성시 필수적으로 배틀룸의 타입을 입력받으며, 해당 값으로 Dictionary를 순회하여 data를 찾는다.
    /// 2. 이후 찾은 data들을 저장한다.
    /// 배틀룸의 변수.
    /// 1. List<class<>>: 몬스터.
    /// 2. List<class<>>: 용사.
    /// 3. monsterCapacity: 몬스터 수용량(기본3)
    /// 4. HeroCapacity: 용사 수용량(기본3)
    /// </summary>
    class DungeonRoomBattle : DungeonRoom
    {
        public BattleRoomType BattleRoomType { get; private set; }
        private int monsterCapacity = 3;
        private List<Monster> roomMonster = new List<Monster>(3);
        private int HeroCapacity = 3;
        private List<Hero> roomHero = new List<Hero>(3);

        //DungeonRoomBattle 생성자가 실행되는 경우는, 새로운 방을 만들었을 때 뿐이다.
        public DungeonRoomBattle(BattleRoomType battleRoom, Vector3 roomBuildPosition)
        {
            InitBattleRoom(battleRoom, roomBuildPosition);
            InitRoomEffect();
        }

        public DungeonRoomBattle(BattleRoomType battleRoom, Vector3 roomBuildPosition, int level)
        {   
            roomLevel = level; //Todo: 방의 레벨에 따라 방의 효과가 상승한다.
            InitBattleRoom(battleRoom,roomBuildPosition);
            InitRoomEffect();
        }

        private void InitBattleRoom(BattleRoomType battleRoom, Vector3 roomBuildPosition)
        {
            //방의 크기와 위치.
            roomPosition = roomBuildPosition;
            roomSize = new Vector3(10, 4, 10);
            //방의 종류와 전투방 종류
            roomType = RoomType.Battle;
            BattleRoomType = battleRoom;
        }

        private void InitRoomEffect()
        {
            //전투방 종류로 룸의 데이터 찾기.
            roomData = RoomDataLoader.FindBattleRoomFromBattleRoomDictionary(BattleRoomType.ToString());

            //찾아낸 데이터로 방의 상태 추가하기.
            Dictionary<string, string> effectDictionary = roomData;
            int effectsCount = Convert.ToInt32(effectDictionary["EffectLength"]);

            for (int ix = 0; ix < effectsCount; ++ix)
            {
                roomEffectAll.Add(
                    new RoomEffect( this,
                    roomLevel,
                    Convert.ToInt32(effectDictionary[$"WhenRange{ix+1}"]),
                    Convert.ToInt32(effectDictionary[$"Range{ix + 1}"]),
                    effectDictionary[$"Effect{ix + 1}"],
                    effectDictionary[$"Target{ix + 1}"],
                    effectDictionary[$"Add{ix + 1}"],
                    effectDictionary[$"Value{ix + 1}"],
                    effectDictionary[$"When{ix + 1}"]
                    )); //Todo: RoomState를 새로 생성한다.
            }
        }
    }

    public enum BattleRoomType //Todo: CSV에서 긁어온 파일로 enum을 만들 수는 없을까?
    {
        SwordAndShield,
        Collosseum,
        Giant,
        Silence,
        EchoingScream,
    }
}