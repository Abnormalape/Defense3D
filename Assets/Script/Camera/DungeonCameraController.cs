using System.Collections;
using UnityEngine;

namespace BHSSolo.DungeonDefense.DungeonCamera
{
    public class DungeonCameraController : MonoBehaviour
    {
        private Vector3 thirdPersonSight;
        private Vector3 firstPersonSight = new Vector3(0f,0.35f,0f);
        private bool _isThirdPersonSight = true;
        private void Start()
        {
            thirdPersonSight = transform.localPosition;
            StartCoroutine(ChangeCameraMode());
        }

        IEnumerator ChangeCameraMode()
        {
            Debug.Log("Camera Coroutine Start");
            while (true)
            {
                yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.C));

                _isThirdPersonSight = !_isThirdPersonSight;
                Debug.Log($"Third: {_isThirdPersonSight}");
                SetCameraSight(_isThirdPersonSight);

                yield return null;
            }
        }

        private void SetCameraSight(bool isThirdPersonSight)
        {
            if (isThirdPersonSight)
                transform.localPosition = thirdPersonSight;
            else
                transform.localPosition = firstPersonSight;
        }
    }
}
