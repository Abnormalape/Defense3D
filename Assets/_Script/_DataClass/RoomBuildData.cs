using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.Data
{
    [Serializable]
    public class RoomBuildData
    {
        public int RoomID;
        public string name;
        public string roomType;
        public int Width;
        public int Depth;
        public int BuildTime;
        public List<Requirement> Requirements;
    }

    [System.Serializable]
    public class RoomBuildDataWrapper
    {
        public List<RoomBuildData> RoomBuildData;
    }
}
