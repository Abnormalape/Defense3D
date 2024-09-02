using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BHSSolo.DungeonDefense.UI
{
    public class DungeonManageUI_Example : UI_Base
    {
        public override void Show()
        {
            base.Show();
            SetDungeonManageUI();
        }

        public override void Hide()
        {
            base.Hide();
            ResetDungeonManageUI();
        }

        protected override void InputAction()
        {
            base.InputAction();
            Debug.Log("InputAction overrides as [DungeonManageUI]");
        }

        private void SetDungeonManageUI()
        {
            Debug.Log("UI Has Set");
        }

        private void ResetDungeonManageUI()
        {
            Debug.Log("UI Has Reset");
        }
    }
}
