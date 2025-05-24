using System.Collections.Generic;
using UnityEngine;

namespace View.UI.Helpers
{
    /// <summary>
    /// A helper class that enables all menu panels in the scene at startup.
    /// The panels will auto-hide when the game state changes to InGame.
    /// This is required to ensure that all menu panels are able to subscribe to the GameStateChanged event
    /// without having all of them enabled in editor to clutter the screen.
    /// </summary>
    public class MenuHelper : MonoBehaviour
    {
        [SerializeField] List<GameObject> menuPanels;
        void Awake()
        {
            foreach (var panel in menuPanels)
            {
                panel.SetActive(true);
            }
        }
    }
}