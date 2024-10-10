using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.Data
{
    [Serializable]
    public class PlayerData
    {
        public List<RoomBluePrint> RoomBluePrints;
        public DungeonResources DungeonResources;
        public List<DungeonGrid> DungeonGrids;
        public List<DungeonRoom> DungeonRooms;
        public List<SavedAlly> SavedAlly;
        public List<SavedAddOn> SavedAddOn;
    }
}
