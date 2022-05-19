using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hunter2D
{
    public class UpgradeOptions : MonoBehaviour
    {
        [Header("Status")]
        public ItemData currentSelect;

        [Header("Components")]
        public ConditionButton selectButton;

        #region Unity Message
        private void Start()
        {
            SkillOption.OnOptionSelected += SelectUpgradeOption;
            selectButton.onClick += ContinueGame;
        }

        private void OnDestroy()
        {
            SkillOption.OnOptionSelected -= SelectUpgradeOption;
            selectButton.onClick -= ContinueGame;
        }
        #endregion

        public void SelectUpgradeOption(ItemData data)
        {
            Debug.Log($"Select {data.itemName} item.");
            currentSelect = data;
            selectButton.SetActive(true);
        }

        public void ContinueGame()
        {
            Debug.Log($"Get {currentSelect.itemName}, continue game.");
            GameManager.Instance.ContinueGame();
            Destroy(gameObject);
        }
    }
}
