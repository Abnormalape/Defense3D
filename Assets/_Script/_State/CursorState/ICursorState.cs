using BHSSolo.DungeonDefense.ManagerClass;

namespace BHSSolo.DungeonDefense.State
{
    public interface ICursorState
    {
        public CursorManager CursorManager_ { get; set; }

        public CursorState CursorState { get; set; }

        public void InitialzieCursorState(CursorManager cursorManager);
    }
}
