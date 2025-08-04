using Model.Level;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
/// <summary>
/// Automatically cleans up editor preview objects when entering Play mode.
/// This prevents clutter in the scene during development.
/// </summary>
public static class PreviewCleanupEditor
{
    // Static ctor fires when the Editor loads/reloads scripts
    static PreviewCleanupEditor()
    {
        EditorApplication.playModeStateChanged += state =>
        {
            // When we’re about to enter Play mode from Edit mode...
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                var previews = GameObject.FindGameObjectsWithTag("EditorPreview");
                foreach (var p in previews)
                    Object.DestroyImmediate(p);
            }
        };
    }
}