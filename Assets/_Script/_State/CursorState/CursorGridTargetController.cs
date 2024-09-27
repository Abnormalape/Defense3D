using System;
using UnityEngine;

namespace BHSSolo.DungeonDefense.State
{
    public class CursorGridTargetController : MonoBehaviour
    {
        private Vector2 roomSize;


        private void Update()
        {
            transform.localScale = new Vector3(roomSize.x, 0.4f, roomSize.y) * 5f;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer("GridFloor")))
            {
                Vector3 tempPosition = new();
                tempPosition.y = hit.point.y;

                float xPosition = hit.point.x;
                float zPosition = hit.point.z;


                if (roomSize.x % 2 == 0)
                { tempPosition.x = EvenSizeGridPosition(xPosition); }
                else
                { tempPosition.x = (int)Math.Round(xPosition / 5.0) * 5; }

                if (roomSize.y % 2 == 0)
                { tempPosition.z = EvenSizeGridPosition(zPosition); }
                else
                { tempPosition.z = (int)Math.Round(zPosition / 5.0) * 5; }

                transform.position = tempPosition;
            }
        }

        public void InitializeGridTarget(GameObject roomToSummon, Vector2 roomToSummonSize)
        {
            Debug.Log("InitializeGridTarget!");
            roomSize = roomToSummonSize;
            Debug.Log(roomSize);

            Instantiate(roomToSummon, transform).transform.localPosition = Vector3.zero;
        }

        private float EvenSizeGridPosition(float position)
        {
            int divided = (int)(position / 5);
            float evenGridPosition = divided * 5;

            if (position < 0) { evenGridPosition -= 2.5f; }
            else { evenGridPosition += 2.5f; }

            return evenGridPosition;
        }
    }
}
