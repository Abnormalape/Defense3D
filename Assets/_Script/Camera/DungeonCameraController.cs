using BHSSolo.DungeonDefense.Function;
using BHSSolo.DungeonDefense.InteractableObject;
using System.Collections;
using System.Text;
using Unity.Mathematics;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace BHSSolo.DungeonDefense.DungeonCamera
{
    public class DungeonCameraController : MonoBehaviour
    {
        private Vector3 cameraPosition { get => transform.position; set => transform.position = value; }
        private Quaternion cameraFacing { get => transform.rotation; set => transform.rotation = value; }

        private Vector3 thirdPersonSight;
        private Vector3 firstPersonSight = new Vector3(0f, 0.35f, 0f);
        private bool _isCameraOnPlayer = true;
        private bool _isThirdPersonSight = true;
        private readonly float ZOOM_TO_ORB_TIME = 2f;


        private void Awake()
        {

        }

        private void Start()
        {
            thirdPersonSight = transform.localPosition;
            StartCoroutine(ChangePlayerCameraMode());
        }

        IEnumerator ChangePlayerCameraMode()
        {
            Debug.Log("Player Chase Camera Coroutine Starts");
            while (_isCameraOnPlayer)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.C));

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

        public void OnInteractDungeonOrb(Vector3 dungeonOrbPostion, System.Action onCompleteMove)
        {
            Debug.Log("zoom into dungeon orb");

            StopCoroutine(ChangePlayerCameraMode());

            _isCameraOnPlayer = false;
            transform.parent = null;

            StartCoroutine(ZoomIntoDungeonOrb(cameraPosition, dungeonOrbPostion, onCompleteMove));
        }

        private IEnumerator ZoomIntoDungeonOrb(Vector3 startPosition, Vector3 dungeonOrbPostion, System.Action onCompleteMove)
        {
            float passedTime = 0f / ZOOM_TO_ORB_TIME;
            bool isNearTarget = false;

            while (!isNearTarget)
            {
                Vector3 horizontalPosition = Vector3.Lerp(startPosition, dungeonOrbPostion, passedTime);

                float yPosition = Mathf.Lerp(startPosition.y, dungeonOrbPostion.y, 2 * passedTime - Mathf.Pow(passedTime, 2));

                cameraPosition = new(horizontalPosition.x, yPosition, horizontalPosition.z);

                passedTime += Time.deltaTime;

                if(Vector3.Distance(cameraPosition, dungeonOrbPostion) <= 0.1f)
                    isNearTarget = true;

                yield return null;
            }

            onCompleteMove();
        }
    }
}