using BHSSolo.DungeonDefense.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class PlayerOnCharacter : IState_<PlayerState_, PlayerManager_>
    {
        public PlayerManager_ BlackBoard { get; set; }
        public PlayerState_ StateType { get; set; } = PlayerState_.PlayerOnCharacter;
        private CustomInputManager customInputManager;

        public void InitializeState(PlayerManager_ blackBoard)
        {
            BlackBoard = blackBoard;
            ControllingGameObject = BlackBoard.CharacterGameObject;
            ControllingPlayerInput = ControllingGameObject.GetComponent<PlayerInput>();
            characterController_ = ControllingGameObject.GetComponent<CharacterController>();

            interactableManager_ = blackBoard.OwnerManager.InteractableManager_;
            interactableGameObjectFinder = new(ControllingGameObject);

            customInputManager = blackBoard.OwnerManager.CustomInputManager_;
            customInputManager.OnPlayerMove += OnMove;
            customInputManager.OnPlayerHorizontal += OnHorizontal;
            customInputManager.OnPlayerVertical += OnVertical;

            TurnOffPlayerInput();
        }

        public GameObject ControllingGameObject { get; set; }
        public PlayerInput ControllingPlayerInput { get; set; }


        private CharacterController characterController_;
        private InteractableGameObjectFinder interactableGameObjectFinder;
        private InteractableManager_ interactableManager_;


        private float MoveSpeed = 3f;
        private float MouseSpeed = 3f;
        private Transform CameraTarget;
        private Vector3 movementVector;

        private void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            movementVector = new(input.normalized.x, 0, input.normalized.y);
        }

        private void OnHorizontal(InputValue value)
        {
            float mouseMoved = value.Get<float>() / 3f;
            ControllingGameObject.transform.rotation *= Quaternion.Euler(0, mouseMoved, 0);
        }

        private void OnVertical(InputValue value)
        {
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
            characterController_.Move(movementVector * Time.deltaTime * MoveSpeed);

            interactableManager_.SetTargetInteractableGameObject(
                interactableGameObjectFinder.FindInteractableGameObject());
        }

        public void TurnOffPlayerInput()
        {
            ControllingPlayerInput.enabled = false;
            Cursor.lockState = CursorLockMode.None;
        }

        public void TurnOnPlayerInput()
        {
            ControllingPlayerInput.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
