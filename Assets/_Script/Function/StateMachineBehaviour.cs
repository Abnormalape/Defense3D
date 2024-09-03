using BHSSolo.DungeonDefense.NPCs;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Function
{
    public class StateMachineBehaviour : MonoBehaviour
    {
        private NPCs.State _currentState;

        private void Update()
        {
            _currentState.OnStateUpdate();
        }

        public void ChangeState(NPCs.State changingState)
        {
            if (_currentState == changingState) { return; }

            _currentState.OnStateExit();

            _currentState = changingState;

            _currentState.OnStateEnter();
        }
    }
}
