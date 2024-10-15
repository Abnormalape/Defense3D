using BHSSolo.DungeonDefense.Contruct;
using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class AllyController_ : MonoBehaviour, IControllerStateMachine
    {
        public abstract NPCCurrentStatus AllyStatus_ { get; set; }

        public abstract NPCStatus AllyBaseStatus_ { get; set; }

        public abstract AllyType AllyEnum_ { get; set; }

        public abstract AllyManager_ AllyManager_ { get; set; }

        public abstract int level { get; set; }

        public abstract int Ally_ID { get; set; }
        //public int Ally_ID { get; set; }

        public abstract BattleManager_ BattleManager_ { get; set; } //BattleManager

        //===



        public IState_ CurrentState { get; set; }
        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; } = new(10);

        #region FindPath
        //public List<DungeonGridData> SearchedPath { get; private set; } = new();
        //private DungeonGridData startGrid;
        //private DungeonGridData endGrid;
        //private List<DungeonGridData> travledCrossRoads = new(20);
        //private List<DungeonGridData> travledForks = new(20);


        //public List<DungeonGridData> ExcludeGrids { get; private set; } = new(20);
        //public void AddExclusion(DungeonGridData exclusion)
        //{
        //    if (!ExcludeGrids.Contains(exclusion))
        //        ExcludeGrids.Add(exclusion);
        //}
        #endregion FindPath

        public virtual void InitializeAllyController()
        {
            InitializeStateDictionary();
            ChangeControllerState(AllyStates.Idle); //Todo:
        }

        protected virtual void Update()
        {
            CurrentState?.StateUpdate();
        }

        public void ChangeControllerState(Enum stateName)
        {
            if (Type_StateDictionary[stateName] == CurrentState)
                return;

            CurrentState?.StateExit();
            CurrentState = Type_StateDictionary[stateName];
            OnChangeControllerState();
            CurrentState.StateEnter();
        }

        public void InitializeStateDictionary()
        {
            var tempAllyStates = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => typeof(IState_).IsAssignableFrom(t)
                                         && typeof(IAllyState).IsAssignableFrom(t)
                                         && !t.IsInterface
                                         && !t.IsAbstract).ToList();

            foreach (var allyState in tempAllyStates)
            {
                var tempAllyState = Activator.CreateInstance(allyState);

                IAllyState tempIAllyState = tempAllyState as IAllyState;
                tempIAllyState.InitializeAllyState(this);
                AddState(tempIAllyState.AllyState, tempAllyState as IState_);
            }
        }

        public void AddState(Enum stateName, IState_ state)
        {
            Type_StateDictionary.Add(stateName, state);
        }

        public void OnChangeControllerState()
        {
        }
    }

    public enum AllyType
    {
        None,
        Goblin,
        Skeleton
    }
}
