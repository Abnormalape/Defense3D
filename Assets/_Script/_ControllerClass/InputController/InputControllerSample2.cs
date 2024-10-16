using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class InputControllerSample2 : InputController_, IController
    {
        public IManagerClass OwnerManager { get; set; }
        public override KeyCode[] AllowedKeys { get; set; } = {
            KeyCode.Q,
            KeyCode.E };
        public override InputState_enum InputState { get; set; } = InputState_enum.Sample2;


        public void InitializeController(IManagerClass ownerManager)
        {
            OwnerManager = ownerManager;
        }
    }
}
