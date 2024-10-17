using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.ManagerClass;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BHSSolo.DungeonDefense.State
{
    public class OnManage_IdleCursor : IState_<CursorState, CursorManager>, ICursorState
    {
        public CursorManager BlackBoard { get; set; }
        public CursorState StateType { get; set; } = CursorState.OnManage_Idle;
        public void InitializeState(CursorManager blackBoard)
        {
            BlackBoard = blackBoard;
        }

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

        private bool clicked = false;
        public void StateUpdate()
        {
            //Todo: new Input System
            if (Input.GetMouseButtonDown(0) && !clicked) //Todo: Not work On UI;
            {
                clicked = true;
            }

            if (Input.GetMouseButtonUp(0) && clicked)
            {
                clicked = false;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000f, 1 << LayerMask.NameToLayer("Room")))
                {
                    RoomController tempRoonController = hit.transform.GetComponent<RoomController>();
                    tempRoonController.OnRoomClicked();
                }
            }
        }
    }
}
