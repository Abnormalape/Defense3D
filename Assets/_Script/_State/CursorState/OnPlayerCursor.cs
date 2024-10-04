using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.State
{
    public class OnPlayerCursor : IState_, ICursorState
    {
        public CursorManager CursorManager_ { get; set; }
        public CursorState CursorState { get; set; } = CursorState.OnPlayer;

        public void InitialzieCursorState(CursorManager cursorManager)
        {
            CursorManager_ = cursorManager;
        }

        public void StateEnter()
        {
            Debug.Log("Enter Cursor State : OnPlayerCursor");
        }

        public void StateExit()
        {
        }

        public void StateUpdate()
        {
            //Debug.Log("Current Cursor State : OnPlayerCursor");
        }
    }
}
