using System;

namespace BHSSolo.DungeonDefense.State
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">Enum to Use</typeparam>
    /// <typeparam name="T2">Blackboard To Use</typeparam>
    public interface IState_<T1, T2> where T1 : Enum
    {
        public T1 StateType { get; set; }
        public T2 BlackBoard { get; set; }
        public void InitializeState(T2 blackBoard);
        public void StateEnter();
        public void StateUpdate();
        public void StateExit();
    }
}