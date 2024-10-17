using BHSSolo.DungeonDefense.State;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class DungeonManageBuild : IState_<GameState, GameStateManager_>, IGameState
    {
        public GameStateManager_ BlackBoard { get; set; }
        public GameState StateType { get; set; } = GameState.Dungeon_ObserveState;
        public void InitializeState(GameStateManager_ blackBoard)
        {
            BlackBoard = blackBoard;
        }


        public GameStateManager_ GameStateManager_ { get; set; }
        public GameState GameState { get; set; } = GameState.Dungeon_ConstructionState;

        public void InitialzieGameState()
        {
            Debug.Log("Game State Initialized");
        }

        public void StateEnter()
        {
            Debug.Log("Now Dungeon Build State.");
        }

        public void StateExit()
        {

        }

        public void StateUpdate()
        {

        }
    }
}
