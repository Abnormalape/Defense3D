using Unity.VisualScripting;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// 모든 던전룸의 이벤트를 관리한다.
    /// </summary>
    class DungeonRoomInstanceEventManager
    {
        //객체가 들어오고 나갈때 이벤트 실행.
        public delegate void HeroEnter();
        public event HeroEnter OnHeroEnter;

        public delegate void HeroExit();
        public event HeroEnter OnHeroExit;

        public delegate void HeroDead(); //Hero.NPCEventHandler.OnNPCDead += HeroDead;
        public event HeroDead OnHeroDead;

        public delegate void MonsterDead(); //Monster.NPCEventHandler.OnNPCDead += MonsterDead;
        public event MonsterDead OnMonsterDead;

        private void HeyListen()
        {
            
        }

        //객체가 들어오고 나가는 것만 관리하면 이것을 구독한 것들이 해당 객체를 알아서 구독하건 말건 할것.
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
