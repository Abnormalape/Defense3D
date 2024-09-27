using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.State
{
    public class OnManage_TargetRoomCursor : IState_, ICursorState
    {
        public CursorManager CursorManager_ { get; set; }
        public CursorState CursorState { get; set; } = CursorState.OnManage_TargetRoom;

        public void InitialzieCursorState(CursorManager cursorManager)
        {
            CursorManager_ = cursorManager;
        }

        public void StateEnter()
        {
        }

        public void StateExit()
        {
        }

        public void StateUpdate()
        {
        }
    }
}
