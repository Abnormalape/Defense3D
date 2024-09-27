using BHSSolo.DungeonDefense.Contruct;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DungeonConstructManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }


        private GameStateManager_ gameStateManager; //Todo:
        private RoomManager_ RoomManager;
        private CursorManager CursorManager_;
        private DungeonGridSpwner dungeonGridSpwner;
        public Dictionary<Vector3, DungeonGridData> GridDatas = new(10000);

        private bool showingGrid = false;

        private GameObject constructurePlaceHolder;
        public GameObject ConstructurePlaceHolder
        {
            get => constructurePlaceHolder;
            set => constructurePlaceHolder = value;
        }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            this.RoomManager = OwnerManager.RoomManager_;
            this.CursorManager_ = OwnerManager.CursorManager_;
            gameStateManager = OwnerManager.GameStateManager_;
            gameStateManager.OnGameStateChanged += GameStateReaction;

            dungeonGridSpwner = FindFirstObjectByType<DungeonGridSpwner>();

            for (int ix = -50; ix < 51; ix++)
            {
                for (int iz = 0; iz < 101; iz++)
                {
                    bool tempIsBuilt = false;
                    bool tempIsRoad = false;

                    if (ix == 0 && iz == 0)
                    {
                        tempIsBuilt = true;
                        tempIsRoad = true;
                    }


                    float tempX = ix;
                    float tempZ = iz;

                    GridDatas.Add(
                        new Vector3(tempX, 0.01f, tempZ) * 5f
                        , new DungeonGridData(
                            tempIsBuilt
                            , tempIsRoad
                            , new Vector3(tempX, 0.01f, tempZ) * 5f));
                }
            }

            MakeGrid(GridDatas);
        }

        private void GameStateReaction(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Dungeon_ConstructionState:
                    ShowGrid(GridDatas);
                    return;

                default:
                    HideGrid();
                    return;
            }
        }

        public void MakeGrid(Dictionary<Vector3, DungeonGridData> gridDatas)
        {
            dungeonGridSpwner.MakeGrid(gridDatas);
        }

        public void ShowGrid(Dictionary<Vector3, DungeonGridData> gridDatas)
        {
            if (!showingGrid)
            {
                Debug.Log("Show Grid");
                showingGrid = true;
                dungeonGridSpwner.ShowGrid(gridDatas);
            }
        }

        public void HideGrid()
        {
            if (showingGrid)
            {
                Debug.Log("Hide Grid");
                showingGrid = false;
                dungeonGridSpwner.HideGrid();
            }
        }

        public void JudgeIsBuildable()
        {
            #region Field
            Vector3 tempGridTargetPosition = CursorManager_.GridTarget.transform.position;

            float gridTargetYPosition = tempGridTargetPosition.y;
            int holdingRoomXSize = (int)CursorManager_.HoldingRoomSize.x;
            int holdingRoomZSize = (int)CursorManager_.HoldingRoomSize.y;

            List<int> xList = new List<int>(holdingRoomXSize);
            List<int> zList = new List<int>(holdingRoomZSize);

            int roadCounts = (holdingRoomXSize + holdingRoomZSize) * 2;
            List<Vector2> roadPositions = new(roadCounts);

            bool roadFound = false;
            #endregion Field

            Debug.Log("Judge IsBuildable.");
            Debug.Log($"Grid Target Position :  {tempGridTargetPosition}");
            Debug.Log($"Holding Room Size :  ({holdingRoomXSize}, {holdingRoomZSize})");

            #region Set Positions
            if (holdingRoomXSize == 0 || holdingRoomZSize == 0) { Debug.LogAssertion("Room Size Can't Be Zero!"); return; }

            //XPositions.
            if (holdingRoomXSize % 2 == 0) //IsEven
            {
                int xEvenCount = (holdingRoomXSize / 2);
                for (int ix = -xEvenCount; ix <= xEvenCount; ix++)
                {
                    if (ix == 0) continue;

                    int multiplier;
                    if (ix < 0) multiplier = -1;
                    else multiplier = 1;

                    xList.Add((int)(tempGridTargetPosition.x + ((ix - (multiplier)) * 5) + (multiplier * 2.5f)));
                }
            }
            else //IsOdd
            {
                int xOddCount = (holdingRoomXSize - 1) / 2;
                for (int ix = -xOddCount; ix <= xOddCount; ix++)
                {
                    xList.Add((int)(tempGridTargetPosition.x + (ix * 5)));
                }
            }

            //ZPositions.
            if (holdingRoomZSize % 2 == 0) //IsEven
            {
                int zEvenCount = (holdingRoomZSize / 2);
                for (int iz = -zEvenCount; iz <= zEvenCount; iz++)
                {
                    if (iz == 0) continue;

                    int multiplier;
                    if (iz < 0) multiplier = -1;
                    else multiplier = 1;

                    zList.Add((int)(tempGridTargetPosition.z + ((iz - (multiplier)) * 5) + (multiplier * 2.5f)));
                }
            }
            else //IsOdd
            {
                int zOddCount = (holdingRoomZSize - 1) / 2;
                for (int iz = -zOddCount; iz <= zOddCount; iz++)
                {
                    zList.Add((int)(tempGridTargetPosition.z + (iz * 5)));
                }
            }


            //Road Positions.
            foreach (int z in zList)
            {
                roadPositions.Add(new Vector2(xList[0] - 5, z));
                roadPositions.Add(new Vector2(xList[xList.Count - 1] + 5, z));
            }
            foreach (int x in xList)
            {
                roadPositions.Add(new Vector2(x, zList[0] - 5));
                roadPositions.Add(new Vector2(x, zList[zList.Count - 1] + 5));
            }

            //Check Positions.
            foreach (int x in xList)
            {
                foreach (int z in zList)
                {
                    Debug.Log($"Room Positions are : ({x},{z})");
                }
            }
            foreach (Vector2 roadPos in roadPositions)
            {
                Debug.Log($"Road Positions are : ({roadPos.x},{roadPos.y})");
            }
            #endregion Set Positions

            #region IsBuildable
            foreach (int x in xList)
            {
                foreach (int z in zList)
                {
                    Vector3 tempRoomPosition = new(x, gridTargetYPosition, z);
                    if (!GridDatas.ContainsKey(tempRoomPosition))
                    {
                        Debug.Log("Out Of Boundary."); //Todo:
                        return;
                    }
                    else
                    {
                        if (GridDatas[tempRoomPosition].IsContructed)
                        {
                            Debug.Log("Already Builded."); //Todo:
                            return;
                        }
                    }
                }
            }
            foreach (Vector2 road in roadPositions)
            {
                Vector3 tempRoadPosition = new(road.x, gridTargetYPosition, road.y);
                if (GridDatas.ContainsKey(tempRoadPosition))
                {
                    if (CursorManager_.HoldingRoomName == "SamplePassage")
                    {
                        if (GridDatas[tempRoadPosition].IsRoad || GridDatas[tempRoadPosition].IsContructed)
                        {
                            Debug.Log("Can Build Passage");
                            roadFound = true;
                            break;
                        }
                        else
                        {
                            Debug.Log("There is Nothing Nearby");
                        }
                    }
                    else if (CursorManager_.HoldingRoomName == "SampleRoom")
                    {
                        if (GridDatas[tempRoadPosition].IsRoad)
                        {
                            Debug.Log("There Is Road");
                            roadFound = true;
                            break;
                        }
                        else
                        {
                            Debug.Log("There is No Road");
                        }
                    }
                }
            }
            #endregion IsBuildable

            if (!roadFound) //Todo:
                return;

            Debug.Log("You Made A New Room!!");

            //Todo: Set GridData's constructed = true.
            foreach (int x in xList)
            {
                foreach (int z in zList)
                {
                    Vector3 tempRoomPosition = new(x, gridTargetYPosition, z);

                    GridDatas[tempRoomPosition].IsContructed = true;

                    if (CursorManager_.HoldingRoomName == "SamplePassage") //Todo:
                    { GridDatas[tempRoomPosition].IsRoad = true; }
                }
            }
            //Todo: Set GridData's IsRoad as proper Bool.
            RoomManager.AddRoom(CursorManager_.HoldingRoomName);

            CursorManager_.ChangeManagerState(CursorState.OnManage_Idle);
        }
    }
}
