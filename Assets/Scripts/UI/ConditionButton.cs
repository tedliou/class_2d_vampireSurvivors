using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Hunter2D
{
    public class ConditionButton : MonoBehaviour
    {
        [Header("Status")]
        public bool isActive;

        [Header("Settings")]
        [Range(0, 1)] public float inactiveAlpha; 

        [Header("Components")]
        public Image image;
        public Button button;
        public CanvasGroup canvasGroup;

        public event UnityAction onClick;

        #region Unity Message
#if UNITY_EDITOR
        private void OnValidate()
        {
            GetRequireComponent();
            SetDefaultValue();
        }

        private void GetRequireComponent()
        {
            image = GetComponent<Image>();
            button = GetComponent<Button>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void SetDefaultValue()
        {
            button.interactable = true;
        }
#endif

        private void Start()
        {
            SetActive(isActive);
            button.onClick.AddListener(() => onClick.Invoke());
        }
        #endregion

        public void SetActive(bool active)
        {
            isActive = active;
            button.interactable = active;
            canvasGroup.alpha = active ? 1 : inactiveAlpha;
        }
    }
}
