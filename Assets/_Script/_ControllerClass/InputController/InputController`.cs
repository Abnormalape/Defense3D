using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class InputController_
    {
        public abstract InputState_enum InputState { get; set; }

        public abstract KeyCode[] AllowedKeys { get; set; }
    }
}
