using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class AllyController_ : MonoBehaviour
    {
        public abstract AllyStatus_ AllyStatus_ { get; set; }

        public abstract Ally_enum AllyEnum_ { get; set; }
    }
}
