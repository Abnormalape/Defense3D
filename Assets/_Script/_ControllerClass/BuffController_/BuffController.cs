using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    /// <summary>
    /// Must Execute [InitializeBuff] After Instantiate.
    /// </summary>
    public abstract class BuffController : IController
    {
        /// <summary>
        /// The Inheriting Object Must Declare This.
        /// </summary>
        public abstract int BuffID { get; set; }

        public IManagerClass OwnerManager { get; set; }
        public BuffManager BuffManager { get; set; }
        public IBuffHolder BuffHolder { get; set; }
        protected BuffBaseData buffBaseData { get; set; }

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            BuffManager = OwnerManager as BuffManager;
        }

        public void InitializeBuff(BuffBaseData buffBaseData, IBuffHolder buffHolder)
        {
            Debug.Log("Setting Buff Data!!");

            BuffHolder = buffHolder;
            this.buffBaseData = buffBaseData;

            // BuffHolder가 NpcController일 경우
            if (buffHolder is NPCController_ npc)
            {
                Debug.Log("Buff Holder is NPC");
                SetNpcBuffUsingBaseData(buffBaseData, npc);
            }
            // BuffHolder가 RoomController일 경우
            else if (buffHolder is RoomController room)
            {
                Debug.Log("Buff Holder is Room");
                SetRoomBuffUsingBaseData(buffBaseData, room);
            }
        }

        public abstract void ExecuteBuff();
        public abstract void ExecuteBuff(NpcBaseStat CurrentStat);
        public abstract void ExecuteBuff(IBuffHolder trigger, IBuffHolder opponent);
        public abstract void ExecuteBuff(NPCController_ actor, NPCController_ opponent);

        public virtual void EndBuff()
        {
            BuffHolder.HoldingBuffs.Remove(BuffID);
        }

        //private Dictionary<IBuffHolder, KeyValuePair<EventInfo, Action>> TriggerTarget_Event_ActionDictionary { get; set; } = new();


        private List<NPCController_> NpcTriggerTarget { get; set; } = new();

        //이 아래는 버프의 소유자가 Npc일 경우에 유효하다.
        private void SetNpcBuffUsingBaseData(BuffBaseData buffBaseData, NPCController_ npc)
        {
            switch (buffBaseData.TriggerTarget)
            {
                case TriggerTarget.BuffHolder:
                    //In this case, Only for Npc, and Npc Must inherit BuffHolder. Casting Must Success.
                    Debug.Log("Trigger Target Is Buff Holder");
                    NpcTriggerTarget = new() { npc };
                    break;

                case TriggerTarget.Opponent:
                    NpcTriggerTarget = npc.OpponentList;
                    npc.OnAddOpponentList += SubscribeNpcTriggerTarget;
                    npc.OnRemoveOpponentList += UnsubscribeNpcTriggerTarget;
                    break;

                case TriggerTarget.RoomAlly:
                    var room = npc.CurrentRoom;
                    NpcTriggerTarget = room.EnteredAllys;
                    room.OnAddAllyList += SubscribeNpcTriggerTarget;
                    room.OnRemoveAllyList += SubscribeNpcTriggerTarget;
                    break;

                case TriggerTarget.RoomEnemy:
                    room = npc.CurrentRoom;
                    NpcTriggerTarget = room.EnteredEnemies;
                    room.OnAddEnemyList += SubscribeNpcTriggerTarget;
                    room.OnRemoveEnemyList += UnsubscribeNpcTriggerTarget;
                    break;

                case TriggerTarget.AllAlly:
                    var allyManager = npc.NpcManager_.GameManager.AllyManager_;
                    NpcTriggerTarget = allyManager.AllAlly;
                    allyManager.OnAddAllAllyList += SubscribeNpcTriggerTarget;
                    allyManager.OnRemoveAllAllyList += UnsubscribeNpcTriggerTarget;
                    break;

                case TriggerTarget.AllEnemy:
                    var enemyManager = npc.NpcManager_.GameManager.EnemyManager_;
                    NpcTriggerTarget = enemyManager.AllEnemy;
                    enemyManager.OnAddAllEnemyList += SubscribeNpcTriggerTarget;
                    enemyManager.OnRemoveAllEnemyList += UnsubscribeNpcTriggerTarget;
                    break;

                default:
                    break;
            }
            SubscribeNpcTriggerTarget(NpcTriggerTarget);
        }
        private void SetRoomBuffUsingBaseData(BuffBaseData buffBaseData, RoomController room)
        {
            switch (buffBaseData.TriggerTarget)
            {
                case TriggerTarget.RoomAlly:
                    NpcTriggerTarget = room.EnteredAllys;
                    room.OnAddAllyList += SubscribeNpcTriggerTarget;
                    room.OnRemoveAllyList += SubscribeNpcTriggerTarget;
                    break;

                case TriggerTarget.RoomEnemy:
                    NpcTriggerTarget = room.EnteredEnemies;
                    room.OnAddEnemyList += SubscribeNpcTriggerTarget;
                    room.OnRemoveEnemyList += UnsubscribeNpcTriggerTarget;
                    break;

                case TriggerTarget.AllAlly:
                    var allyManager = room.OwnerManager.GameManager.AllyManager_;
                    NpcTriggerTarget = allyManager.AllAlly;
                    allyManager.OnAddAllAllyList += SubscribeNpcTriggerTarget;
                    allyManager.OnRemoveAllAllyList += UnsubscribeNpcTriggerTarget;
                    break;

                case TriggerTarget.AllEnemy:
                    var enemyManager = room.OwnerManager.GameManager.EnemyManager_;
                    NpcTriggerTarget = enemyManager.AllEnemy;
                    enemyManager.OnAddAllEnemyList += SubscribeNpcTriggerTarget;
                    enemyManager.OnRemoveAllEnemyList += UnsubscribeNpcTriggerTarget;
                    break;

                default:
                    break;
            }
            SubscribeNpcTriggerTarget(NpcTriggerTarget);
        }
        private void SubscribeNpcTriggerTarget(List<NPCController_> triggerTarget)
        {
            var action = buffBaseData.TriggerAction;

            foreach (var e in triggerTarget)
            {
                switch (action)
                {
                    case TriggerAction.OnAttack: //관측 대상이 공격 시
                        e.OnAttack += ExecuteBuff;
                        break;
                    case TriggerAction.OnHit: //관측 대상이 공격 성공시
                        e.OnHit += ExecuteBuff;
                        break;
                    case TriggerAction.OnAttacked: //관측 대상이 공격 대상 시
                        e.OnAttacked += ExecuteBuff;
                        break;
                    case TriggerAction.OnDamaged: //관측 대상이 피격 시
                        e.OnDamaged += ExecuteBuff;
                        break;
                    case TriggerAction.OnEnterRoom: //관측 대상이 방에 들어갈 시 
                        e.OnEnterRoom += ExecuteBuff;
                        break;
                    case TriggerAction.OnExitRoom: //관측 대상이 방에서 나올 시
                        e.OnExitRoom += ExecuteBuff;
                        break;
                    case TriggerAction.OnDead: //관측 대상이 사망시
                        e.OnDead += ExecuteBuff;
                        break;
                    case TriggerAction.OnCurrentResourceStatModified:
                        (e as IStatHolder).OnCurrentResourceStatModified += ExecuteBuff;
                        break;
                    case TriggerAction.OnFinalAbilityStatModified:
                        (e as IStatHolder).OnFinalAbilityStatModified += ExecuteBuff;
                        break;
                    case TriggerAction.OnStart:
                        ExecuteBuff();
                        break;
                }
            }
        }
        private void UnsubscribeNpcTriggerTarget(List<NPCController_> triggerTarget)
        {
            var action = buffBaseData.TriggerAction;

            foreach (var e in triggerTarget)
            {
                switch (action)
                {
                    case TriggerAction.OnAttack: //관측 대상이 공격 시
                        e.OnAttack -= ExecuteBuff;
                        break;
                    case TriggerAction.OnHit: //관측 대상이 공격 성공시
                        e.OnHit -= ExecuteBuff;
                        break;
                    case TriggerAction.OnAttacked: //관측 대상이 공격 대상 시
                        e.OnAttacked -= ExecuteBuff;
                        break;
                    case TriggerAction.OnDamaged: //관측 대상이 피격 시
                        e.OnDamaged -= ExecuteBuff;
                        break;
                    case TriggerAction.OnEnterRoom: //관측 대상이 방에 들어갈 시 
                        e.OnEnterRoom -= ExecuteBuff;
                        break;
                    case TriggerAction.OnExitRoom: //관측 대상이 방에서 나올 시
                        e.OnExitRoom -= ExecuteBuff;
                        break;
                    case TriggerAction.OnDead: //관측 대상이 사망시
                        e.OnDead -= ExecuteBuff;
                        break;
                }
            }
        }

        private void SetBuffEffectTarget()
        {

        }
    }
}

//private void SubscribeToEvent(IBuffHolder target, string eventName)
//{
//    var targetType = target.GetType();
//    var eventInfo = targetType.GetEvent(eventName);

//    if (eventInfo == null)
//    {
//        Debug.LogWarning("No event Found.");
//        return;
//    }

//    Action callback = () =>
//    {
//        ExecuteBuff();
//    };

//    TriggerTarget_Event_ActionDictionary.Add(target, new(eventInfo, callback));

//    try
//    {
//        eventInfo.GetAddMethod()?.Invoke(target, new object[] { callback });
//    }
//    catch (Exception ex)
//    {
//        Debug.LogWarning($"Failed to subscribe to event '{eventName}': {ex.Message}");
//    }
//}

//private void UnSubscribeToEvent(IBuffHolder target)
//{
//    TriggerTarget_Event_ActionDictionary.TryGetValue(target, out var event_Action);

//    event_Action.Key.GetRemoveMethod()?.Invoke(target, new object[] { event_Action.Value });
//}