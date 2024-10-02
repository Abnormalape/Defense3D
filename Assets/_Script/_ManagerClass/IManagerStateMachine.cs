using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public interface IManagerStateMachine
    {
        public IState_ CurrentState { get; set; }

        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; }

        public void OnInitializeManager_StateMachine();

        public void AddState(Enum stateName, IState_ state_);

        public void RemoveState(Enum stateName);

        public void ChangeManagerState(Enum stateName);

        public void OnChangeManagerState();
    }
}