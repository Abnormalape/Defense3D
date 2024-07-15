<<<<<<< HEAD
﻿namespace BHSSolo.DungeonDefense.DungeonRoom
{
    static class RoomDatas
    {

=======
﻿using BHSSolo.DungeonDefense.StaticFunction;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.DungeonRoom
{
    static class RoomDatas
    {
        //Dungeon Room Battle Data.
        [SerializeField] static TextAsset DungeonRoomBattleDataCSV;
        public static List<Dictionary<string, string>> dungeonRoomBattleData { get; private set; }
         = ParseCSV.ParseCsvFile(DungeonRoomBattleDataCSV.text);

        //Dungeon Room Trap Data.
        [SerializeField] static TextAsset DungeonRoomTrapDataCSV;
        public static List<Dictionary<string, string>> dungeonRoomTrapData { get; private set; }
         = ParseCSV.ParseCsvFile(DungeonRoomTrapDataCSV.text);

        //Dungeon Room Function Data.
        [SerializeField] static TextAsset DungeonRoomFunctionDataCSV;
        public static List<Dictionary<string, string>> dungeonRoomFunctionData { get; private set; }
         = ParseCSV.ParseCsvFile(DungeonRoomFunctionDataCSV.text);

        //Dungeon Room Effect Data.
        [SerializeField] static TextAsset DungeonRoomEffectDataCSV;
        public static List<Dictionary<string, string>> dungeonRoomEffectData { get; private set; }
         = ParseCSV.ParseCsvFile(DungeonRoomEffectDataCSV.text);
>>>>>>> 4f7035a0fb0bb46dcbaf538f47ebde1df47f54de
    }
}