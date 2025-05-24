using System.Collections.Generic;
using Model.Level;
using UnityEngine;

namespace View.UI
{
    public class LevelSelectPanel : MenuPanel
    {
        [SerializeField] private GameObject LevelSetPrefab;
        [SerializeField] private Transform LevelSetContainer;
        List<LevelButton> levelSetButtons = new List<LevelButton>();
        protected override void Show()
        {
            base.Show();
            LevelManager.Instance.CurrentLevelSet.Levels.ForEach(level =>
            {
                GameObject levelSetButton = Instantiate(LevelSetPrefab, LevelSetContainer);
                LevelButton button = levelSetButton.GetComponent<LevelButton>();
                if (button != null)
                {
                    button.Initialize(level);
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