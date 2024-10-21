using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.Controller
{
    [System.Serializable]
    public class BuffBaseData //Data
    {
        public int BuffID;
        public string BuffName;
        public string BuffDescription;

        public TriggerTarget TriggerTarget;
        public TriggerAction TriggerAction;
        public EffectTarget EffectTarget;
    }

    [System.Serializable]
    public class BuffBaseDataWrapper
    {
        public List<BuffBaseData> BuffBaseDatas;
    }

    public enum TriggerTarget
    {
        None = 0, //Error
        BuffHolder,
        Opponent,
        RoomAlly,
        RoomEnemy,
        AllAlly,
        AllEnemy,
    }

    public enum TriggerAction
    {
        None = 0,
        OnAttack, //공격을 시도
        OnHit, //공격이 적중
        OnAttacked, //공격을 받음
        OnDamaged, //데미지를 입음
        OnDead, //죽었을 때
        OnEnterRoom, //방에 입장
        OnExitRoom, //방에서 퇴장
        OnCurrentResourceStatModified, //자원 스탯 (현재값/최대값)이 변화
        OnFinalAbilityStatModified, //능력 스탯이 변화
        OnStart, //즉시
    }

    public enum EffectTarget
    {
        None = 0, //Error
        BuffHolder,
        Opponent,
        RoomAlly,
        RoomEnemy,
        AllAlly,
        AllEnemy,
    }
}