using System.Collections.Generic;
using UnityEngine;
using BHSSolo.DungeonDefense.State;
using System;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class PlayerManager_ : MonoBehaviour, IManagerClass, IManagerStateMachine
    {
        public GameManager_ OwnerManager { get; set; }
        public IState_ CurrentState { get; set; }
        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; } = new();


        private GameStateManager_ GameStateManager_;


        private void Update()
        {
            if(OwnerManager == null) return;

            CurrentState.StateUpdate();
            //StateMachineBehaviour_.OnStateUpdate();
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            this.GameStateManager_ = OwnerManager.GameStateManager_;
            this.GameStateManager_.OnGameStateChanged += GameStateReactor;

            OnInitializeManager_StateMachine();
        }

        public void OnInitializeManager_StateMachine()
        {
            FindPlayerState();
        }

        private void GameStateReactor(GameState gameState)
        {
            if(gameState == GameState.Dungeon_ObserveState || gameState == GameState.Dungeon_ConstructionState)
                ChangeManagerState(PlayerState_.PlayerManageSight);
            else
                ChangeManagerState(PlayerState_.PlayerOnCharacter);


            Debug.Log("Player Manager React To GameState Change.");
            Debug.Log($"Player Manager Know Current State is {gameState}.");
        }

        private void FindPlayerState()
        {
            PlayerState[] playerStates =
                FindObjectsByType<PlayerState>(FindObjectsSortMode.None);

            foreach (PlayerState playerState in playerStates)
            {
                playerState.InitializePlayerState(this);
                AddState(playerState.PlayerState_, playerState as IState_);
            }

            if (Type_StateDictionary.ContainsKey(PlayerState_.PlayerOnCharacter))
                ChangeManagerState(PlayerState_.PlayerOnCharacter);
            else
                Debug.Log("No Idle State Found.");
        }

        public void AddState(Enum stateName, IState_ state_)
        {
            Type_StateDictionary.Add(stateName, state_);
        }

        public void RemoveState(Enum stateName)
        {
            Type_StateDictionary.Remove(stateName);
        }

        public void ChangeManagerState(Enum stateName)
        {
            if (CurrentState == stateName)
                return;

            CurrentState?.StateExit();
            CurrentState = Type_StateDictionary[stateName];
            CurrentState?.StateEnter();

            OnChangeManagerState();
        }

        public void OnChangeManagerState()
        {
            Debug.Log("Player State Changed.");
        }
    }

    public enum PlayerState_
    {
        PlayerManageSight,
        PlayerOnCharacter,
    }
}