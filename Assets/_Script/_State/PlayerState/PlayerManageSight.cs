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


        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float scrollSpeed = 1f;
        private Vector2 inputVector;

        public override void InitializePlayerState(PlayerManager_ ownerManager)
        {
            PlayerManager_ = ownerManager;

            this.PlayerInput = GetComponent<PlayerInput>();

            TurnOffPlayerInput();
        }

        private void OnMove(InputValue value)
        {
            inputVector = value.Get<Vector2>();
        }

        private void OnZoom(InputValue value)
        {
            float zoomRate = value.Get<Vector2>().y / 100f;
            Vector3 tempVector = new Vector3(1f / 2f, -1f, -1f / 2f) * zoomRate * scrollSpeed;

            if (this.transform.position.y + tempVector.y > 100f
                || this.transform.position.y + tempVector.y < 10f)
                return;

            this.transform.position += tempVector;
        }

        private void MoveDirection(Vector2 input)
        {
            if (input == null)
                return;

            float x = input.x * moveSpeed;
            float y = input.y * moveSpeed;

            this.transform.position += new Vector3(-x, 0, -x) * Time.deltaTime;
            this.transform.position += new Vector3(y, 0, -y) * Time.deltaTime;
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
            MoveDirection(inputVector);
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
