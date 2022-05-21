using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Hunter2D
{
    public class UpgradeOptions : MonoBehaviour
    {
        [Header("Status")]
        public ItemData currentSelect;

        [Header("Components")]
        public ConditionButton selectButton;
        public ConditionButton ignoreButton;

        public static int IgnoreCount;

        #region Unity Message
        private void Start()
        {
            SkillOption.OnOptionSelected += SelectUpgradeOption;
            selectButton.onClick += GetItemAndContinueGame;
            ignoreButton.onClick += ContinueGame;
        }

        private void OnDestroy()
        {
            SkillOption.OnOptionSelected -= SelectUpgradeOption;
            selectButton.onClick -= GetItemAndContinueGame;
            ignoreButton.onClick -= ContinueGame;
        }
        #endregion

        public void SelectUpgradeOption(ItemData data)
        {
            Debug.Log($"Select {data.itemName} item.");
            currentSelect = data;
            selectButton.SetActive(true);
        }

        public void GetItemAndContinueGame()
        {
            Debug.Log($"Get {currentSelect.itemName}, continue game.");
            GamePlayer.instance.AddItem(currentSelect);
            GameManager.Instance.ContinueGame();
            Destroy(gameObject);
        }

        public void ContinueGame()
        {
            IgnoreCount += 1;
            GameManager.Instance.ContinueGame();
            Destroy(gameObject);
        }
    }
}
