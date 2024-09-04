using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class Player3DController_ : MonoBehaviour, IController, IPlayerController
    {
        public PlayerManager_ playerManager_ { get; set; }

        public IState_ CurrentPlayerState { get; private set; }

        [SerializeField] public float moveSpeed = 5f;
        [SerializeField] public float mouseSensitivity = 100f;
        public Transform playerCamera;

        private CharacterController characterController;
        private float xRotation = 0f;


        private void Awake()
        {
            if (playerCamera == null)
                playerCamera = Camera.main.transform;
            ControllerInitializer();
        }

        void Start()
        {
            characterController = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            ReactToInput();
        }

        public void ControllerInitializer()
        {
            SetPlayerManager();
        }

        public void ReactToInput()
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

        public void SetPlayerManager()
        {
            playerManager_ = FindFirstObjectByType<PlayerManager_>();
        }
    }
}
