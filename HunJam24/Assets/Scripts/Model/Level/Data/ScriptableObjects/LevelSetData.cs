using UnityEngine;
using System.Linq;
using System.Collections.Generic;
namespace Model.Level.Data
{
    [CreateAssetMenu(fileName = "LevelSet", menuName = "LevelData/LevelSet", order = 1)]
    public class LevelSet : ScriptableObject
    {
        [Tooltip("The name of the level set, must be unique as the save data is indexed using this name.")]
        [SerializeField] string levelSetName; public string LevelSetName => levelSetName;
        [Tooltip("The levels in the level set.")]
        [SerializeField] List<LevelData> levels = new List<LevelData>(); public List<LevelData> Levels => levels;
        [Tooltip("The number of total stars required to unlock this level set")]
        [SerializeField] int starsRequired = 0; public int StarsRequired => starsRequired;
        public int TotalStars => levels.Sum(level => level.CollectedStars);
        public int EarnableStars => levels.Count * 3;
        public int StarsBefore => LevelManager.Instance.StarsBefore(this);
        public bool IsUnlocked => StarsBefore >= StarsRequired;

        public void ResetStars()
        {
            foreach (var level in levels)
            {
                level.ResetStars();
            }
            PlayerPrefs.Save();
        }
    }
}