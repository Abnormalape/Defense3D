using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BHSSolo.DungeonDefense.State
{
    public class OnManage_GridCursor : IState_<CursorState, CursorManager>, ICursorState
    {
        public CursorManager BlackBoard { get; set; }
        public CursorState StateType { get; set; } = CursorState.OnManage_Grid;
        public void InitializeState(CursorManager blackBoard)
        {
            BlackBoard = blackBoard;
        }

        public CursorManager CursorManager_ { get; set; }
        public CursorState CursorState { get; set; } = CursorState.OnManage_Grid;

        private DungeonConstructManager DungeonConstructManager_ { get; set; }

        private bool mousePressed = false;

        private GameObject gridObject;

        public void InitialzieCursorState(CursorManager cursorManager)
        {
            CursorManager_ = cursorManager;
            DungeonConstructManager_ = CursorManager_.GameManager.DungeonConstructManager_; //Todo:
        }

        public void StateEnter()
        {
            Debug.Log("Cursor State : Manage_Grid");
            gridObject = CursorManager_.SummonGridTarget();
        }

        public void StateExit()
        {
            CursorManager_.DestroyGridTarget();
        }

        public void StateUpdate()
        {
            if (Input.GetMouseButtonDown(0) && !mousePressed)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("Pointer Pressed");
                    mousePressed = true;
                }
            }

            if (Input.GetMouseButtonUp(0) && mousePressed)
            {
                mousePressed = false;
                Debug.Log("You clicked on Manage_GridCursor State.");

                DungeonConstructManager_.ConstructionProgress.JudgePositionIsBuildable(gridObject.transform.position);
            }
        }


    }
}