using BHSSolo.DungeonDefense.Controller;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class GameStateManager_ : MonoBehaviour, IManagerClass, IStateMachineOwner<GameStateManager_, GameState>
    {
        public GameManager_ GameManager { get; set; }

        private void Start()
        {

        }

        private void Update()
        {
        }


        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;

            InitializeStateMachine(this);
        }

        public delegate void GameStateChanged(GameState changedInto);
        public event GameStateChanged OnGameStateChanged;
        //=========================================
        public CustomStateMachine<GameStateManager_, GameState> StateMachine { get; set; }
        public void InitializeStateMachine(GameStateManager_ stateBlackBoard)
        {
            StateMachine = new(stateBlackBoard);
            ChangeState(GameState.Player_IdleState);
        }

        public void ChangeState(GameState state)
        {
            StateMachine.ChangeState(state);
            OnGameStateChanged(state);
        }
    }

    public enum GameState
    {
        Player_IdleState,
        Player_BattleState,
        Dungeon_ObserveState,
        Dungeon_ConstructionState,
    }
}