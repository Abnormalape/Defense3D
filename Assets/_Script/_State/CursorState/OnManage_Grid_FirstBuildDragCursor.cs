using BHSSolo.DungeonDefense.Contruct;
using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace BHSSolo.DungeonDefense.Controller
{
    public class OnManage_Grid_FirstBuildDragCursor : IState_, ICursorState
    {
        public CursorManager CursorManager_ { get; set; }
        public CursorState CursorState { get; set; } = CursorState.OnManage_Grid_FirstBuildDrag;

        private DungeonConstructManager dungeonConstructManager_;
        private GameStateManager_ gameStateManager_;
        private RoomManager_ roomManager_;

        private DungeonGridData selectedGrid = null;
        private Dictionary<Vector3, DungeonGridData> sideGrids = new(24);
        private Dictionary<Vector3, DungeonGridData> nearbyGrids = new(4);


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
            dungeonConstructManager_.ShowGrids(dungeonConstructManager_.RoomSideGrids);
            sideGrids = dungeonConstructManager_.RoomSideGrids;
            Debug.Log("Drag State Enter");
        }

        public void StateExit()
        {
            dungeonConstructManager_.HideAllGrids();

            nearbyGrids.Clear();
            sideGrids.Clear();
            selectedGrid = null;
            dungeonConstructManager_.RoomSideGrids.Clear(); //Todo: Don't execute this in this class.

            Debug.Log("Drag State Exit");
        }

        public void StateUpdate()
        {
            if(Input.GetKey(KeyCode.Escape) || Input.GetMouseButtonDown(1))
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
                Debug.Log("Mouse Button Down");

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
                Debug.Log("Mouse Button Up");

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

                        if (!tempGridData.IsVisible)
                        {
                            Debug.Log("Not Visible Grid. Select Visible Grid.");
                            nearbyGrids.Clear();
                            selectedGrid = null;
                            //Warning UI : Please Select Visible grid.

                            //Nearby But not Connectable.
                        }
                        else
                        {
                            //Todo: Conditions
                            //Like => if(tempGridData.IsRoad)
                            Debug.Log("That Room Is Visible.");

                            selectedGrid.ConnectedGrids.Add(tempGridData);
                            tempGridData.ConnectedGrids.Add(selectedGrid);

                            Debug.Log($"Grid on Position : ({selectedGrid.ConstructedPosition}) is Connected to Grid on Position : ({selectedGrid.ConnectedGrids[0].ConstructedPosition})");

                            //roomManager_.SummonGameObject(null , null); //Todo:

                            ExitConstructionState(); //Todo: Adjust
                            return;
                        }
                    }
                    else
                    {
                        // Warning UI : Not Nearby.
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
            CursorManager_.ChangeManagerState(CursorState.OnManage_Idle);
            
            gameStateManager_.ChangeManagerState(GameState.Dungeon_ConstructionState);
        }

        private void ShowNearbyGrids()
        {
            dungeonConstructManager_.HideAllGrids();
            dungeonConstructManager_.ShowGrids(nearbyGrids);
        }
    }
}
