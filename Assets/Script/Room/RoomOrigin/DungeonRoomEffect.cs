using BHSSolo.DungeonDefense.NPCs;
using BHSSolo.DungeonDefense.StaticFunction;
using JetBrains.Annotations;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// 방이 가진 상태.
    /// 방은 RoomState를 여럿 가진다.
    /// Provider: 제공자(던전룸, 애드온, 몬스터 등)
    /// ProviderLevel: 제공자 레벨
    /// type: 종류(이름)
    /// target: 대상
    /// value: 값
    /// Add: 증감
    /// When: 조건
    /// </summary>
    class DungeonRoomEffect
    {
        private object effectProvider;
        private int effectProviderLevel;
        private int effectObserveRange; // 관측 범위
        private int effectGiveRange; // 효과 부여 범위
        private string effectType;
        private string effectTarget;
        private string effectValue;
        private string effectAdd;
        
        public string effectWhen { get; private set; }

        public DungeonRoomEffect(string type)
        {
            FindSingleDictionaryFromDictionaryList.FindDictionaryByKey //생성할때 입력받은 타입으로, 단일 dictionary를 출력한다.
                (RoomDatas.dungeonRoomEffectData, "RoomEffectName", type);
            //dictionary의 값들을 순회해서 배치한다.
        }
        public DungeonRoomEffect(
            object inputeffectProvider,
            int inputeffectProviderLevel,
            int inputeffectObserveRange,
            int inputeffectGiveRange,
            string inputEffectType,
            string inputEffectTarget,
            string inputEffectValue,
            string inputEffectAdd,
            string inputEffectWhen)
        {
            effectProvider = inputeffectProvider;
            effectProviderLevel = inputeffectProviderLevel;
            effectObserveRange = inputeffectObserveRange;
            effectGiveRange = inputeffectGiveRange;
            effectType = inputEffectType;
            effectTarget = inputEffectTarget;
            effectValue = inputEffectValue;
            effectAdd = inputEffectAdd;
            effectWhen = inputEffectWhen;
        }
        
        private void InitObserveTarget() //Todo: 용사와 몬스터가 완성되면 다시 돌아오겠다 크아아아아.
        {   //관측(구독)할 타겟 설정.

            //effect를 가진 대상 확인. (room, monster, hero)

            //effect를 가진 대상을 관측 기준으로 설정. (관측 기준 : Room)
            //(thisRoom과의 거리가 ObserveRange이하인 Room들의 RoomEventManger가 관측대상.)

            //자신의 effectWhen 에따라.
            //관측된 RoomManager들의 이벤트를 구독.

            //만약 effectWhen이 HeroDead라면 RoomManager의 HeroEnter에 GudokHero를 할당. // 이러면 이후 용사가 들어올때 그 용사를 구독.
            //만약 effectWhen이 HeroEnter라면 RoomManager의 HeroEnter에 GoGoEffect를 할당. // 이러면 이후 용사가 들어올때 그놈에게 효과 부여.
        }

        private void GuDokHero(Hero hero) //Todo: 임시코드. 히어로에게 나를 구독하라 시킴.
        {
            effectgogo += hero.GudokRoomEffect;
        }
        
        private void InitEffectTarget() //Todo: 임시코드.
        {   //효과부여(나를 구독)할 타겟 설정.

            ////EffectTarget을 확인.
            //string s = effectTarget;
            //Hero h = new Heros();
            ////그들을 Addlistener를 자신의 effect를 함.
            //effectgogo += h.GudokRoomEffect;
        }

        public delegate void GoEffectGo();//Todo: 임시코드.
        public event GoEffectGo effectgogo;//Todo: 임시코드.

        private void GudokMe()//Todo: 임시코드.
        {

        }

        private void GoGoEffect()//Todo: 임시코드.
        {

        }
    }
}