using BHSSolo.DungeonDefense.Contruct;
using BHSSolo.DungeonDefense.ManagerClass;
using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    public abstract class EnemyController_ : MonoBehaviour, IControllerStateMachine
    {
        protected EnemyManager_ enemyManager_;
        public EnemyManager_ EnemyManager { get => enemyManager_; }

        public int Enemy_ID { get; set; }

        public IState_ CurrentState { get; set; }
        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; } = new(10);

        public List<DungeonGridData> SearchedPath { get; private set; }
        private DungeonGridData startGrid;
        private DungeonGridData endGrid;
        private List<DungeonGridData> travledCrossRoads = new(20);
        private List<DungeonGridData> travledForks = new(20);

        public virtual void InitializeEnemyController()
        {
            InitializeStateDictionary();
            ChangeControllerState(EnemyStates.SearchPath); //Todo:
        }

        protected virtual void Update()
        {
            CurrentState?.StateUpdate();
        }

        public void ChangeControllerState(Enum stateName)
        {
            if (Type_StateDictionary[stateName] == CurrentState)
                return;

            Debug.Log("Enemy State Changing...");
            CurrentState?.StateExit();
            CurrentState = Type_StateDictionary[stateName];
            OnChangeControllerState();
            CurrentState.StateEnter();
        }

        public void InitializeStateDictionary()
        {
            var tempEnemyStates = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => typeof(IState_).IsAssignableFrom(t)
                                         && typeof(IEnemyState).IsAssignableFrom(t)
                                         && !t.IsInterface
                                         && !t.IsAbstract).ToList();

            foreach (var enemyState in tempEnemyStates)
            {
                var tempEnemyState = Activator.CreateInstance(enemyState);

                IEnemyState tempIEnemyState = tempEnemyState as IEnemyState;
                tempIEnemyState.InitializeEnemyState(this);
                AddState(tempIEnemyState.EnemyState, tempEnemyState as IState_);
            }
        }

        public void AddState(Enum stateName, IState_ state)
        {
            Type_StateDictionary.Add(stateName, state);
        }

        public void OnChangeControllerState()
        {
            Debug.Log("Enemy State OnChange...");
        }

        public void SetSearchedPath(List<DungeonGridData> searchedPath)
        {
            SearchedPath.Clear();
            SearchedPath = searchedPath;
        }
    }
}
