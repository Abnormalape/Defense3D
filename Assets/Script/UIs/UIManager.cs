using System;
using System.Collections.Generic;
using UnityEditor;

namespace BHSSolo.DungeonDefense.UI
{
    public class UIManager
    {
        public UIManager()
        {
            _uis = new Dictionary<Type, IUI>();
            _popups = new List<IUI>();
        }

        private const int SCREEN_DEFAULT_SORTS_ORDER = -10;
        private Dictionary<Type, IUI> _uis;
        private IUI _screen;
        private List<IUI> _popups;

        /// <summary>
        /// Add UI to Dictionary. No Duplicant.
        /// </summary>
        /// <param name="ui">UI to register.</param>
        /// <exception cref="Exception">Duplicant ui register try exception.</exception>
        public void Register(IUI ui)
        {
            if (_uis.TryAdd(ui.GetType(), ui) == false)
                throw new Exception($"[{nameof(UIManager)}] : Failed to register {ui}. Already exist.");
        }

        /// <summary>
        /// Get UI by type.
        /// </summary>
        /// <typeparam name="T">Ui type to get.</typeparam>
        /// <returns>Ui instace.</returns>
        /// <exception cref="Exception">No Ui in Hierarchy.</exception>
        public T Get<T>()
            where T : IUI
        {
            if (_uis.TryGetValue(typeof(T), out IUI ui))
                return (T)ui;

            throw new Exception($"[{nameof(UIManager)}] : Failed to get {typeof(T)}. Not exist");
        }

        /// <summary>
        /// Set screen Ui and reorder.
        /// </summary>
        /// <param name="ui">Screen Ui to show as new</param>
        public void SetScreen(IUI ui)
        {
            if (_screen != null)
            {
                _screen.Hide();
            }

            ui.sortingOrder = SCREEN_DEFAULT_SORTS_ORDER;
            ui.Show();
        }

        /// <summary>
        /// Add to Popup stack's last then reorder.
        /// </summary>
        /// <param name="ui">new popup to show</param>
        public void Push(IUI ui)
        {
            _popups.Add(ui);
            ui.sortingOrder = _popups.Count;
        }

        /// <summary>
        /// Search popup to remove from last of Popup stack. Then remove popup. Then reorder.
        /// </summary>
        /// <param name="ui">Popup to remove.</param>
        /// <exception cref="Exception">No popup but try to remove.</exception>
        public void Pop(IUI ui)
        {
            int index = _popups.FindLastIndex(x => x == ui);

            if (index < 0)
                throw new Exception($"[{nameof(UIManager)}] : Failed to pop {ui}. Has not been pushed.");

            for (int ix = index; ix < _popups.Count - 1; ix++)
            {
                _popups[ix] = _popups[ix + 1];
                _popups[ix].sortingOrder = ix + 1;
            }

            _popups.RemoveAt(_popups.Count - 1);
        }
    }
}
