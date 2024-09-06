using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class InputControllerSample : InputController_, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override KeyCode[] AllowedKeys { get; set; } = {
            KeyCode.W,
            KeyCode.A,
            KeyCode.S,
            KeyCode.D,
            KeyCode.F,
            KeyCode.M };
        public override InputState_enum InputState { get; set; } = InputState_enum.Sample;


        public void ControllerInitializer(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
        }
    }
}