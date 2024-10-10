using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.Data
{
    [Serializable]
    public class DungeonRoom
    {
        public int RoomID;
        public List<int> locateGrid;
        public List<PlacedAlly> placedAlly;
        public List<PlacedAddOn> placedAddOn;
    }
}
