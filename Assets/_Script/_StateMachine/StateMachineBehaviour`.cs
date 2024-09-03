using UnityEngine;

namespace BHSSolo.DungeonDefense.State
{
    public class StateMachineBehaviour_
    {
        public void OnStateEnter()
        {
            Debug.Log("On State Enter");
        }

        public void OnStateUpdate()
        {
            Debug.Log("On State Update");
        }

        public void OnStateExit()
        {
            Debug.Log("On State Exit");
        }
    }
}
