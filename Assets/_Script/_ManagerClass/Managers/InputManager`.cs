using BHSSolo.DungeonDefense.Controller;
using BHSSolo.DungeonDefense.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class InputManager_ : MonoBehaviour, IManagerClass, IManagerStateMachine
    {
        public delegate void KeyPress(KeyCode key);
        public delegate void KeyPressing(KeyCode key);
        public delegate void KeyUnpress(KeyCode key);
        public event KeyPress OnKeyPress;
        public event KeyPressing OnKeyPressing;
        public event KeyUnpress OnKeyUnpress;



        public Dictionary<Enum, IController> DictionaryEnumController { get; set; } = new();
        public Dictionary<Enum, IState_> Type_StateDictionary { get; set; } = new();
        public GameManager_ OwnerManager { get; set; }
        public IState_ CurrentState { get; set; }

        private InputState_enum currentInputState;
        private InputController_ currentInputController;


        private void Update()
        {
            foreach (KeyCode KC in currentInputController?.AllowedKeys)
            {
                if (Input.GetKeyDown(KC))
                    OnKeyPress?.Invoke(KC);
                else if (Input.GetKey(KC))
                    OnKeyPressing?.Invoke(KC);
                else if (Input.GetKeyUp(KC))
                    OnKeyUnpress?.Invoke(KC);

                //Input.GetAxis;
                //Input.GetAxisRaw;
                //Input.mousePosition;
            }
        }

        public void InitializeManager(GameManager_ gameManager_)
        {
            OwnerManager = gameManager_;

            OnInitializeManager_StateMachine();
        }

        public void ChangeController(InputState_enum inputState)
        {
            if (currentInputController.InputState != inputState)
                currentInputController = DictionaryEnumController[inputState] as InputController_;
        }

        public void OnInitializeManager_StateMachine()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            var controllerTypes = types.Where(t => typeof(IController).IsAssignableFrom(t)
                                                && typeof(InputController_).IsAssignableFrom(t)
                                                && !t.IsInterface
                                                && !t.IsAbstract).ToList();

            foreach (var controllerType in controllerTypes)
            {
                var inputController = Activator.CreateInstance(controllerType);

                DictionaryEnumController.Add( //Todo: remove
                    (inputController as InputController_).InputState,
                    inputController as IController);

                Type_StateDictionary.Add(
                    (inputController as InputController_).InputState,
                    inputController as IState_);
            }

            Debug.Log($"State Counts : {DictionaryEnumController.Count}");
            currentInputController = DictionaryEnumController[InputState_enum.Sample] as InputController_; //Todo: remove
            //currentInputController = Type_StateDictionary[InputState_enum.Sample] as InputController_; //Todo: adjust to IState
        }

        public void AddState(Enum stateName, IState_ state_)
        {
        }

        public void RemoveState(Enum stateName)
        {
        }

        public void ChangeManagerState(Enum stateName)
        {
        }

        public void OnChangeManagerState()
        {
        }
    }

    public enum InputState_enum
    {
        Sample = 0,
        Sample2,
    }
}
