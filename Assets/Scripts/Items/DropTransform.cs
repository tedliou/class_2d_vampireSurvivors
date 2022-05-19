using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hunter2D
{
    public class DropTransform : MonoBehaviour
    {
        [Header("Status")]
        public ItemData itemData;

        [Header("Settings")]
        public DropItem dropItem;

#if UNITY_EDITOR
        private void OnValidate()
        {
                
        }
#endif
    }
}
