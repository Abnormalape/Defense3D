using UnityEngine;

namespace BHSSolo.DungeonDefense.Grid
{
    /// <summary>
    /// Use When "Building Room Mode"
    /// </summary>
    public class RoomGridController : MonoBehaviour
    {
        private void Update()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1f, 1 << LayerMask.NameToLayer("GridFloor")))
            {
                if (!hit.transform.GetComponent<FloorGridController>())
                { return; }
                if (!hit.transform.GetComponent<FloorGridController>().HasBuilding)
                {
                    
                }
            }
        }
    }
}
