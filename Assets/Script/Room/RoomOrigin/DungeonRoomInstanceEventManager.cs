using BHSSolo.DungeonDefense.NPCs;
using UnityEngine;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// This class Manages Single Dungeon Room's Event.
    /// Event Occurs In Unity.
    /// This Subscribes Monster in Room, Heros Entering Room(Hero should Enter room to locate at room)
    /// and Room's Floor.
    /// </summary>
    class DungeonRoomInstanceEventManager
    {
        //객체가 들어오고 나갈때 이벤트 실행.
        public delegate void HeroEnter(NPC nPC);
        public event HeroEnter OnHeroEnter;

        public delegate void HeroExit(NPC nPC);
        public event HeroEnter OnHeroExit;

        public delegate void HeroDead(NPC nPC); //Hero.NPCEventHandler.OnNPCDead += HeroDead;
        public event HeroDead OnHeroDead;

        public delegate void MonsterDead(NPC nPC); //Monster.NPCEventHandler.OnNPCDead += MonsterDead;
        public event MonsterDead OnMonsterDead;


    }
    public enum DungeonRoomInstanceEvent
    {
        None,
        MonsterSet,
        MonsterDead,
        MonsterAttack,
        MonsterHit,
        HeroEnter,
        HeroDead,
        HeroExit,
        HeroAttack,
        HeroHit,
    }
}
