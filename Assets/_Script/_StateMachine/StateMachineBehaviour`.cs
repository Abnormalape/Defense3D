using UnityEngine;

namespace BHSSolo.DungeonDefense.State
{
    public class StateMachineBehaviour_
    {
        private IState_ currentState_;


        public void ChangeState(IState_ newState_)
        {
            currentState_?.StateExit();
            currentState_ = newState_;
            currentState_?.StateEnter();
        }

        public void OnStateUpdate()
        {
            currentState_?.StateUpdate();
        }
    }
}
