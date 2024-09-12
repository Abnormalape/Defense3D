namespace BHSSolo.DungeonDefense.State
{
    public interface IState_
    {
        public void StateEnter();
        public void StateUpdate();
        public void StateExit();
    }
}