using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hunter2D
{
    [CreateAssetMenu(menuName = "Hunter2D/ItemData")]
    public class ItemData : ScriptableObject
    {
        public int itemId;
        public string itemName;
        public string itemDescription;
        public GameObject itemObject;
    }
}
