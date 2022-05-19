using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hunter2D
{
    public class DropItem : MonoBehaviour
    {
        [Header("Settings")]
        public Transform dropTransform;

        [Header("Components")]
        public RectTransform rectTransform;

#if UNITY_EDITOR
        private void OnValidate()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void OnDrawGizmos()
        {
            UpdateDropPosition();
        }
#endif

        private void Start()
        {
            
        }

        private void Update()
        {
            UpdateDropPosition();
        }

        private void UpdateDropPosition()
        {
            var pos = Camera.main.WorldToScreenPoint(dropTransform.position);
            pos.z = 0;
            rectTransform.anchoredPosition = pos;
        }
    }
}
