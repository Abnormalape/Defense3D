using UnityEngine;
using BHSSolo.DungeonDefense.Controller;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class PlayerManager_ : MonoBehaviour, IManagerClass, IStateMachineOwner<PlayerManager_, PlayerState_>//, IManagerStateMachine
    {
        public GameManager_ OwnerManager { get; set; }


        private GameStateManager_ GameStateManager_;


        
        [SerializeField] private GameObject managerSightGameObject;
        public GameObject ManagerSightGameObject => managerSightGameObject;
        [SerializeField] private GameObject characterGameObject;
        public GameObject CharacterGameObject => characterGameObject;



        private void Update()
        {
            if (OwnerManager == null) return;
            StateMachine.CurrentState.StateUpdate();
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            this.GameStateManager_ = OwnerManager.GameStateManager_;
            this.GameStateManager_.OnGameStateChanged += GameStateReactor;

            InitializeStateMachine(this);
            ChangeState(PlayerState_.PlayerOnCharacter);
        }

        private void GameStateReactor(GameState gameState)
        {
            if (gameState == GameState.Dungeon_ObserveState || gameState == GameState.Dungeon_ConstructionState)
                ChangeState(PlayerState_.PlayerManageSight);
            else
                ChangeState(PlayerState_.PlayerOnCharacter);


            Debug.Log("Player Manager React To GameState Change.");
            Debug.Log($"Player Manager Know Current State is {gameState}.");
        }

        //==========================================//
        public CustomStateMachine<PlayerManager_, PlayerState_> StateMachine { get; set; }
        public void InitializeStateMachine(PlayerManager_ stateBlackBoard)
        {
            StateMachine = new(stateBlackBoard);
            ChangeState(PlayerState_.PlayerOnCharacter);
        }

        public void ChangeState(PlayerState_ state)
        {
            StateMachine.ChangeState(state);
        }
    }

    public enum PlayerState_
    {
        PlayerManageSight,
        PlayerOnCharacter,
    }
}