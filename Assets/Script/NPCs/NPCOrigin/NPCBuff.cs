using System.Collections.Generic;
using Unity.VisualScripting;

namespace BHSSolo.DungeonDefense.NPCs
{
    /// <summary>
    /// NPC가 가지는 버프의 생성자.
    /// </summary>
    class NPCBuff
    {
        private Dictionary<string, string> buffData; //Buff Data Dictionary.
        public BuffName BuffName { get; private set; }
        public BuffTrigger BuffTrigger { get; private set; }
        public BuffType BuffType { get; private set; }
        public object buffGiver { get; private set; } //버프제공자. Trait인가, Equipment인가, 그외의 누군가인가?.

        public NPCBuff(BuffName buffName) //버프의 이름을 받아 생성자가 작동한다. Todo: Enum과 string을 혼재중. 속히 수정 요망.
        {
            BuffName = buffName; //버프의 이름을 설정한다.

            //buffData = ;//Todo: BuffName으로 NPCBuffData에서 하나의 Dictionary를 찾는다.
        }
    }

    public enum BuffTrigger //버프가 언제 발동할까?
    {
        OnAttack, //공격시
        OnDamaged, //피격시
        OnDead, //사망시
        OnEnemyDead, //적군 사망시
        OnAlleyDead, //아군 사망시
    }

    public enum BuffName
    {
        BleedResist, //출혈저항
        BleedOffer, //출혈부여
        Bleeding, //출혈
    }

    public enum BuffType //버프의 타입.
    {
        None,
        BuffDefender, //버프를 막는 버프. (출혈방어 : 출혈버프를 방어한다.)
        BuffProvider, //버프를 주는 버프. (출혈부여 : 출혈버프를 준다.)
        StatModifier, //스탯을 조정하는 버프. ()
        DamamgeProvider, //데미지를 주는 버프. (출혈 : 몇초마다 데미지를 준다.)
    }
}