using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems;

namespace Hunter2D
{
    public enum SkillOptionStats
    {
        Normal = 0,
        Hover = 1,
        Active = 2
    }

    public class SkillOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public static SkillOption Current { get; private set; }
        public static event UnityAction<ItemData> OnOptionSelected;

        [Header("Status")]
        public bool isActive;
        public ItemData optionItem;

        [Header("Settings")]
        public Color normalOutlineColor;
        public Color hoverOutlineColor;
        public Color activeOutlineColor;

        [Header("Components")]
        public NicerOutline nicerOutline;

        #region Unity Messages
#if UNITY_EDITOR
        private void OnValidate()
        {
            GetRequireComponent();
            SetDefaultValue();
        }

        private void GetRequireComponent()
        {
            nicerOutline = GetComponent<NicerOutline>();
        }

        private void SetDefaultValue()
        {
            isActive = false;
            SetOptionStats(SkillOptionStats.Normal);
        }
#endif

        private void Start()
        {
            Current = null;
            Instantiate(optionItem.itemObject, transform).transform.localPosition = Vector3.zero;
        }

        private void OnDestroy()
        {
            Current = null;
        }
        #endregion

        #region Interfaces
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isActive) SetOptionStats(SkillOptionStats.Hover);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isActive) SetOptionStats(SkillOptionStats.Normal);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SetOptionStats(SkillOptionStats.Active);
            SetActive(true);
        }
        #endregion

        public void SetOptionStats(SkillOptionStats stats)
        {
            switch (stats)
            {
                case SkillOptionStats.Normal:
                    nicerOutline.effectColor = normalOutlineColor;
                    break;
                case SkillOptionStats.Hover:
                    nicerOutline.effectColor = hoverOutlineColor;
                    break;
                case SkillOptionStats.Active:
                    nicerOutline.effectColor = activeOutlineColor;
                    break;
                default:
                    break;
            }
        }

        public void SetActive(bool active)
        {
            if (active)
            {
                if (Current) Current.SetActive(!active);
                Current = this;
                OnOptionSelected.Invoke(optionItem);
            }
            else
            {
                SetOptionStats(SkillOptionStats.Normal);
            }
            isActive = active;
        }
    }
}
