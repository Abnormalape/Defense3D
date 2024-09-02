using System.Runtime.InteropServices;
using UnityEngine;

namespace Temp
{
    public class Moving : MonoBehaviour
    {
        Camera cam;
        Transform playerBody;
        private float xRotation = 0f;
        public float mouseSensitivity = 100f;

        private void Awake()
        {
            cam = Camera.main;
            playerBody = this.transform;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            // 마우스 입력 값을 받습니다.
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // 마우스 Y 입력값을 통해 카메라를 위아래로 회전합니다.
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // 카메라를 위아래로 회전시킵니다.
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // 플레이어 몸체를 좌우로 회전시킵니다.
            playerBody.Rotate(Vector3.up * mouseX);

            if (Input.GetKey(KeyCode.W))
            {
                playerBody.transform.position += Vector3.forward * 2f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                playerBody.transform.position += Vector3.left * 2f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerBody.transform.position += Vector3.back * 2f * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                playerBody.transform.position += Vector3.right * 2f * Time.deltaTime;
            }
        }
    }
}
