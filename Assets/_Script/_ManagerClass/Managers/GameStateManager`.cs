using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class GameStateManager_ : MonoBehaviour, IManagerClass, IManagerStateMachine
    {
        public GameManager_ OwnerManager { get; set; }
        public IState_ CurrentState { get; set; }
        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; }

        public void AddState()
        {
        }

        public void ChangeManagerState()
        {
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
        }

        public void OnChangeManagerState()
        {
        }

        public void OnInitializeManager_StateMachine()
        {
        }

        public void RemoveState()
        {
        }
    }
}
