using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.Contruct;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DungeonConstructManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ GameManager { get; set; }


        private GameStateManager_ gameStateManager; //Todo:
        private RoomManager_ RoomManager;
        private CursorManager CursorManager_;
        private DungeonGridSpwner dungeonGridSpawner;
        private DataManager_ dataManager_;

        public Dictionary<Vector3, DungeonGridData> GridDatas { get; private set; } = new(10000);
        public ConstructionProgress ConstructionProgress { get; private set; }

        private List<GridNode> nodeDatas = new();
        public List<GridNode> NodeDatas { get => nodeDatas; }


        private GameObject constructurePlaceHolder;
        public GameObject ConstructurePlaceHolder
        {
            get => constructurePlaceHolder;
            set => constructurePlaceHolder = value;
        }


        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;

            this.RoomManager = GameManager.RoomManager_;
            this.CursorManager_ = GameManager.CursorManager_;
            this.dataManager_ = GameManager.DataManager_;
            gameStateManager = GameManager.GameStateManager_;
            gameStateManager.OnGameStateChanged += GameStateReaction;

            this.ConstructionProgress = new(this);

            dungeonGridSpawner = FindFirstObjectByType<DungeonGridSpwner>();

            SetGridDataUsingMap();
            MakeGrid(GridDatas);

            GridPathFinder.SetNodeGrids(GridDatas.Values.ToList(), out nodeDatas);
        }

        private void GameStateReaction(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Dungeon_ConstructionState:
                    HideAllGrids();
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
        }

        private void MakeTempGrid(string mapData, ref Dictionary<Vector3, DungeonGridData> tempGridMap, out string[] rows)
        {
            string[] internalRows = mapData.Trim('\r').Trim('\n').Split('\n');
            Array.Reverse(internalRows);

            rows = internalRows;

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
            dungeonGridSpawner.ShowAllGrids(gridDatas);
        }

        public void ShowGrids(List<DungeonGridData> gridDatas)
        {
            dungeonGridSpawner.ShowAllGrids(gridDatas);
        }

        public void HideAllGrids()
        {
            dungeonGridSpawner.HideAllGrids(GridDatas);
        }

        public void HideGrids(Dictionary<Vector3, DungeonGridData> gridDatas)
        {
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

        public void TempFindPath_RemoveThisMothod()
        {
            GridPathFinder.SetNodeGrids(GridDatas.Values.ToList(), out nodeDatas);
            //GridPathFinder.FindShortestWay(tempNodes.First(), tempNodes.Last(), tempNodes);

            //Debug.Log("CanRemove: " + GridPathFinder.CanRemoveRoom(tempNodes, tempNodes[1].NodeGrid));

            //Debug.Log("CanReach: " + GridPathFinder.CanReachGoal(tempNodes[0], tempNodes[tempNodes.Count - 1]));
        }
    }
}
