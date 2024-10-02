using BHSSolo.DungeonDefense.State;
using System.Collections.Generic;
using System;

namespace BHSSolo.DungeonDefense.Controller
{
    public interface IControllerStateMachine
    {
        public IState_ CurrentState { get; set; }

        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; }

        public void ChangeControllerState(Enum stateName);

        public void InitializeStateDictionary();

        public void AddState(Enum stateName, IState_ state);

        public void OnChangeControllerState();
    }
}
