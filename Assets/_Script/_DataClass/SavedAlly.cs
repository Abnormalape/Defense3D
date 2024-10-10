using System;
using System.Collections.Generic;
namespace BHSSolo.DungeonDefense.Data
{
    [Serializable]
    public class SavedAlly
    {
        public int savedAllyID;
        public int AllyID;
        public int level;
        public List<int> status;
    }
}
