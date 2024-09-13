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
                gridObject = value; visulaizer = gridObject.GetComponent<MeshRenderer>();
            }
        }


        public bool IsContructed;
        public Vector3 ConstructedPosition;
        private MeshRenderer visulaizer;


        public void SetVisible()
        {
            visulaizer.enabled = true;
        }

        public void SetInvisible()
        {
            visulaizer.enabled = false;
        }
    }
}
