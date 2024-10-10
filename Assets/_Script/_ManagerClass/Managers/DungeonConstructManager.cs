﻿using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Contruct;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DungeonConstructManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ OwnerManager { get; set; }


        private GameStateManager_ gameStateManager; //Todo:
        private RoomManager_ RoomManager;
        private CursorManager CursorManager_;
        private DungeonGridSpwner dungeonGridSpawner;
        private DataManager_ dataManager_;

        public Dictionary<Vector3, DungeonGridData> GridDatas { get; private set; } = new(10000);
        public Dictionary<Vector3, DungeonGridData> RoomSideGrids { get; private set; } = new(24);


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
            this.dataManager_ = OwnerManager.DataManager_;
            gameStateManager = OwnerManager.GameStateManager_;
            gameStateManager.OnGameStateChanged += GameStateReaction;

            dungeonGridSpawner = FindFirstObjectByType<DungeonGridSpwner>();

            SetGridDataUsingMap();
            MakeGrid(GridDatas);
        }

        private void GameStateReaction(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Dungeon_ConstructionState:
                    ShowGrids(GridDatas
                        .Where((t) => !t.Value.IsContructed)
                        .ToDictionary(t => t.Key, t => t.Value));
                    return;

                default:
                    HideGrids(GridDatas);
                    return;
            }
        }

        public void SetGridDataUsingMap()
        {
            string mapData = dataManager_.defaultMapTextAsset.text;
            Dictionary<Vector3, DungeonGridData> tempGridMap = new();
            string[] rows;

            MakeTempGrid(mapData, ref tempGridMap, out rows);
            SetTempGridData(rows);
            ConnectGrids(rows);

            Vector3 start = new Vector3(250f, 0.01f, 0f);
            Vector3 end = new Vector3(250f, 0.01f, 80f);

            //Todo: Remove
            List<DungeonGridData> Grids = new();
            foreach (var e in GridDatas) { Grids.Add(e.Value); }
            List<GridNode> nodes;

            GridPathFinder.SetNodeGrids(Grids, out nodes);
            GridPathFinder.FindShortestWay(nodes[0], nodes[9], nodes);
            //Todo: Remove
        }

        private void MakeTempGrid(string mapData, ref Dictionary<Vector3, DungeonGridData> tempGridMap, out string[] rows)
        {
            string[] internalRows = mapData.Trim('\r').Trim('\n').Split('\n');
            Array.Reverse(internalRows);

            rows = internalRows; //Out

            tempGridMap = new(internalRows.Length * internalRows[0].Trim(',').Length);

            int iy = 0;
            foreach (string row in internalRows)
            {
                string tempRow = row.Replace(",", "").Replace("\n", "").Replace("\r", "");

                int ix = 0;
                foreach (char c in tempRow)
                {
                    Vector3 tempGridPosition = new Vector3(ix * 5f, 0.01f, iy * 5f);
                    DungeonGridData tempGrid = new(this, tempGridPosition);
                    tempGridMap.Add(tempGridPosition, tempGrid);
                    ix++;
                }
                iy++;
            }

            GridDatas.AddRange(tempGridMap);
        }

        private void SetTempGridData(string[] rows)
        {
            int iy = 0;
            foreach (string row in rows)
            {
                string[] tempRow = row.Replace("\n", "").Replace("\r", "").Split(",");

                int ix = 0;
                foreach (string s in tempRow)
                {
                    Vector3 tempGridPosition = new Vector3(ix * 5f, 0.01f, iy * 5f);
                    DungeonGridData tempGrid;

                    if (!GridDatas.TryGetValue(tempGridPosition, out tempGrid))
                    { continue; }


                    switch (s)
                    {
                        case string str when str.StartsWith("R"): //RoomCore
                            int roomSize = Convert.ToInt32(s.Replace("R", ""));
                            GameObject tempRoom = RoomManager.TempBuildRoomMethodUsingCsvData(s); //Todo:
                            tempGrid.SetRoomCore(roomSize, tempRoom);
                            break;
                        case "e": //Empty
                            tempGrid.SetEmpty();
                            break;
                        case "E": //Entrance
                            GameObject tempEntrance = RoomManager.TempBuildRoomMethodUsingCsvData("Entrance"); //Todo:
                            tempGrid.SetEntrance(tempEntrance);
                            break;
                        case "r": //Room
                            tempGrid.SetRoom();
                            break;
                        case "+": //Passage
                        case "│":
                        case "─":
                        case "┴":
                        case "┬":
                        case "┤":
                        case "├":
                        case "┘":
                        case "┐":
                        case "└":
                        case "┌":
                        case "╵": //UP
                        case "╷": //DOWN
                        case "╴": //LEFT
                        case "╶": //RIGHT
                            GameObject tempPassage = RoomManager.TempBuildRoomMethodUsingCsvData("Passage"); //Todo:
                            tempGrid.SetPassage(tempPassage);
                            break;
                        case "N": //Void
                            GridDatas.Remove(tempGridPosition);
                            break;
                        default: //Error
                            Debug.Log($"Wrong Word In Map: {s}");
                            break;
                    }
                    ix++;
                }
                iy++;
            }
        }

        private void ConnectGrids(string[] rows)
        {
            int iy = 0;
            foreach (string row in rows)
            {
                string[] tempRow = row.Replace("\n", "").Replace("\r", "").Split(",");

                int ix = 0;
                foreach (string s in tempRow)
                {
                    Vector3 tempGridPosition = new Vector3(ix * 5f, 0.01f, iy * 5f);
                    DungeonGridData tempGrid;

                    if (!GridDatas.TryGetValue(tempGridPosition, out tempGrid))
                    { continue; }

                    switch (s)
                    {
                        case "+":
                            tempGrid.SetConnectedRooms(true, true, true, true);
                            break;
                        case "│":
                            tempGrid.SetConnectedRooms(true, true, false, false);
                            break;
                        case "─":
                            tempGrid.SetConnectedRooms(false, false, true, true);
                            break;
                        case "┴":
                            tempGrid.SetConnectedRooms(true, false, true, true);
                            break;
                        case "┬":
                            tempGrid.SetConnectedRooms(false, true, true, true);
                            break;
                        case "┤":
                            tempGrid.SetConnectedRooms(true, true, true, false);
                            break;
                        case "├":
                            tempGrid.SetConnectedRooms(true, true, false, true);
                            break;
                        case "┘":
                            tempGrid.SetConnectedRooms(true, false, true, false);
                            break;
                        case "┐":
                            tempGrid.SetConnectedRooms(false, true, true, false);
                            break;
                        case "└":
                            tempGrid.SetConnectedRooms(true, false, false, true);
                            break;
                        case "┌":
                            tempGrid.SetConnectedRooms(false, true, false, true);
                            break;
                        case "╵": //UP
                            tempGrid.SetConnectedRooms(true, false, false, false);
                            break;
                        case "╷": //DOWN
                            tempGrid.SetConnectedRooms(false, true, false, false);
                            break;
                        case "╴": //LEFT
                            tempGrid.SetConnectedRooms(false, false, true, false);
                            break;
                        case "╶": //RIGHT
                            tempGrid.SetConnectedRooms(false, false, false, true);
                            break;
                        default:
                            break;
                    }
                    ix++;
                }
                iy++;
            }
        }

        public void MakeGrid(Dictionary<Vector3, DungeonGridData> gridDatas)
        {
            dungeonGridSpawner.MakeGrid(gridDatas);
        }

        public void ShowAllGrids()
        {
            dungeonGridSpawner.ShowAllGrids(GridDatas);
        }

        public void ShowGrids(Dictionary<Vector3, DungeonGridData> gridDatas)
        {
            Debug.Log("Show Grid");
            dungeonGridSpawner.ShowAllGrids(gridDatas);
        }

        public void HideAllGrids()
        {
            dungeonGridSpawner.HideAllGrids(GridDatas);
        }

        public void HideGrids(Dictionary<Vector3, DungeonGridData> gridDatas)
        {
            Debug.Log("Hide Grid");
            dungeonGridSpawner.HideAllGrids(gridDatas);
        }

        public void ShowGrid(DungeonGridData gridData)
        {
            dungeonGridSpawner.ShowGrid(gridData);
        }

        public void HideGrid(DungeonGridData gridData)
        {
            dungeonGridSpawner.HideGrid(gridData);
        }

        public void JudgeIsBuildable(GameObject gridTarget) //Todo: 
        {
            #region Field
            Vector3 tempGridTargetPosition = CursorManager_.GridTarget.transform.position; //Todo: use gridTarget in parameter Instead

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
            { Debug.Log("No Road Found"); return; }

            Debug.Log("You Can Made Room");

            //Todo: Passage and Room Must be Different
            RoomBuildType tempRoomBuildType = RoomBuildType.Passage;//Todo: Remove

            if (tempRoomBuildType == RoomBuildType.Passage)
            {
            }
            else if (tempRoomBuildType == RoomBuildType.Room)
            {
            }

            //Todo: Set GridData's constructed = true.
            //foreach (int x in xList)
            //{
            //    foreach (int z in zList)
            //    {
            //        Vector3 tempRoomPosition = new(x, gridTargetYPosition, z);

            //        GridDatas[tempRoomPosition].IsContructed = true;

            //        if (CursorManager_.HoldingRoomName == "SamplePassage") //Todo:
            //        { GridDatas[tempRoomPosition].IsRoad = true; }
            //    }
            //}

            HideGrids(GridDatas);

            foreach (int x in xList)
            {
                foreach (int z in zList)
                {
                    Vector3 tempRoomPosition = new(x, gridTargetYPosition, z);
                    DungeonGridData tempGridData = GridDatas[tempRoomPosition];

                    //Todo: Condition => if(NearbyRoom == roomType.Passage)
                    Debug.Log(tempRoomPosition);

                    RoomSideGrids.Add(tempRoomPosition, tempGridData);
                }
            }

            //foreach (var e in roadPositions)
            //{
            //    Vector3 tempRoadPosition = new(e.x, gridTargetYPosition, e.y);
            //    if (GridDatas.ContainsKey(tempRoadPosition))
            //    {
            //        DungeonGridData tempGridData = GridDatas[tempRoadPosition];

            //        //Todo: Condition => if(myRoomType == roomType.Passage)
            //        ShowGrid(tempGridData);
            //    }
            //}

            CursorManager_.ChangeManagerState(CursorState.OnManage_Grid_FirstBuildDrag);

            //Todo: Set GridData's IsRoad as proper Bool.
            //RoomManager.AddRoom(CursorManager_.HoldingRoomName); // 9/30 

            //CursorManager_.ChangeManagerState(CursorState.OnManage_Idle); // 9/30
        }

        public void PrepareConstructionPassage() //Todo: Adjust
        { //BuildCondition; 
        }
        private void JudgePassageBuildable() //Click position must be on Construction. On of the four grid around click position must be empty.
        { StretchPassage(); }
        private void StretchPassage() { }
        private void FinishBuildPassage() { } //If "left shift" is pressed and this method runs, goto StretchPassage Method.

        public void PrepareConstructionRoom() //Todo: Adjust
        { //BuildCondition; 
        }
        private void JudgeRoomBuildable() //One of the Grid around room grids except corner, must contains passage grid. Also room grid can't be on Construction.
        { ConnectRoomToPassage(); }
        private void ConnectRoomToPassage() { }
        private void FinishBuildRoom() { }
    }
}
