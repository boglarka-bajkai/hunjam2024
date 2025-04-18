using UnityEngine;

namespace Model.Level.Data
{
    [CreateAssetMenu(fileName = "LevelSet", menuName = "LevelData/LevelSet", order = 1)]
    public class LevelSet : ScriptableObject 
    {
        [Tooltip("The name of the level set, must be unique as the save data is indexed using this name.")]
        [SerializeField] string levelSetName; public string LevelSetName => levelSetName;
        [Tooltip("The levels in the level set.")]
        [SerializeField] LevelData[] levels = new LevelData[9]; public LevelData[] Levels => levels;
        [Tooltip("The number of total stars required to unlock this level set")]
        [SerializeField] int starsRequired = 0; public int StarsRequired => starsRequired;
    }
}