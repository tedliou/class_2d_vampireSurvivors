using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI.Extensions;
using UnityEngine.EventSystems;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hunter2D
{
    public enum SkillOptionStats
    {
        Normal = 0,
        Hover = 1,
        Active = 2,
        Disable = 3
    }

    public class SkillOption : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public static SkillOption Current { get; private set; }
        public static event UnityAction<ItemData> OnOptionSelected;

        [Header("Status")]
        public bool isActive;
        public SkillOptionStats skillOptionStats;
        public ItemData optionItem;

        [Header("Settings")]
        public Color normalOutlineColor;
        public Color hoverOutlineColor;
        public Color activeOutlineColor;
        public Color disableOutlineColor;
        public List<ItemData> itemDataPreload;

        [Header("Components")]
        public NicerOutline nicerOutline;
        public CanvasGroup canvasGroup;

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
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void SetDefaultValue()
        {
            isActive = false;
            SetOptionStats(SkillOptionStats.Normal);
            itemDataPreload = AssetDatabase.FindAssets($"t:{nameof(ItemData)}").Select(x => AssetDatabase.LoadAssetAtPath<ItemData>(AssetDatabase.GUIDToAssetPath(x))).ToList();
        }
#endif

        private void Start()
        {
            Current = null;
            optionItem = itemDataPreload[Random.Range(0, itemDataPreload.Count)];
            Instantiate(optionItem.itemObject, transform).transform.localPosition = Vector3.zero;
            if (GamePlayer.instance.GetItemCount(optionItem) >= optionItem.holdLimitCount)
            {
                SetOptionStats(SkillOptionStats.Disable);
            }
        }

        private void OnDestroy()
        {
            Current = null;
        }
        #endregion

        #region Interfaces
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (skillOptionStats == SkillOptionStats.Disable) return;
            if (!isActive) SetOptionStats(SkillOptionStats.Hover);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (skillOptionStats == SkillOptionStats.Disable) return;
            if (!isActive) SetOptionStats(SkillOptionStats.Normal);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (skillOptionStats == SkillOptionStats.Disable) return;
            SetOptionStats(SkillOptionStats.Active);
            SetActive(true);
        }
        #endregion

        public void SetOptionStats(SkillOptionStats stats)
        {
            skillOptionStats = stats;
            switch (stats)
            {
                case SkillOptionStats.Normal:
                    nicerOutline.effectColor = normalOutlineColor;
                    canvasGroup.alpha = 1;
                    break;
                case SkillOptionStats.Hover:
                    nicerOutline.effectColor = hoverOutlineColor;
                    canvasGroup.alpha = 1;
                    break;
                case SkillOptionStats.Active:
                    nicerOutline.effectColor = activeOutlineColor;
                    canvasGroup.alpha = 1;
                    break;
                case SkillOptionStats.Disable:
                    nicerOutline.effectColor = disableOutlineColor;
                    canvasGroup.alpha = .6f;
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
