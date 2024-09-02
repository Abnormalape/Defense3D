using UnityEngine;
using BHSSolo.DungeonDefense.Function;

class MouseChaser : MonoBehaviour
{
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 hitPosition;

        if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer("GridFloor")))
        {
            hitPosition = hit.point;

            double posX = hitPosition.x;
            double posZ = hitPosition.z;

            posX = StaircaseFunction.CalculateStair(posX, 5d);
            posZ = StaircaseFunction.CalculateStair(posZ, 5d);

            this.transform.position = new Vector3((float)posX, hitPosition.y, (float)posZ);
        }
    }
}