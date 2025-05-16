using System;
using Model.Tiles.Data;
using UnityEngine;

namespace Model.Tiles.Helpers
{
    class TileConnectionHelper
    {
        #region Singleton Management
        /// <summary>
        /// Singleton instance of the ConnectionManager.
        /// </summary>
        static TileConnectionHelper _instance;
        /// <summary>
        /// Gets the singleton instance of the ConnectionManager.
        /// </summary>
        /// <returns>The singleton instance of the ConnectionManager.</returns>
        public static TileConnectionHelper Instance {
            get {
                if (_instance == null) {
                    _instance = new TileConnectionHelper();
                }
                return _instance;

            }
        }
        #endregion

        #region Activation Events
        /// <summary>
        /// Event that is triggered when an activator tile is activated.
        /// </summary>
        public event Action<TileConnectionGroup> OnActivatorActivated;
        /// <summary>
        /// Event that is triggered when an activator tile is deactivated.
        /// </summary>
        public event Action<TileConnectionGroup> OnActivatorDeactivated;
        /// <summary>
        /// Function to call when an activator tile is activated.
        /// </summary>
        /// <param name="tileGroup">The tile group that was activated.</param>
        public void ActivatorActivated(TileConnectionGroup tileGroup) {
            OnActivatorActivated?.Invoke(tileGroup);
        }
        /// <summary>
        /// Function to call when an activator tile is deactivated.
        /// </summary>
        /// <param name="tileGroup">The tile group that was deactivated.</param>
        public void ActivatorDeactivated(TileConnectionGroup tileGroup) {
            OnActivatorDeactivated?.Invoke(tileGroup);
        }
        #endregion
    }
}