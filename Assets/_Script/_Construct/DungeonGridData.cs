﻿using BHSSolo.DungeonDefense.ManagerClass;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.MemoryProfiler;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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


        private GameObject containingRoom; //Todo: NOT GridData. Use Room DataInstead.
        public GameObject ContainingRoom { get => containingRoom; set => containingRoom = value; } //Todo: NOT GridData. Use Room DataInstead.

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

        private DungeonGridData roomCoreGrid;
        public DungeonGridData RoomCoreGrid
        {
            get
            {
                return roomCoreGrid;
            }
            set
            {
                roomCoreGrid = value;
            }
        }

        private List<DungeonGridData> roomCorePartGrid;
        public List<DungeonGridData> RoomCorePartGrid { get => roomCorePartGrid; set => roomCorePartGrid = value; }


        private bool isContructed = false;
        public bool IsContructed
        {
            get => isContructed;
            set
            {
                if (isContructed == value)
                    return;

                isContructed = value;
                //if (IsContructed)
                //{
                //    SetVisible();
                //}
                //else
                //{
                //    SetInvisible();
                //}
            }
        }


        public Vector3 ConstructedPosition { get; private set; }

        public bool IsVisible { get; private set; }

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


        public void SetEmpty() { GridType = GridType.Empty; }
        public void SetRoom()
        {
            GridType = GridType.Room;
            isContructed = true;
        } //Todo: Grid Need To Have Containing Room.
        public void SetEntrance(GameObject InputEntrance)//Todo: Grid Need To Have Containing Room.
        {
            GridType = GridType.Entrance;
            isContructed = true;
            ContainingRoom = InputEntrance;
            ContainingRoom.transform.position = ConstructedPosition;
        }
        public void SetPassage(GameObject InputPassage)//Todo: Grid Need To Have Containing Room.
        {
            GridType = GridType.Passage;
            isContructed = true;
            ContainingRoom = InputPassage;
            ContainingRoom.transform.position = ConstructedPosition;
        }
        public void SetRoomCore(int roomSize, GameObject InputRoom) //Todo: Grid Need To Have Containing Room.
        {
            GridType = GridType.RoomCore;
            isContructed = true;
            ContainingRoom = InputRoom;
            ContainingRoom.transform.position = this.ConstructedPosition;
            roomCorePartGrid = new();

            List<Vector3> roomGridVectors = new(25);

            if (roomSize % 2 == 0) //Is Even
            {
                int loops = roomSize / 2;

                for (int ix = -loops + 1; ix <= loops; ix++)
                {
                    for (int iz = -loops + 1; iz <= loops; iz++)
                    {
                        Vector3 tempPosition = ConstructedPosition + (new Vector3(ix, 0, iz) * 5f);

                        roomGridVectors.Add(tempPosition);
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
                        Vector3 tempPosition = ConstructedPosition + (new Vector3(ix, 0, iz) * 5f);

                        roomGridVectors.Add(tempPosition);
                    }
                }
            }

            foreach (Vector3 v in roomGridVectors)
            {
                DungeonGridData tempPart = dungeonConstructManager.GridDatas[v];
                RoomCorePartGrid.Add(tempPart);
                tempPart.ContainingRoom = this.ContainingRoom;
                tempPart.RoomCoreGrid = this;
            }
        }
        public void SetRoomCore(List<DungeonGridData> rooms, GameObject InputRoom)
        {
            GridType = GridType.RoomCore;
            isContructed = true;
            ContainingRoom = InputRoom;
            roomCorePartGrid = new();

            InputRoom.transform.position = this.ConstructedPosition;

            this.ContainingRoom = InputRoom;

            foreach (var e in rooms)
            {
                RoomCorePartGrid.Add(e);
                e.ContainingRoom = this.ContainingRoom;
                e.RoomCoreGrid = this;
            }
        }

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
                DungeonGridData tempGrid = dungeonConstructManager.GridDatas[connection];

                ConnectedGrids.Add(tempGrid);

                if (tempGrid.GridType == GridType.Entrance)
                {
                    tempGrid.SetConnection(this);
                }
                else if (tempGrid.GridType == GridType.Room || tempGrid.GridType == GridType.RoomCore)
                {
                    tempGrid.SetConnection(this);
                    tempGrid.RoomCoreGrid.SetConnection(this);
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
        RoomCore,
    }
}
