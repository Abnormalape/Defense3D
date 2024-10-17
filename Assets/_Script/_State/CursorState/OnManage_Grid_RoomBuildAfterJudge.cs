using BHSSolo.DungeonDefense.Contruct;
using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BHSSolo.DungeonDefense.Controller
{
    public class OnManage_Grid_RoomBuildAfterJudge : IState_<CursorState, CursorManager>, ICursorState
    {
        public CursorManager BlackBoard { get; set; }
        public CursorState StateType { get; set; } = CursorState.OnManage_Grid_RoomBuildAfterJudge;
        public void InitializeState(CursorManager blackBoard)
        {
            BlackBoard = blackBoard;
        }


        public CursorManager CursorManager_ { get; set; }
        public CursorState CursorState { get; set; } = CursorState.OnManage_Grid_RoomBuildAfterJudge;

        private DungeonConstructManager dungeonConstructManager_;
        private GameStateManager_ gameStateManager_;
        private RoomManager_ roomManager_;

        private DungeonGridData selectedGrid = null;
        private List<DungeonGridData> sideGrids { get; set; } = new(24);
        private Dictionary<Vector3, DungeonGridData> nearbyGrids { get; set; } = new(4);


        public void InitialzieCursorState(CursorManager cursorManager)
        {
            CursorManager_ = cursorManager;
            dungeonConstructManager_ = cursorManager.OwnerManager.DungeonConstructManager_;
            gameStateManager_ = cursorManager.OwnerManager.GameStateManager_;
            roomManager_ = cursorManager.OwnerManager.RoomManager_;
        }

        public void StateEnter()
        {
            dungeonConstructManager_.HideAllGrids();
            dungeonConstructManager_.ShowGrids(dungeonConstructManager_.ConstructionProgress.TempRoomSideGrids);
            sideGrids = dungeonConstructManager_.ConstructionProgress.TempRoomSideGrids;
            Debug.Log("Drag State Enter");
        }

        public void StateExit()
        {
            nearbyGrids.Clear();
            sideGrids.Clear();
            selectedGrid = null;

            Debug.Log("Drag State Exit");
        }

        public void StateUpdate()
        {
            if (Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                ExitConstructionState();
            }

            StartGridSelect();
            EndGridSelect();
        }

        private void StartGridSelect()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && selectedGrid == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer("GridFloor")))
                {
                    Vector3 hitPosition = hit.transform.position;
                    DungeonGridData tempGridData;
                    Dictionary<Vector3, DungeonGridData> tempGridDatas = dungeonConstructManager_.GridDatas;

                    if (tempGridDatas.ContainsKey(hitPosition))
                    {
                        tempGridData = tempGridDatas[hitPosition];
                        if (!tempGridData.IsVisible)
                        {
                            Debug.Log("Not Visible Grid. Select Visible Grid.");
                            return;
                        }
                        else
                        {
                            Debug.Log("Selected Grid's Nearby Rooms are BLAH BLAH.");
                            selectedGrid = tempGridData;

                            Vector3[] nearbyPositions = new Vector3[4];
                            nearbyPositions[0] = new Vector3(hitPosition.x + 5f, hitPosition.y, hitPosition.z);
                            nearbyPositions[1] = new Vector3(hitPosition.x - 5f, hitPosition.y, hitPosition.z);
                            nearbyPositions[2] = new Vector3(hitPosition.x, hitPosition.y, hitPosition.z + 5f);
                            nearbyPositions[3] = new Vector3(hitPosition.x, hitPosition.y, hitPosition.z - 5f);

                            for (int ix = 0; ix < 4; ix++)
                            {
                                if (tempGridDatas.ContainsKey(nearbyPositions[ix]))
                                {
                                    //Todo: Conditions Add
                                    //if (tempGridDatas[nearbyPositions[ix]].IsRoad);
                                    nearbyGrids.Add(nearbyPositions[ix], tempGridDatas[nearbyPositions[ix]]);
                                }
                            }
                            ShowNearbyGrids();
                        }
                    }
                }
            }
        }

        private void EndGridSelect()
        {
            if (Input.GetMouseButtonUp(0) && selectedGrid != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer("GridFloor")))
                {
                    Vector3 hitPosition = hit.transform.position;
                    DungeonGridData tempGridData;
                    Dictionary<Vector3, DungeonGridData> tempGridDatas = dungeonConstructManager_.GridDatas;

                    if (nearbyGrids.ContainsKey(hitPosition))
                    {
                        tempGridData = tempGridDatas[hitPosition];

                        if (!tempGridData.IsVisible || tempGridData.GridType != GridType.Passage)
                        {
                            Debug.Log("Please Select Correct Grid.");
                            nearbyGrids.Clear();
                            selectedGrid = null;
                        }
                        else
                        {
                            Debug.Log("That Room Is Visible.");

                            dungeonConstructManager_.ConstructionProgress.CompleteRoomBuild(selectedGrid, tempGridData);

                            return;
                        }
                    }
                    else
                    {
                        Debug.Log("Please Select Nearby Grid.");
                    }
                }
                nearbyGrids.Clear();
                selectedGrid = null;

                dungeonConstructManager_.HideAllGrids();
                dungeonConstructManager_.ShowGrids(sideGrids);
            }
        }

        private void ExitConstructionState()
        {
            CursorManager_.ChangeState(CursorState.OnManage_Idle);
            gameStateManager_.ChangeState(GameState.Dungeon_ConstructionState);
        }

        private void ShowNearbyGrids()
        {
            dungeonConstructManager_.HideAllGrids();
            dungeonConstructManager_.ShowGrids(nearbyGrids);
        }
    }
}
