using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.UI;

namespace BHSSolo.DungeonDefense.Contruct
{
    public class DungeonGridData
    {
        public DungeonGridData(DungeonConstructManager dungeonConstructManager, Vector3 constructedPosition) //Empty Grid Setting.
        {
            this.GridType = GridType.Empty;
            this.dungeonConstructManager = dungeonConstructManager;
            this.ConstructedPosition = constructedPosition;
        }


        public GridType GridType { get; private set; }
        private DungeonConstructManager dungeonConstructManager;


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
        private MeshRenderer visulaizer;


        private DungeonGridData containingRoom; //Todo: NOT GridData. Use Room DataInstead.
        public DungeonGridData ContainingRoom { get => containingRoom; set => value = containingRoom; } //Todo: NOT GridData. Use Room DataInstead.

        private List<DungeonGridData> connectedGrids = new(4);
        public List<DungeonGridData> ConnectedGrids
        {
            get
            {
                return connectedGrids;
            }
            set
            {
                connectedGrids = value;
            }
        }

        private List<DungeonGridData> connectedRooms = new(4); //Todo: NOT GridData. Use Room DataInstead.
        public List<DungeonGridData> ConnectedRooms { get => connectedRooms; set => value = connectedRooms; } //Todo: NOT GridData. Use Room DataInstead.


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

        public bool IsVisible { get; private set; }

        public bool IsRoad { get; set; } = true; //Todo: Adjust

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


        private const int GRID_INTERVAL = 5;


        public void SetEmpty() { this.GridType = GridType.Empty; }
        public void SetEntrance() { this.GridType = GridType.Entrance; this.ContainingRoom = null; } //Todo: Grid Need To Have Containing Room.
        public void SetRoom() { this.GridType = GridType.Room; this.ContainingRoom = null; } //Todo: Grid Need To Have Containing Room.
        public void SetRoomCore(int roomSize) //Todo: Grid Need To Have Containing Room.
        {
            this.GridType = GridType.Room;
            List<Vector3> roomGridVectors = new(25);

            if (roomSize % 2 == 0) //Is Even
            {
                int loops = roomSize / 2;

                for (int ix = -loops + 1; ix <= loops; ix++)
                {
                    for (int iz = -loops + 1; iz <= loops; iz++)
                    {
                        roomGridVectors.Add(ConstructedPosition + (new Vector3(ix, 0, iz) * 5f));
                    }
                }
            }
            else //Is Odd
            {
                int loops = (roomSize - 1) / 2;

                for (int ix = -loops; ix <= loops; ix++)
                {
                    for (int iz = -loops; iz <= loops; iz++)
                    {
                        roomGridVectors.Add(ConstructedPosition + (new Vector3(ix, 0, iz) * 5f));
                    }
                }
            }

            foreach(Vector3 v in roomGridVectors)
            {
                dungeonConstructManager.GridDatas[v].ContainingRoom = this; //This???
                Debug.Log($"Room Core Contains: {v}");
            }

            this.ContainingRoom = null;
        }
        public void SetPassage() { this.GridType = GridType.Passage; this.ContainingRoom = null; } //Todo: Grid Need To Have Containing Room.
        public void SetConnectedRooms(bool isUp, bool isDown, bool isLeft, bool isRight)
        {
            List<Vector3> connections = new();
            if (isUp)
            {
                Vector3 connected = ConstructedPosition + new Vector3(0, 0, GRID_INTERVAL);
                connections.Add(connected);
            }
            if (isDown)
            {
                Vector3 connected = ConstructedPosition + new Vector3(0, 0, -GRID_INTERVAL);
                connections.Add(connected);
            }
            if (isLeft)
            {
                Vector3 connected = ConstructedPosition + new Vector3(-GRID_INTERVAL, 0, 0);
                connections.Add(connected);
            }
            if (isRight)
            {
                Vector3 connected = ConstructedPosition + new Vector3(GRID_INTERVAL, 0, 0);
                connections.Add(connected);
            }

            foreach (Vector3 connection in connections)
            {
                Debug.Log(connection);

                DungeonGridData tempGrid = dungeonConstructManager.GridDatas[connection];

                ConnectedGrids.Add(tempGrid);
                ConnectedRooms.Add(tempGrid.ContainingRoom);

                if (tempGrid.GridType == GridType.Entrance || tempGrid.GridType == GridType.Room)
                {
                    tempGrid.SetConnection(this);
                    tempGrid.ConnectedRooms.Add(this.ContainingRoom);
                }
            }
        }
        public void SetConnection(DungeonGridData connectedRoom)
        {
            connectedGrids.Add(connectedRoom);
        }
    }

    public enum GridType
    {
        Empty,
        Entrance,
        Passage,
        Room,
    }
}
