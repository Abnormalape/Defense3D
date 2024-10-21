using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.State
{
    public class OnManage_TargetNPCCursor : IState_<CursorState, CursorManager>
    {
        public CursorManager BlackBoard { get; set; }
        public CursorState StateType { get; set; } = CursorState.OnManage_TargetNPC;
        public void InitializeState(CursorManager blackBoard)
        {
            BlackBoard = blackBoard;
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
