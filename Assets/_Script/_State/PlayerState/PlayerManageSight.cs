using BHSSolo.DungeonDefense.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class PlayerManageSight : IState_<PlayerState_, PlayerManager_>
    {
        public PlayerManager_ BlackBoard { get; set; }
        public PlayerState_ StateType { get; set; } = PlayerState_.PlayerManageSight;
        public void InitializeState(PlayerManager_ blackBoard)
        {
            BlackBoard = blackBoard;
            ControllingGameObject = BlackBoard.ManagerSightGameObject;
            ControllingPlayerInput = ControllingGameObject.GetComponent<PlayerInput>();
            TurnOffPlayerInput();
        }

        public GameObject ControllingGameObject { get; set; }
        public PlayerInput ControllingPlayerInput { get; set; }


        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float scrollSpeed = 1f;
        private Vector2 inputVector;

        private void OnMove(InputValue value)
        {
            inputVector = value.Get<Vector2>();
        }

        private void OnZoom(InputValue value)
        {
            float zoomRate = value.Get<Vector2>().y / 100f;
            Vector3 tempVector = new Vector3(1f / 2f, -1f, -1f / 2f) * zoomRate * scrollSpeed;

            if (ControllingGameObject.transform.position.y + tempVector.y > 100f
                || ControllingGameObject.transform.position.y + tempVector.y < 10f)
                return;

            ControllingGameObject.transform.position += tempVector;
        }

        private void MoveDirection(Vector2 input)
        {
            if (input == null)
                return;

            float x = input.x * moveSpeed;
            float y = input.y * moveSpeed;

            ControllingGameObject.transform.position += new Vector3(-x, 0, -x) * Time.deltaTime;
            ControllingGameObject.transform.position += new Vector3(y, 0, -y) * Time.deltaTime;
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

        public void TurnOffPlayerInput()
        {
            ControllingPlayerInput.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }

        public void TurnOnPlayerInput()
        {
            ControllingPlayerInput.enabled = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
