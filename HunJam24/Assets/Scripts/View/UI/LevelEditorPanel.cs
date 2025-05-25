using System;
using System.Collections.Generic;
using Model.Level;
using Model.LevelEditor;
using TMPro;
using UnityEngine;

namespace View.UI
{
    public class LevelEditorPanel : MenuPanel
    {
        [SerializeField] private TMP_Dropdown TileDropdown;

        protected override void Show()
        {
            base.Show();
            TileDropdown.ClearOptions();
            List<string> tileOptions = new List<string>();
            foreach (var tile in LevelEditor.Instance.AllowedTiles)
            {
                tileOptions.Add(tile.TileName);
            }
            TileDropdown.AddOptions(tileOptions);
            TileDropdown.onValueChanged.AddListener(OnTileSelected);
            TileDropdown.value = 0; // Set default to the first tile
            TileDropdown.RefreshShownValue();
            //Add Listener for dropdown value change
        }

        private void OnTileSelected(int value)
        {
            if (value < 0 || value >= LevelEditor.Instance.AllowedTiles.Count)
            {
                Debug.LogError("Selected tile index is out of range.");
                return;
            }
            var selectedTile = LevelEditor.Instance.AllowedTiles[value];
            LevelEditor.Instance.Select(selectedTile.TileName);
            Debug.Log($"Selected tile: {selectedTile.TileName}");
        }

        protected override void Hide()
        {
            base.Hide();
            // Clear the dropdown options and remove the listener
            TileDropdown.ClearOptions();
            TileDropdown.onValueChanged.RemoveAllListeners();
            // Optionally reset the dropdown to a default state
            TileDropdown.value = 0; // Reset to the first tile or a default state
            TileDropdown.RefreshShownValue();
        }
    }
}