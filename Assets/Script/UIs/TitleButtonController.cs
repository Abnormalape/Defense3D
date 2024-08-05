using UnityEngine;
using UnityEditor.EventSystems;
using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEditor.Experimental.GraphView;

namespace BHSSolo.DungeonDefense.UI
{
    public class TitleButtonController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Color32 _idleColor;
        [SerializeField] private Color32 _hoverColor;
        [SerializeField] private Color32 _clickColor;
        [SerializeField] private Color32 _selectedColor;
        [SerializeField] private Color32 _unclickableColor;
        [SerializeField] public UnityEvent OnClick;
        [SerializeField] private float _fadeTime;

        private Image _buttonImage { get => GetComponent<Image>(); }

        private bool _clickable = true;
        private bool _selectable = false;
        private bool _selected = false;
        private bool _mouseHovered = false;

        public bool Clickable
        {
            get => _clickable;
            set
            {
                _clickable = value;
                if (_clickable)
                    StartCoroutine(ChangeButtonColorOnTime(_idleColor));
                else
                    StartCoroutine(ChangeButtonColorOnTime(_unclickableColor));
            }
        }

        public bool Selectable
        {
            get => _selectable;
            set => _selectable = value;
        }

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                if (_selected)
                    StartCoroutine(ChangeButtonColorOnTime(_selectedColor));
                else
                    StartCoroutine(ChangeButtonColorOnTime(_idleColor));
            }
        }
        public bool MouseMovered
        {
            get => _mouseHovered;
            set
            {
                _mouseHovered = value;
            }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (_selectable)
            { StartCoroutine(ChangeButtonColorOnTime(_selectedColor)); }

            OnClick.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            StartCoroutine(ChangeButtonColorOnTime(_clickColor));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StartCoroutine(ChangeButtonColorOnTime(_idleColor));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            StartCoroutine(ChangeButtonColorOnTime(_hoverColor));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StartCoroutine(ChangeButtonColorOnTime(_idleColor));
        }

        IEnumerator ChangeButtonColorOnTime(Color32 targetColor)
        {
            float deltaChangingRed = (targetColor.r - _buttonImage.color.r) / _fadeTime;
            float deltaChanginggreen = (targetColor.g - _buttonImage.color.g) / _fadeTime;
            float deltaChangingblue = (targetColor.b - _buttonImage.color.b) / _fadeTime;
            float deltaChangingalpha = (targetColor.a - _buttonImage.color.a) / _fadeTime;
            float passedTime = 0;

            while (passedTime < _fadeTime)
            {
                _buttonImage.color = new Color(
                    _buttonImage.color.r + deltaChangingRed,
                    _buttonImage.color.g + deltaChanginggreen,
                    _buttonImage.color.b + deltaChangingblue,
                    _buttonImage.color.a + deltaChangingalpha
                    );
                passedTime += Time.deltaTime;
                yield return null;
            }
        }
    }
}