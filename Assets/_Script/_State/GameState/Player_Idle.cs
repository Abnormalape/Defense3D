using BHSSolo.DungeonDefense.State;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class Player_Idle : IState_<GameState, GameStateManager_>, IGameState
    {
        public GameStateManager_ BlackBoard { get; set; }
        public GameState StateType { get; set; } = GameState.Player_IdleState;
        public void InitializeState(GameStateManager_ blackBoard)
        {
            BlackBoard = blackBoard;
        }


        public GameStateManager_ GameStateManager_ { get; set; }

        public GameState GameState { get; set; } = GameState.Player_IdleState;


        public readonly float TimeScale = 1f;


        public void InitialzieGameState()
        {
            Debug.Log("Game State Initialized");
        }

        public void StateEnter()
        {
            Debug.Log("Now Player Idle State.");
        }

        public void StateExit()
        {
            
        }

        public void StateUpdate()
        {
            
        }

    }
}
