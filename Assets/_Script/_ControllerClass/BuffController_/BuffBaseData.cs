using BHSSolo.DungeonDefense.ManagerClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    [System.Serializable]
    public class BuffBaseData //Data
    {
        public int BuffID;
        public string BuffName;

        public List<EffectType> EffectTypes;
        public List<int> EffectsIDs;

        public List<EffectValueTypes> EffectValueTypes;
        public List<int> EffectValueIDs;

        public TriggerTarget TriggerTarget;
        public SubTriggerTarget TriggerSubTarget;
        public TriggerAction TriggerAction;
        public SubTriggerAction SubTriggerAction;

        public EffectTarget EffectTarget;

        public DurationType DurationType;
    }

    public enum EffectType
    {
        None = 0, //Error
        BuffGiver,
        StatModifier,
        DamagerGiver,
        TargetController,
    }

    public enum EffectValueTypes
    {
        None = 0,
        Fixed,
        Variable,
    }

    public enum TriggerTarget
    {
        None = 0,
        BuffHolder,
        BuffHolderTarget,
        NearAlly,
        NearEnemy,
        AllAlly,
        AllEnemy,
        Time,

        Opponent,
        RoomAlly,
        RoomEnemy,
    }

    public enum SubTriggerTarget
    {
        None = 0,
        Buff,
        Stat,
        Passed,
    }

    public enum TriggerAction
    {
        None = 0,
        OnAttack, //공격을 시도
        OnHit, //공격이 적중
        OnAttacked, //공격을 받음
        OnDamaged, //데미지를 입음
        OnDead, //죽었을 때
        Start, //시작 할때
    }

    public enum SubTriggerAction
    {
        None = 0,
        Over, //스탯이 몇 이하일때
        Below, //체력이 몇 이상일때
        Changed, //공격력이 변경될때
        Execute, //어떤 버프가 실행될때
    }

    public enum EffectTarget
    {
        None = 0,
        BuffHolder,
        BuffHolderTarget,
        NearAlly,
        NearEnemy,
        AllAlly,
        AllEnemy,
        Opponent, //전투시, 상대방(복수 가능)
    }

    public enum DurationType
    {
        OnTime, //지속시간이 시간에 따라 줄어든다
        OnStack, //지속시간이 버프 발동할때마다 줄어든다
        Infinite, //지속시간이 줄어들지 않는다
    }
}