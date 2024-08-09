using UnityEngine;

namespace BHSSolo.DungeonDefense.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] public float moveSpeed = 5f;
        [SerializeField] public float mouseSensitivity = 100f;
        public Transform playerCamera;

        private CharacterController characterController;
        private float xRotation = 0f;

        private void Awake()
        {
            if (playerCamera == null)
                playerCamera = Camera.main.transform;
        }

        void Start()
        {
            characterController = GetComponent<CharacterController>();

            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
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
    }
}