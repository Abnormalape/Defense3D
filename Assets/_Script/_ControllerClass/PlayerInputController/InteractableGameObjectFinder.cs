using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public class InteractableGameObjectFinder
    {
        public InteractableGameObjectFinder(Player3DController_ FinderHolder)
        {
            this.finderHolder = FinderHolder;
        }


        private Player3DController_ finderHolder;
        private float searchDistance = 3f;


        public void OnFinderUpdate()
        {

        }

        public InteractableController FindInteractableGameObject()
        {
            Collider[] finds
                = Physics.OverlapSphere(
                    finderHolder.transform.position,
                    searchDistance,
                    1 << LayerMask.NameToLayer("InteractableObject"));

            Collider resultCollider = null;
            float distanceOfResult = 0f;

            Debug.Log(finds.Length);

            foreach (Collider c in finds)
            {
                float instantDistance
                    = Vector3.Distance(
                            finderHolder.transform.position,
                            c.transform.position);

                if (resultCollider == null)
                {
                    resultCollider = c;
                    distanceOfResult = instantDistance;
                    continue;
                }
                else if (distanceOfResult >= instantDistance)
                {
                    resultCollider = c;
                    distanceOfResult = instantDistance;
                    continue;
                }
            }

            if (resultCollider == null)
                return null;
            else
                return resultCollider.GetComponent<InteractableController>();
        }
    }
}
