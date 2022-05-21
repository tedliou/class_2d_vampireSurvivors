using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hunter2D
{
    [CreateAssetMenu(menuName = "Hunter2D/ItemData")]
    public class ItemData : ScriptableObject
    {
        [Header("Settings")]
        public int itemId;
        public string itemName;
        public string itemDescription;
        public GameObject itemObject;
        public GameBuff buff;

        [Header("Limits")]
        public int holdLimitCount;
    }
}
