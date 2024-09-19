using BHSSolo.DungeonDefense.State;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DungeonManageState : IState_, IGameState
    {
        public GameStateManager_ GameStateManager_ { get; set; }

        public GameState GameState { get; set; } = GameState.Dungeon_ObserveState;


        public readonly float TimeScale = 1f;


        public void InitialzieGameState()
        {
            Debug.Log("Game State Initialized");
        }

        public void StateEnter()
        {
            Debug.Log("Now Dungeon Observe State.");
        }

        public void StateExit()
        {

        }

        public void StateUpdate()
        {

        }

    }
}
