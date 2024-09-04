using UnityEngine;

namespace BHSSolo.DungeonDefense.State
{
    public class StateMachineBehaviour_
    {
        private IState_ currentState_;


        public void ChangeState(IState_ newState_)
        {
            currentState_?.Exit();
            currentState_ = newState_;
            currentState_?.Enter();
        }

        public void OnStateUpdate()
        {
            currentState_?.Update();
        }
    }
}
