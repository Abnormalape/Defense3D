using UnityEngine;
using UnityEngine.InputSystem;

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
        public event InputActionEvent OnManageMove;
        public event InputActionEvent OnManageZoom;

        private void OnPlayerMoveAction(InputValue value) => OnPlayerMove?.Invoke(value);
        private void OnPlayerHorizontalAction(InputValue value) => OnPlayerHorizontal?.Invoke(value);
        private void OnPlayerVerticalAction(InputValue value) => OnPlayerVertical?.Invoke(value);
        private void OnManageMoveAction(InputValue value) => OnManageMove?.Invoke(value);
        private void OnManageZoomAction(InputValue value) => OnManageZoom?.Invoke(value);
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