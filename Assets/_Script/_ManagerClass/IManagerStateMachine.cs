using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public interface IManagerStateMachine
    {
        public IState_ CurrentState { get; set; }

        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; }


        public void OnInitializeManager_StateMachine();

        public void AddState();

        public void RemoveState();

        public void ChangeManagerState();

        public void OnChangeManagerState();
    }
}