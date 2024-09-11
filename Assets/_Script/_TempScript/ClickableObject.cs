using BHSSolo.DungeonDefense.Function;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Test
{
    public class ClickableObject : MonoBehaviour
    {
        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer("ClickableObject")))
            {
                Debug.Log(hit.transform.gameObject.name);
                if(Input.GetMouseButtonDown(0))
                {
                    Debug.Log("ClickableObjectClicked");
                }
            }
        }
    }
}
