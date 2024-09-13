using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class GameStateManager_ : MonoBehaviour, IManagerClass, IManagerStateMachine
    {
        public GameManager_ OwnerManager { get; set; }
        public IState_ CurrentState { get; set; }
        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; } = new(10);
        public StateMachineBehaviour_ StateMachineBehaviour_ { get; set; }


        private void Start()
        {
            
        }

        private void Update()
        {
            StateMachineBehaviour_?.OnStateUpdate();
        }


        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;
            OnInitializeManager_StateMachine();

            ChangeManagerState(GameState.Player_IdleState);
        }

        public void OnInitializeManager_StateMachine()
        {
            StateMachineBehaviour_ = new StateMachineBehaviour_();
            FindGameState();
        }

        private void FindGameState()
        {
            var gameStates = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => typeof(IState_).IsAssignableFrom(t)
                                         && typeof(IGameState).IsAssignableFrom(t)
                                         && !t.IsInterface
                                         && !t.IsAbstract).ToList();

            foreach (var gameState in gameStates)
            {
                var tempGameState = Activator.CreateInstance(gameState);

                IGameState tempIGameState = tempGameState as IGameState;
                tempIGameState.InitialzieGameState();
                AddState(tempIGameState.GameState, tempGameState as IState_);
            }
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
            if (Type_StateDictionary.ContainsKey(stateName))
            {
                CurrentState = Type_StateDictionary[stateName];
            }
            else
            {
                Debug.LogWarning("No State Found.");
                return;
            }

            OnChangeManagerState();
        }

        public void OnChangeManagerState()
        {
            GameState tempGameState = (CurrentState as IGameState).GameState;
            Debug.Log($"Game State Changed Into {tempGameState}");
            OnGameStateChanged?.Invoke(tempGameState);
        }


        public delegate void GameStateChanged(GameState changedInto);
        public event GameStateChanged OnGameStateChanged;
    }

    public enum GameState
    {
        Player_IdleState,
        Player_BattleState,
        DungeonObserveState,
        DungeonBuildState,
    }
}
