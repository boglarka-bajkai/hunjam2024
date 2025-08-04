using Model.Level;
using UnityEditor;
using UnityEngine;
public class LevelManagerDebug : EditorWindow
{
    [MenuItem("Tools/PlayerPrefs Editor")]
    public static void ShowWindow()
    {
        GetWindow<LevelManagerDebug>("Level Manager Debug");
    }

    private void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Controls", EditorStyles.boldLabel);

        if (GUILayout.Button("Unlock All Levels"))
        {
            foreach (var levelSet in LevelManager.Instance.LevelSets)
            {
                levelSet.ResetStars();
                foreach (var level in levelSet.Levels)
                {
                    level.SetCompletionScore(0); // Reset to 0 moves to unlock all levels
                }
            }
        }

        if (GUILayout.Button("Clear Level Progress"))
        {
            foreach (var levelSet in LevelManager.Instance.LevelSets)
            {
                levelSet.ResetStars();
                foreach (var level in levelSet.Levels)
                {
                    level.ResetStars(); // Reset stars for each level
                }
            }
            Debug.Log("All levels progress cleared.");
        }
    }
}

