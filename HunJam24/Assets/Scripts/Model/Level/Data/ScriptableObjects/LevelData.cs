using System.Collections.Generic;
using Model.Level.Data;
using UnityEngine;
namespace Model.Level.Data
{
    [CreateAssetMenu(fileName = "Level", menuName = "LevelData/Level", order = 0)]
    public class LevelData : ScriptableObject
    {
        [Header("Level Data")]
        [Tooltip("The name of the level, must be unique as the save data is indexed using this name.")]
        [SerializeField] string levelName; public string LevelName => levelName;
        [Tooltip("The tile-coordinate-data triples of the tiles present in the level.")]
        [SerializeField] public List<TilePlacement> tilePlacements = new List<TilePlacement>(); public List<TilePlacement> TilePlacements => tilePlacements;
        [Header("Score Data")]
        [Tooltip("The maximum number of moves to reach 3 stars (should be equal to the best solution).")]
        [SerializeField] public int threeStarThreshold = 0; public int ThreeStarThreshold => threeStarThreshold;
        [Tooltip("The maximum number of moves to reach 2 stars.")]
        [SerializeField] public int twoStarThreshold = 0; public int TwoStarThreshold => twoStarThreshold;

        public int CollectedStars => PlayerPrefs.GetInt($"Level_{levelName}_Stars", 0);

        public void SetCompletionScore(int steps)
        {
            int oldStars = CollectedStars;
            int stars;
            if (steps <= threeStarThreshold) stars = 3;
            else if (steps <= twoStarThreshold) stars = 2;
            else stars = 1;
            if (stars > oldStars)
            {
                PlayerPrefs.SetInt($"Level_{levelName}_Stars", stars);
                PlayerPrefs.Save();
            }
        }

        public void ResetStars()
        {
            PlayerPrefs.SetInt($"Level_{levelName}_Stars", 0);
            PlayerPrefs.Save();
        }

        public LevelData PreviousLevel => LevelManager.Instance.PreviousLevel(this);
        public LevelData NextLevel => LevelManager.Instance.NextLevel(this);
        public bool IsUnlocked => PreviousLevel == null || PreviousLevel.CollectedStars > 0;
        public bool HasNextLevel => NextLevel != null;
    }
}