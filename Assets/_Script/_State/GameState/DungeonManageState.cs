using BHSSolo.DungeonDefense.State;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DungeonManageState : IState_, IGameState
    {
        public GameStateManager_ GameStateManager_ { get; set; }

        public GameState GameState { get; set; } = GameState.DungeonManagementState;


        public readonly float TimeScale = 1f;


        public void InitialzieGameState()
        {
            Debug.Log("Game State Initialized");
        }

        public void StateEnter()
        {
            Debug.Log("Now Dungeon Manage State.");
        }

        public void StateExit()
        {

        }

        public void StateUpdate()
        {

        }

    }
}
