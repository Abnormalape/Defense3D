using BHSSolo.DungeonDefense.Controller;
using System.Collections.Generic;
using UnityEngine;
using BHSSolo.DungeonDefense.State;
using System;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class PlayerManager_ : MonoBehaviour, IManagerClass, IManagerStateMachine
    {
        public GameManager_ OwnerManager { get; set; }
        public StateMachineBehaviour_ StateMachineBehaviour_ { get; private set; }
        public IState_ CurrentState { get; set; }
        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; }


        private void Update()
        {
            StateMachineBehaviour_.OnStateUpdate();
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
            StateMachineBehaviour_ = new();
            OwnerManager = gameManager_;
        }

        public void OnInitializeManager_StateMachine()
        {

        }

        public void AddState()
        {
        }

        public void RemoveState()
        {
        }

        public void ChangeManagerState()
        {
        }

        public void OnChangeManagerState()
        {
        }
    }

    public enum PlayerState_
    {
        Player2D,
        Player3D,
    }
}