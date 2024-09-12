using UnityEngine;
using UnityEngine.InputSystem;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public abstract class PlayerState : MonoBehaviour
    {
        public abstract PlayerManager_ PlayerManager_ { get; set; }

        public abstract PlayerState_ PlayerState_ { get; set; }

        public abstract PlayerInput PlayerInput { get; set; }


        public abstract void InitializePlayerState(PlayerManager_ ownerManager);

        public abstract void TurnOnPlayerInput();

        public abstract void TurnOffPlayerInput();
    }
}
