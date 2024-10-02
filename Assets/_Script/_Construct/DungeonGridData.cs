using UnityEngine;

namespace BHSSolo.DungeonDefense.Contruct
{
    public class DungeonGridData
    {
        public DungeonGridData(bool isConstructed, bool isRoad, Vector3 constructedPosition)
        {
            IsContructed = isConstructed;
            IsRoad = isRoad;
            ConstructedPosition = constructedPosition;
        }


        private GameObject gridObject; //Green Grid
        public GameObject GridObject
        {
            get => gridObject;
            set
            {
                gridObject = value;
                visulaizer = gridObject.GetComponent<MeshRenderer>();
            }
        }


        private DungeonGridData[] connectedRooms = new DungeonGridData[4];
        public DungeonGridData[] ConnectedRooms
        {
            get
            {
                return connectedRooms;
            }
            set
            {
                connectedRooms = value;
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

        public bool IsRoad { get; set; } //Todo: Adjust

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
