
using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.NPCs;
using System;
using System.Collections.Generic;
using System.Reflection;
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
        protected RoomController ActualBuffHolder_Room { get; set; }
        protected NPCController_ ActualBuffHolder_Npc { get; set; }
        protected BuffBaseData buffBaseData { get; set; }

        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
            BuffManager = OwnerManager as BuffManager;
        }

        public void InitializeBuff(BuffBaseData buffBaseData, IBuffHolder buffHolder)
        {
            BuffHolder = buffHolder;
            this.buffBaseData = buffBaseData;

            if (buffHolder is NPCController_ npc)
            {
                SetNpcBuffUsingBaseData(buffBaseData, npc);
            }
            // BuffHolder가 RoomController일 경우
            else if (buffHolder is RoomController room)
            {
                SetRoomBuffUsingBaseData(buffBaseData, room);
            }
        }

        public abstract void ExecuteBuff(); //Todo: Buff's Action. Like Make Buff, Damage Someone etc...
        public abstract void ExecuteBuff(IBuffHolder trigger, IBuffHolder opponent);

        private Dictionary<IBuffHolder, KeyValuePair<EventInfo, Action>> TriggerTarget_Event_ActionDictionary { get; set; } = new();


        private List<NPCController_> NpcTriggerTarget { get; set; } = new();
        private void SetNpcBuffUsingBaseData(BuffBaseData buffBaseData, NPCController_ npc)
        {
            //이 아래는 버프의 소유자가 Npc일 경우에 유효하다.
            switch (buffBaseData.TriggerTarget)
            {
                case TriggerTarget.BuffHolder:
                    //In this case, Only for Npc, and Npc Must inherit BuffHolder. Casting Must Success.
                    NpcTriggerTarget = new() { npc };
                    break;

                case TriggerTarget.Opponent:
                    NpcTriggerTarget = npc.OpponentList;
                    npc.OnAddOpponentList += SubscribeTriggerTarget;
                    npc.OnRemoveOpponentList += UnsubscribeTriggerTarget;
                    break;

                case TriggerTarget.RoomAlly:
                    var room = npc.CurrentRoom;
                    NpcTriggerTarget = room.EnteredAllys;
                    room.OnAddAllyList += SubscribeTriggerTarget;
                    room.OnRemoveAllyList += SubscribeTriggerTarget;
                    break;

                case TriggerTarget.RoomEnemy:
                    room = npc.CurrentRoom;
                    NpcTriggerTarget = room.EnteredEnemys;
                    room.OnAddEnemyList += SubscribeTriggerTarget;
                    room.OnRemoveEnemyList += UnsubscribeTriggerTarget;
                    break;

                case TriggerTarget.AllAlly:
                    var allyManager = npc.NpcManager_.GameManager.AllyManager_;
                    NpcTriggerTarget = allyManager.AllAlly;
                    allyManager.OnAddAllAllyList += SubscribeTriggerTarget;
                    allyManager.OnRemoveAllAllyList += UnsubscribeTriggerTarget;
                    break;

                case TriggerTarget.AllEnemy:
                    var enemyManager = npc.NpcManager_.GameManager.EnemyManager_;
                    NpcTriggerTarget = enemyManager.AllEnemy;
                    enemyManager.OnAddAllEnemyList += SubscribeTriggerTarget;
                    enemyManager.OnRemoveAllEnemyList += UnsubscribeTriggerTarget;
                    break;

                default:
                    return;
            }

            SubscribeTriggerTarget(NpcTriggerTarget);
        }
        private void SubscribeTriggerTarget(List<NPCController_> triggerTarget)
        {
            var action = buffBaseData.TriggerAction;

            foreach (var e in triggerTarget)
            {
                switch (action)
                {
                    case TriggerAction.OnAttack:
                        e.OnAttack += ExecuteBuff;
                        break;
                    case TriggerAction.OnHit:
                        e.OnHit += ExecuteBuff;
                        break;
                    case TriggerAction.OnAttacked:
                        e.OnAttacked += ExecuteBuff;
                        break;
                    case TriggerAction.OnDamaged:
                        e.OnDamaged += ExecuteBuff;
                        break;
                }
            }
        }
        private void UnsubscribeTriggerTarget(List<NPCController_> triggerTarget)
        {
            var action = buffBaseData.TriggerAction;

            foreach (var e in triggerTarget)
            {
                switch (action)
                {
                    case (TriggerAction.OnAttack):
                        e.OnAttack -= ExecuteBuff;
                        break;
                    case (TriggerAction.OnHit):
                        e.OnHit -= ExecuteBuff;
                        break;
                    case (TriggerAction.OnAttacked):
                        e.OnAttacked -= ExecuteBuff;
                        break;
                    case (TriggerAction.OnDamaged):
                        e.OnDamaged -= ExecuteBuff;
                        break;
                }
            }
        }


        private List<RoomController> RoomTriggerTarget { get; set; } = new();
        private void SetRoomBuffUsingBaseData(BuffBaseData buffBaseData, RoomController room)
        {
            switch (buffBaseData.TriggerTarget)
            {
                case TriggerTarget.RoomAlly:
                    NpcTriggerTarget = room.EnteredAllys;
                    room.OnAddAllyList += SubscribeTriggerTarget;
                    room.OnRemoveAllyList += SubscribeTriggerTarget;
                    break;

                case TriggerTarget.RoomEnemy:
                    NpcTriggerTarget = room.EnteredEnemys;
                    room.OnAddEnemyList += SubscribeTriggerTarget;
                    room.OnRemoveEnemyList += UnsubscribeTriggerTarget;
                    break;

                case TriggerTarget.AllAlly:
                    var allyManager = room.OwnerManager.GameManager.AllyManager_;
                    NpcTriggerTarget = allyManager.AllAlly;
                    allyManager.OnAddAllAllyList += SubscribeTriggerTarget;
                    allyManager.OnRemoveAllAllyList += UnsubscribeTriggerTarget;
                    break;

                case TriggerTarget.AllEnemy:
                    var enemyManager = room.OwnerManager.GameManager.EnemyManager_;
                    NpcTriggerTarget = enemyManager.AllEnemy;
                    enemyManager.OnAddAllEnemyList += SubscribeTriggerTarget;
                    enemyManager.OnRemoveAllEnemyList += UnsubscribeTriggerTarget;
                    break;

                default:
                    return;
            }

            SubscribeTriggerTarget(NpcTriggerTarget);
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