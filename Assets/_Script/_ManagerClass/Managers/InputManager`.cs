using BHSSolo.DungeonDefense.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class InputManager_ : MonoBehaviour, IManagerClass
    {
        public delegate void KeyPress(KeyCode key);
        public delegate void KeyPressing(KeyCode key);
        public delegate void KeyUnpress(KeyCode key);
        public event KeyPress OnKeyPress;
        public event KeyPressing OnKeyPressing;
        public event KeyUnpress OnKeyUnpress;



        public List<IController> ListOfController { get; set; }
        public Dictionary<IController, GameObject> DictionaryOfController { get; set; }
        public Dictionary<Enum, IController> DictionaryEnumController { get; set; } = new();
        public GameManager_ OwnerManager { get; set; }


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
            FindAllAppropriateControllers();
        }

        public void AddToDictionary(IController controller)
        {

        }

        public void AddGameObejctToControllerDictionary(IController controller, GameObject controllerGameObject)
        {

        }

        public void AddToList(IController controller)
        {

        }

        public void RemoveFromDictionary(IController controller)
        {

        }

        public void RemoveFronList(IController controller)
        {

        }

        public void EventLoudSpeaker()
        {

        }

        public void FindAllAppropriateControllers()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();

            var controllerTypes = types.Where(t => typeof(IController).IsAssignableFrom(t)
                                                && typeof(InputController_).IsAssignableFrom(t)
                                                && !t.IsInterface
                                                && !t.IsAbstract).ToList();

            foreach (var controllerType in controllerTypes)
            {
                var inputController = Activator.CreateInstance(controllerType);

                DictionaryEnumController.Add(
                    (inputController as InputController_).InputState,
                    inputController as IController);
            }

            Debug.Log($"State Counts : {DictionaryEnumController.Count}");
            currentInputController = DictionaryEnumController[InputState_enum.Sample] as InputController_; //Todo:
        }

        public void ChangeController(InputState_enum inputState)
        {
            if (currentInputController.InputState != inputState)
                currentInputController = DictionaryEnumController[inputState] as InputController_;
        }
    }

    public enum InputState_enum
    {
        Sample = 0,
        Sample2,
    }
}
