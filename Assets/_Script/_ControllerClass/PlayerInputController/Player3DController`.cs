using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class Player3DController_ : MonoBehaviour, IController, IPlayerController, IInputReactor
    {
        public PlayerManager_ playerManager_ { get; set; }
        public IManagerClass OwnerManager { get; set; }

        public IState_ CurrentPlayerState { get; private set; }

        [SerializeField] public float moveSpeed = 5f;
        [SerializeField] public float mouseSensitivity = 100f;
        public Transform playerCamera;

        private CharacterController characterController;
        private InteractableGameObjectFinder interactableGameObjectFinder; //Only For 3D
        private InteractableManager_ interactableManager;
        private InputManager_ inputManager;
        private float xRotation = 0f;


        private void Awake()
        {
            if (playerCamera == null)
                playerCamera = Camera.main.transform;
            //ControllerInitializer(); //Todo: Adjust
            SetPlayerManager(); //Todo: Remove
            interactableGameObjectFinder = new(this); //Todo: Remove
            SubScribeInputManager(); //Todo: Remove
        }

        void Start()
        {
            characterController = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            ReactToInput();

            SendTargetToInteractManager();
        }

        public void ControllerInitializer(IManagerClass owenerManager)
        {
            Debug.Log("Player Initializer");
            interactableGameObjectFinder = new(this);
            SetPlayerManager();
            SubScribeInputManager();
        }

        public void ReactToInput()
        {
            Player3DMovement();
        }

        public void SetPlayerManager() //Todo: Adjust This Method To Need Parameter (IManagerClass)
        {
            OwnerManager = FindFirstObjectByType<PlayerManager_>();
            playerManager_ = (PlayerManager_)OwnerManager;
            interactableManager = playerManager_.OwnerManager.InteractableManager_;
            inputManager = playerManager_.OwnerManager.InputManager_;
            Debug.Log(interactableManager);
        }

        private void Player3DMovement()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;

            characterController.Move(move * moveSpeed * Time.deltaTime);
        }

        private void SendTargetToInteractManager() //Change as Event?
        {
            interactableManager
                .SetTargetInteractableGameObject(
                    interactableGameObjectFinder
                    .FindInteractableGameObject());
        }

        public void SubScribeInputManager()
        {
            inputManager.OnKeyPress += GetKeyDownReaction;
            inputManager.OnKeyPressing += GetKeyReaction;
            inputManager.OnKeyUnpress += GetKeyUpReaction;
        }

        public void GetKeyDownReaction(KeyCode key)
        {
            if (key == KeyCode.W) { Debug.Log("State1 W Pressed"); }
            if (key == KeyCode.A) { Debug.Log("State1 A Pressed"); }
            if (key == KeyCode.S) { Debug.Log("State1 S Pressed"); }
            if (key == KeyCode.D) { Debug.Log("State1 D Pressed"); }
        }

        public void GetKeyReaction(KeyCode key)
        {
            if (key == KeyCode.W) { Debug.Log("State1 W Pressing"); }
            if (key == KeyCode.A) { Debug.Log("State1 A Pressing"); }
            if (key == KeyCode.S) { Debug.Log("State1 S Pressing"); }
            if (key == KeyCode.D) { Debug.Log("State1 D Pressing"); }
            if (key == KeyCode.E) { Debug.Log("State2 E Pressing"); }
            if (key == KeyCode.Q) { Debug.Log("State2 Q Pressing"); }
        }

        public void GetKeyUpReaction(KeyCode key)
        {
            if (key == KeyCode.W) { Debug.Log("State1 W Unpressed"); }
            if (key == KeyCode.A) { Debug.Log("State1 A Unpressed"); }
            if (key == KeyCode.S) { Debug.Log("State1 S Unpressed"); }
            if (key == KeyCode.D) { Debug.Log("State1 D Unpressed"); }
        }
    }
}
