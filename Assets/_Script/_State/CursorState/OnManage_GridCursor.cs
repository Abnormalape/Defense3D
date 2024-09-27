using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BHSSolo.DungeonDefense.State
{
    public class OnManage_GridCursor : IState_, ICursorState
    {
        public CursorManager CursorManager_ { get; set; }
        public CursorState CursorState { get; set; } = CursorState.OnManage_Grid;

        private DungeonConstructManager DungeonConstructManager_ { get; set; }
        private Vector2 CenterPosition;
        private bool mousePressed = false;


        public void InitialzieCursorState(CursorManager cursorManager)
        {
            CursorManager_ = cursorManager;
            DungeonConstructManager_ = CursorManager_.OwnerManager.DungeonConstructManager_; //Todo:
        }

        public void StateEnter()
        {
            Debug.Log("Cursor State : Manage_Grid");
            CursorManager_.SummonGridTarget();
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
                Debug.Log("You clicked on Manage_GridCursor State.");
                DungeonConstructManager_.JudgeIsBuildable();
                mousePressed = false;
            }
        }
    }
}