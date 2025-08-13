using System.Collections.Generic;
using Model.Level;
using UnityEngine;

namespace View.UI
{
    public class LevelSetSelectPanel : MenuPanel
    {
        [SerializeField] private GameObject LevelSetPrefab;
        [SerializeField] private Transform LevelSetContainer;
        List<LevelSetButton> levelSetButtons = new List<LevelSetButton>();
        protected override void Show()
        {
            base.Show();
            LevelManager.Instance.LevelSets.ForEach(levelSet =>
            {
                GameObject levelSetButton = Instantiate(LevelSetPrefab, LevelSetContainer);
                LevelSetButton button = levelSetButton.GetComponent<LevelSetButton>();
                if (button != null)
                {
                    button.Initialize(levelSet);
                    levelSetButtons.Add(button);
                }
                else
                {
                    Debug.LogError("LevelSetButton component not found on the instantiated prefab.");
                }
            });
        }

        protected override void Hide()
        {
            base.Hide();
            foreach (var button in levelSetButtons)
            {
                if (button != null)
                {
                    Destroy(button.gameObject);
                }
            }
            levelSetButtons.Clear();
        }
    }
}