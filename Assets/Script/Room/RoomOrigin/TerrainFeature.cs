using BHSSolo.DungeonDefense.Management;
using BHSSolo.DungeonDefense.NPCs;
using BHSSolo.DungeonDefense.Function;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    /// <summary>
    /// DungeonRoomEffect use as Initializer.
    /// And when it Initialized, Methods in this class Subscribe Certain Events.
    /// </summary>
    class TerrainFeature
    {
        //private DungeonRoom EffectOwner; //DungeonRoom which has DungeonRoomEffect List.

        private Dictionary<string, string> effectData;
        public TerrainFeature(string RoomEffectName)
        {
            effectData = FindSingleDictionaryFromMultipleDictionary.FindDictionaryFromDictionary
                (Data.GameData.DungeonRoomEffectData, RoomEffectName);

            //InitObserveTarget();
            //InitEffectTarget();
        }

        //private void InitObserveTarget()
        //{
        //    //Subscribe Owner Room's Event
        //    if (effectData["ObserveTarget"] == "MyRoom")
        //    {
        //        if (effectData["EffectTrigger"] == "OnHeroExit")
        //        {
        //            EffectOwner.dungeonRoomInstanceEventManager.OnHeroExit += ActivateEffectToEffectTarget;
        //        }
        //        else if (effectData["EffectTrigger"] == "OnHeroEnter")
        //        {
        //            EffectOwner.dungeonRoomInstanceEventManager.OnHeroEnter += ActivateEffectToEffectTarget;
        //        }
        //        else if (effectData["EffectTrigger"] == "OnHeroDead")
        //        {
        //            EffectOwner.dungeonRoomInstanceEventManager.OnHeroDead += ActivateEffectToEffectTarget;
        //        }
        //        else if (effectData["EffectTrigger"] == "OnMonsterDead")
        //        {
        //            EffectOwner.dungeonRoomInstanceEventManager.OnMonsterDead += ActivateEffectToEffectTarget;
        //        }
        //    }
        //    //Subscribe Target Rooms' Event
        //    else if (effectData["ObserveTarget"] == "OtherRoom")
        //    {
        //        if (effectData["ObserveTargetRange"] == "All")
        //        {
        //            //Todo: if (effectData["EffectTrigger"] == "OnHeroExit") {}
        //            // foreach(DungeonRoom e in DungeonRoomManager.AllDungeonRooms)
        //            // { e.dungeonRoomInstanceEventManager.Event += ActivateEffectToEffectTarget; }
        //        }
        //        else if (effectData["ObserveTargetRange"] == "None")
        //        {
        //            //this Doesn't make Sense.
        //        }
        //        else
        //        {
        //            //Todo: List<DungeonRoom> instRooms = FindRoomsNearby.FindDungeonRoomsByRange
        //            // (EffectOwner,Convert.ToInt32(effectData["ObserveTargetRange"]))
        //            // if (effectData["EffectTrigger"] == "OnHeroExit") {}
        //            // foreach (DungeonRoom e in instRooms)
        //            // { e.dungeonRoomInstanceEventManager.Event += ActivateEffectToEffectTarget; }
        //        }
        //    }
        //    else if (effectData["ObserveTarget"] == "None") 
        //    {
        //        //In this Case Effect Activates only one time When Day Started.
        //        if (effectData["EffectTrigger"] == "OnSet") 
        //        { 
        //            //Todo:
        //            GameManager.OnBattlePhaseStarted += ActivateEffectToEffectTarget;
        //        }
        //        else if (effectData["EffectTrigger"] == "OnTime") 
        //        { }
        //        else 
        //        { }
        //    }
        //    else
        //    {
        //        //this Doesn't make Sense. 
        //    }
        //}
        private void AddObserveTarget() { }
        private void RemoveObserveTarget() { }
        private void ResetObserveTarget() { }
        private void SubscribeObserveTargetEvent() { }
        private void InitEffectTarget() { }
        private void AddEffectTarget() { }
        private void RemoveEffectTarget() { }
        private void ResetEffectTarget() { }

        private void ActivateEffectToEffectTarget(NPC nPC) { } //Room's EventManager Contains NPC parameter.
        private void ActivateEffectToEffectTarget() { } //GameManager's EventManaget Do not Contains NPC parameter.
    }
}