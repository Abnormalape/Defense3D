using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;

namespace BHSSolo.DungeonDefense.State
{
    public class OnPlayerCursor : IState_<CursorState, CursorManager>
    {
        public CursorManager BlackBoard { get; set; }
        public CursorState StateType { get; set; } = CursorState.OnPlayer;
        public void InitializeState(CursorManager blackBoard)
        {
            BlackBoard = blackBoard;
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
