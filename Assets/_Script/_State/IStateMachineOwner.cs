using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BHSSolo.DungeonDefense.Controller
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">Blackboard</typeparam>
    /// <typeparam name="T2">Enums To Use</typeparam>
    public interface IStateMachineOwner<T1, T2> where T2 : Enum
    {
        public CustomStateMachine<T1, T2> StateMachine { get; set; }
        public void InitializeStateMachine(T1 stateBlackBoard);
        public void ChangeState(T2 state);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">Blackboard</typeparam>
    /// <typeparam name="T2">Enums To Use</typeparam>
    public class CustomStateMachine<T1, T2> where T2 : Enum
    {
        public CustomStateMachine(T1 stateBlackBoard)
        {
            BlackBoard = stateBlackBoard;
            Type_StateDictionary = new();
            InitializeStates();
        }

        private void InitializeStates()
        {
            var tempStates = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(t => typeof(IState_<T2, T1>).IsAssignableFrom(t)
                                         && !t.IsInterface
                                         && !t.IsAbstract).ToList();

            foreach (var state in tempStates)
            {
                var tempState = Activator.CreateInstance(state);

                IState_<T2, T1> tempIState = tempState as IState_<T2, T1>;
                tempIState.InitializeState(BlackBoard);
                AddState(tempIState.StateType, tempState as IState_<T2, T1>);
            }
        }

        public void AddState(T2 stateName, IState_<T2, T1> state)
        {
            Debug.Log(stateName);
            Type_StateDictionary.Add(stateName, state);
        }

        public Dictionary<T2, IState_<T2, T1>> Type_StateDictionary;

        public T1 BlackBoard { get; set; }

        public IState_<T2, T1> CurrentState { get; set; }

        public void ChangeState(T2 stateName)
        {
            IState_<T2, T1> tempState = Type_StateDictionary[stateName];

            CurrentState?.StateExit();
            CurrentState = tempState;
            CurrentState?.StateEnter();
        }
    }
}
//var tempStates = Assembly.GetExecutingAssembly().GetTypes()
//                            .Where(t => t.GetInterfaces().Any(i =>
//                            i.IsGenericType &&
//                            i.GetGenericTypeDefinition() == typeof(IState_<,>) &&
//                            i.GenericTypeArguments[0] == typeof(T2) &&
//                            i.GenericTypeArguments[1] == typeof(T1)))
//                            .Where(t => !t.IsInterface && !t.IsAbstract)
//                            .ToList();