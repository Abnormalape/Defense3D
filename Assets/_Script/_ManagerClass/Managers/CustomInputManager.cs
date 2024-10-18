using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.Examples.ObjectSpin;

namespace BHSSolo.DungeonDefense.ManagerClass
{
    public class CustomInputManager : MonoBehaviour, IManagerClass
    {
        public GameManager_ GameManager { get; set; }

        public PlayerInput PlayerInput { get; private set; }


        public void InitializeManager(GameManager_ gameManager_)
        {
            GameManager = gameManager_;
            PlayerInput = GetComponent<PlayerInput>();
        }

        public void ChangeInputMap(InputActionMapType inputActionMapType)
        {
            string actionMapName = inputActionMapType.ToString();

            try
            {
                PlayerInput.SwitchCurrentActionMap(actionMapName);
            }
            catch
            {
                Debug.Log($"Incorrect ActionMap: {actionMapName}");
            }
        }

        public delegate void InputActionEvent(InputValue value);

        public event InputActionEvent OnPlayerMove;
        public event InputActionEvent OnPlayerHorizontal;
        public event InputActionEvent OnPlayerVertical;

        private void OnPlayerMoveAction(InputValue value) => OnPlayerMove?.Invoke(value);
        private void OnPlayerHorizontalAction(InputValue value) => OnPlayerHorizontal?.Invoke(value);
        private void OnPlayerVerticalAction(InputValue value) => OnPlayerVertical?.Invoke(value);
    }

    public enum InputActionMapType
    {
        OnPlayer,
        OnManage,
    }

    public enum InputActionType
    {
        PlayerMoveAction,
        PlayerHorizontalAction,
        PlayerVerticalAction,
    }
}