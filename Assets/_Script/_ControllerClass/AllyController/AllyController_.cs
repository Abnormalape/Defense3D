using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class AllyController_ : MonoBehaviour
    {
        public abstract AllyStatus_ AllyStatus_ { get; set; }

        public abstract AllyBaseStatus AllyBaseStatus_ { get; set; }

        public abstract AllyType AllyEnum_ { get; set; }

        public abstract AllyManager_ AllyManager_ { get; set; }

        public abstract int level { get; set; }

        public abstract int Ally_ID { get; set; }

        public abstract void AllyControllerInitializer(AllyBaseStatus statusFound);
    }
}
