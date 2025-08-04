using UnityEngine;
using UnityEditor;
using Model.Level.Data;
using Model.Level;

[CustomEditor(typeof(LevelData))]
/// <summary>
/// Custom editor for LevelData to add a button for previewing the level in the scene.
/// This allows quick testing of level designs without needing to enter Play mode.
/// </summary>
public class LevelPreview : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw default inspector
        DrawDefaultInspector();

        // Add custom button
        if (GUILayout.Button("Preview Level in Scene"))
        {
            LevelManager.Instance.SelectPreviewLevel((LevelData)target);
            LevelManager.Instance.LoadLevel();
        }
        if (GUILayout.Button("Unload Level"))
        {
            var previews = GameObject.FindGameObjectsWithTag("EditorPreview");
                foreach (var p in previews)
                    Object.DestroyImmediate(p);
        }
    }

}
