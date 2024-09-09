using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class AllyController_ : MonoBehaviour
    {
        public abstract AllyStatus_ AllyStatus_ { get; set; }

        public abstract Ally_enum AllyEnum_ { get; set; }

        public abstract AllyManager_ AllyManager_ { get; set; }

        public abstract int level { get; set; }

        public abstract void AllyControllerInitializer(AllyStatus_ statusFound);
    }
}
