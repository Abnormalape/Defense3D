using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.State
{
    public class OnManage_IdleCursor : IState_, ICursorState
    {
        public CursorManager CursorManager_ { get; set; }
        public CursorState CursorState { get; set; } = CursorState.OnManage_Idle;

        public void InitialzieCursorState(CursorManager cursorManager)
        {
            CursorManager_ = cursorManager;
        }

        public void StateEnter()
        {
            Debug.Log("Idle Cursor State Enter.");
        }

        public void StateExit()
        {
        }

        public void StateUpdate()
        {
        }
    }
}
