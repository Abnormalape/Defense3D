using System.Collections.Generic;
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


        private List<DungeonGridData> connectedRooms = new(4);
        public List<DungeonGridData> ConnectedRooms
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

                isContructed = value;
            }
        }



        public Vector3 ConstructedPosition { get; private set; }
        private MeshRenderer visulaizer;

        public bool IsVisible { get; private set; }

        public bool IsRoad { get; set; } //Todo: Adjust

        public void SetVisible()
        {
            if (visulaizer != null)
            {
                visulaizer.enabled = true;
                IsVisible = true;
            }
        }

        public void SetInvisible()
        {
            if (visulaizer != null)
            {
                visulaizer.enabled = false;
                IsVisible = false;
            }
        }
    }
}
