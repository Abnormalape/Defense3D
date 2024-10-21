using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.Controller
{
    [Serializable]
    public class TraitBaseData
    {
        public int TraitID;
        public string TraitName;
        public int[] BuffIDs;
    }

    [Serializable]
    public class TraitDataWrapper
    {
        public List<TraitBaseData> TraitDatas;
    }
}