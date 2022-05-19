using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Hunter2D
{
    public class DamageBubble : MonoBehaviour
    {
        [Header("Settings")]
        public float popupY;

        [Header("Components")]
        public RectTransform rectTransform;
        public TMP_Text bubbleText;

        private Vector2 _targetPos;

#if UNITY_EDITOR
        private void OnValidate()
        {
            rectTransform = GetComponent<RectTransform>();
            bubbleText = GetComponent<TMP_Text>();
        }
#endif

        private void Start()
        {
            _targetPos = rectTransform.anchoredPosition + Vector2.up * popupY;
            rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + popupY, 1)
                         .OnComplete(() => Destroy(gameObject));
        }
    }
}
