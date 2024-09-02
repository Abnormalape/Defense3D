using System;
using System.Collections.Generic;
using BHSSolo.DungeonDefense.Singleton;
using UnityEngine;

namespace BHSSolo.DungeonDefense.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public UIManager()
        {
            _uis = new Dictionary<Type, IUI>();
            _popups = new List<IUI>(16);
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
            where T : UnityEngine.Object, IUI
        {
            // Find UI form dictionary.
            if (_uis.TryGetValue(typeof(T), out IUI ui))
                return (T)ui;

            // If ui not in dictionary, Find from Resources.
            T prefab = (T)Resources.Load($"UI/{nameof(T)}");

            // And also not in Resources, throw exception.
            if (prefab == null)
                throw new Exception($"[{nameof(UIManager)}] : Failed to get {typeof(T)}. Not exist");
            else
                return GameObject.Instantiate(prefab);
        }

        /// <summary>
        /// Set screen Ui and reorder.
        /// </summary>
        /// <param name="ui">Screen Ui to show as new</param>
        public void SetScreen<T>()
            where T : UnityEngine.Object, IUI
        {
            // If screen exsist, hide it.
            if (_screen != null)
            {
                _screen.Hide();
            }

            T ui = Get<T>(); // New screen.
            ui.sortingOrder = SCREEN_DEFAULT_SORTS_ORDER; // Order canvas.
            ui.Show(); // Show screen.
        }

        /// <summary>
        /// Add to Popup stack's last then reorder.
        /// </summary>
        /// <param name="ui">new popup to show</param>
        public void Push(IUI ui)
        {
            if (_popups.Count > 0)
                _popups[_popups.Count - 1].inputActionEnabled = false;

            ui.sortingOrder = _popups.Count;
            _popups.Add(ui);
            ui.inputActionEnabled = true;
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

            // if popup want to remove is recent one.
            if (index == _popups.Count - 1)
            {
                _popups[index].inputActionEnabled = false;

                //If there is any other popup except I want to remove.
                if(index >= 1)
                    _popups[index - 1].inputActionEnabled = true;
            }

            for (int ix = index; ix < _popups.Count - 1; ix++)
            {
                _popups[ix] = _popups[ix + 1];
                _popups[ix].sortingOrder = ix + 1;
            }

            _popups.RemoveAt(_popups.Count - 1);
        }
    }
}
