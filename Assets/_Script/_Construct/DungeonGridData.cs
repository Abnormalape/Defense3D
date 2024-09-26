using UnityEngine;

namespace BHSSolo.DungeonDefense.Contruct
{
    public class DungeonGridData
    {
        public DungeonGridData(bool isConstructed, Vector3 constructedPosition)
        {
            IsContructed = isConstructed;
            ConstructedPosition = constructedPosition;
        }


        private GameObject gridObject;
        public GameObject GridObject
        {
            get => gridObject;
            set
            {
                gridObject = value;
                visulaizer = gridObject.GetComponent<MeshRenderer>();
            }
        }


        private bool isContructed = false;
        public bool IsContructed
        {
            get => isContructed;
            set
            {
                if (isContructed == value)
                    return;
                else
                {
                    isContructed = value;

                    if (isContructed)
                        SetInvisible();
                    else
                        SetVisible();
                }
            }
        }
        public Vector3 ConstructedPosition;
        private MeshRenderer visulaizer;

        public bool IsRoad { get; private set; } = true; //Todo: Adjust

        public void SetVisible()
        {
            if (visulaizer != null)
                visulaizer.enabled = true;
        }

        public void SetInvisible()
        {
            if (visulaizer != null)
                visulaizer.enabled = false;
        }
    }
}
