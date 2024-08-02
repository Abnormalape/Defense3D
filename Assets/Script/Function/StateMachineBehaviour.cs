using BHSSolo.DungeonDefense.NPCs;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Function
{
    class StateMachineBehaviour : MonoBehaviour
    {
        private State _currentState;

        private void Update()
        {
            _currentState.OnStateUpdate();
        }

        public void ChangeState(State changingState)
        {
            if (_currentState == changingState) { return; }

            _currentState.OnStateExit();

            _currentState = changingState;

            _currentState.OnStateEnter();
        }
    }
}
