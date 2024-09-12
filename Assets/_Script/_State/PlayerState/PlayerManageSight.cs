using BHSSolo.DungeonDefense.State;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class PlayerManageSight : PlayerState, IState_
    {
        public override PlayerManager_ PlayerManager_ { get; set; }
        public override PlayerState_ PlayerState_ { get; set; } = PlayerState_.PlayerManageSight;
        public override PlayerInput PlayerInput { get; set; }

        public override void InitializePlayerState(PlayerManager_ ownerManager)
        {
            PlayerManager_ = ownerManager;

            this.PlayerInput = GetComponent<PlayerInput>();

            TurnOffPlayerInput();
        }

        private void OnMove(InputValue value)
        {
            Vector2 inputKey = value.Get<Vector2>();

            MoveDirection(inputKey);
        }

        private void MoveDirection(Vector2 input)
        {
            float x = input.x;
            float y = input.y;

            transform.position += new Vector3(x, y, 0f);
        }

        public void StateEnter()
        {
            TurnOnPlayerInput();
        }

        public void StateExit()
        {
            TurnOffPlayerInput();
        }

        public void StateUpdate()
        {

        }

        public override void TurnOffPlayerInput()
        {
            this.PlayerInput.enabled = false;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        public override void TurnOnPlayerInput()
        {
            this.PlayerInput.enabled = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }
}
