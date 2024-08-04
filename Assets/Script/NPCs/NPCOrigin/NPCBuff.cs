using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.NPCs
{
    /// <summary>
    /// NPC가 가지는 버프의 생성자.
    /// </summary>
    public class NPCBuff
    {
        private Dictionary<string, string> _buffData;
        private NPCBuffController _buffHolder;

        public NPCBuff(NPCBuffController buffHolder, Dictionary<string, string> buffData)
        {
            _buffData = buffData;
            _buffHolder = buffHolder;
        }

        //============================================================
        public BuffName BuffName { get; private set; }
        public BuffTrigger BuffTrigger { get; private set; }
        public BuffType BuffType { get; private set; }
        public object buffGiver { get; private set; } //버프제공자. Trait인가, Equipment인가, 그외의 누군가인가?.
        //============================================================
        private Dictionary<string, string> buffData;

        private NPC _buffProvider;

        private bool _observeTargetFound;   //Observe Target Finding has Finished. If true, Observe target may be "me".
                                            //If false, this keeps finding observation target.

        private bool _effectTargetFound;    //Effect Target Finding has Finished. I true, Effect target may be "me".
                                            //If false, this keeps finding effection target.

        //public NPCBuff(string buffName, NPC buffOwner) //buffOwner Means Who Contains buff.
        //{
        //    if (buffOwner is Hero) { _buffProvider = (Hero)buffOwner; }
        //    else if (buffOwner is Monster) { _buffProvider = (Monster)buffOwner; }
        //    else { return; }

        //    buffData = FindSingleDictionaryFromMultipleDictionary.FindDictionaryFromDictionary
        //        (Data.GameData.BuffData, buffName);
        //}

        private List<object> _subscribedObserveTarget; // Room, Monster, Hero, Mawang, Champion etc
        private List<object> _subscribedEffecetTarget; // Room, Monster, Hero, Mawang, Champion etc


        /// <summary>
        /// This method makes ActivateBuff subscribe to appropriate event.
        /// To find appropriate event, use buffData["ObserveTarget"] and buffData["ObserveTargetAction"]
        /// Before Day(or Battle) Begins, this runs.
        /// </summary>
        //private void SubdcribeToObserveTargetEvent()
        //{
        //    //If Observing Target is "Me" subscribe "ActivateBuff" to "Me"'s EventHandler.
        //    if (buffData["ObserveTarget"] == "Me")
        //    {
        //        if (buffData["ObserveTargetAction"] == "OnAttack")
        //        {
        //            _buffProvider.NPCEventHandler.OnNPCAttack += ActivateBuff;
        //        }
        //        else if (buffData["ObserveTargetAction"] == "OnAttacked")
        //        {
        //            _buffProvider.NPCEventHandler.OnNPCAttacked += ActivateBuff;
        //        }
        //        else if (buffData["ObserveTargetAction"] == "OnDead")
        //        {
        //            _buffProvider.NPCEventHandler.OnNPCDead += ActivateBuff;
        //        }
        //    }
        //    //Else if Observing Target is not "Me" it will run in "Room" Scale.
        //    else if (buffData["ObserveTarget"] == "Room")
        //    {
        //        int observingRange = Convert.ToInt32(buffData["ObserveTargetRange"]);

        //        List<DungeonRoom.DungeonRoom> observingRooms =
        //            FindRoomsNearby.FindDungeonRoomsByRange(_buffProvider.placingDungeonRoom, observingRange);

        //        //If Buff activates when Hero Enters at room.
        //        if (buffData["ObserveTargetAction"] == "OnHeroEnter")
        //        {
        //            for (int ix = 0; ix < observingRooms.Count; ++ix)
        //            {
        //                observingRooms[ix].dungeonRoomInstanceEventManager.OnHeroEnter += ActivateBuff;
        //            }
        //        }
        //        //If Buff activates when Hero Exit from room.
        //        else if (buffData["ObserveTargetAction"] == "OnHeroExit")
        //        {
        //            for (int ix = 0; ix < observingRooms.Count; ++ix)
        //            {
        //                observingRooms[ix].dungeonRoomInstanceEventManager.OnHeroExit += ActivateBuff;
        //            }
        //        }
        //    }
        //    //If observe Target is Hero. 
        //    else if (buffData["ObserveTarget"] == "Hero")
        //    {
        //        int observingRange = Convert.ToInt32(buffData["ObserveTargetRange"]);

        //        List<DungeonRoom.DungeonRoom> observingRooms =
        //            FindRoomsNearby.FindDungeonRoomsByRange(_buffProvider.placingDungeonRoom, observingRange);

        //        if (buffData["ObserveTargetAction"] == "OnHeroDead")
        //        {
        //            for (int ix = 0; ix < observingRooms.Count; ++ix)
        //            {
        //                observingRooms[ix].dungeonRoomInstanceEventManager.OnHeroDead += ActivateBuff;
        //            }
        //        }
        //    }
        //    //If observe Target is Monster.
        //    else if (buffData["ObserveTarget"] == "Monster")
        //    {
        //        int observingRange = Convert.ToInt32(buffData["ObserveTargetRange"]);

        //        List<DungeonRoom.DungeonRoom> observingRooms =
        //            FindRoomsNearby.FindDungeonRoomsByRange(_buffProvider.placingDungeonRoom, observingRange);

        //        if (buffData["ObserveTargetAction"] == "OnMonsterDead")
        //        {
        //            for (int ix = 0; ix < observingRooms.Count; ++ix)
        //            {
        //                observingRooms[ix].dungeonRoomInstanceEventManager.OnMonsterDead += ActivateBuff;
        //            }
        //        }
        //    }
        //    else { return; }
        //}

        ///// <summary>
        ///// This method makes ActivateBuff Unsubscribe certain event.
        ///// After Day(or Battle) ends, this runs.
        ///// Todo: Think other Way. => It should be Better to Reset all Events when Day(or Battle) Finished.
        ///// </summary>
        //private void UnsubdcribeToObserveTargetEvent()
        //{
        //}

        //private List<DungeonRoom.DungeonRoom> _buffTargetRooms;

        //private Dictionary<int, NPC> _buffTargetNPCs;
        //private void AddBuffTarget(NPC targetNPC) { _buffTargetNPCs.Add(targetNPC.NPCID ,targetNPC);} //Todo: OnHeroEnter needs npc as parameter.
        //private void AbstractBuffTarget(NPC targetNPC) { _buffTargetNPCs.Remove(targetNPC.NPCID); } //Todo: Also HeroExit and HeroDead.

        ///// <summary>
        ///// This Method Subscribes Room's event, so Buff Can know who are targets.
        ///// This Method Runs whem buff Instantiates, or BuffRange or Room changes.
        ///// </summary>
        //private void SubscribeBuffTargetRoom()
        //{
        //    if (buffData["BuffTarget"] == "Me")
        //    {
        //        AddBuffTarget(_buffProvider); //Buff Provider is Target.
        //    }
        //    else if (buffData["BuffTarget"] == "Hero") 
        //    {
        //        foreach (var e  in _buffTargetRooms)
        //        {
        //            e.dungeonRoomInstanceEventManager.OnHeroEnter += AddBuffTarget;
        //            e.dungeonRoomInstanceEventManager.OnHeroExit += AbstractBuffTarget;
        //            e.dungeonRoomInstanceEventManager.OnHeroDead += AbstractBuffTarget;
        //            //AddBuffTarget might Run when HeroEnter.
        //            //AbstractBuffTarget might Run when HeroExit Or dead.
        //        }
        //    }
        //    else if (buffData["BuffTarget"] == "Monster")
        //    {
        //        foreach (var e in _buffTargetRooms)
        //        {
        //            e.dungeonRoomInstanceEventManager.OnMonsterDead += AbstractBuffTarget;
        //        }
        //    }
        //}

        ////Todo: Think other way => Reset _buffTargetNPCs.
        //private void UnsubscribeBuffTargetRoom()
        //{

        //}

        ///// <summary>
        ///// This Method Activates Buff.
        ///// This runs on certain event.
        ///// Ex) if there is buff like(when "me" attacks, "me" get damage) , This method runs get damage.
        ///// Todo: nPC do nothing in this method?
        ///// </summary>
        //private void ActivateBuff(NPC nPC) 
        //{
        //    if (buffData["BuffType"] == "BuffModifier") //Add or Remove Target's Buff.
        //    {
        //        foreach (var e in _buffTargetNPCs)
        //        {
        //            e.Value.NPCBuffController.AddBuff(); //Todo: call target's buffcontroller and run its method.
        //        }
        //    }
        //    else if (buffData["BuffType"] == "StatusModifier") //Modify Target's Status Like 
        //    {
        //        foreach (var e in _buffTargetNPCs)
        //        {
        //            e.Value.NPCStatusController.ModifyStatus(); //Todo: call target's Status modifier and run its method.
        //        }
        //    }
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