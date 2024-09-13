using BHSSolo.DungeonDefense.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class PlayerOnCharacter : PlayerState, IState_
    {
        public override PlayerManager_ PlayerManager_ { get; set; }
        public override PlayerState_ PlayerState_ { get; set; } = PlayerState_.PlayerOnCharacter;
        public override PlayerInput PlayerInput { get; set; }


        private CharacterController characterController_;
        private InteractableGameObjectFinder interactableGameObjectFinder;
        private InteractableManager_ interactableManager_;


        public override void InitializePlayerState(PlayerManager_ ownerManager)
        {
            PlayerManager_ = ownerManager;
            interactableManager_ = PlayerManager_.OwnerManager.InteractableManager_;

            this.PlayerInput = GetComponent<PlayerInput>();
            characterController_ = GetComponent<CharacterController>();

            interactableGameObjectFinder = new(this);

            TurnOffPlayerInput();
        }

        [SerializeField] private float MoveSpeed;
        [SerializeField] private float MouseSpeed;
        [SerializeField] private Transform CameraTarget;
        private Vector2 movementVector;
        private void OnMove(InputValue value)
        {
            Vector2 input = value.Get<Vector2>();
            movementVector = input;
        }

        private void OnHorizontal(InputValue value)
        {
            float mouseMoved = value.Get<float>() / 3f;
            this.transform.rotation *= Quaternion.Euler(0, mouseMoved, 0);
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
            characterController_.Move(((transform.right * movementVector.x) + (transform.forward * movementVector.y)) * Time.deltaTime * MoveSpeed);

            interactableManager_.SetTargetInteractableGameObject(
                interactableGameObjectFinder.FindInteractableGameObject());
        }

        public override void TurnOffPlayerInput()
        {
            this.PlayerInput.enabled = false;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        public override void TurnOnPlayerInput()
        {
            this.PlayerInput.enabled = true;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
