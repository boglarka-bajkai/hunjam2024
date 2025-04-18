using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using Model.Tiles;
using Model.Tiles.Helpers;
using Model.Tiles.Data;
using Model.Level.Data;

[CustomPropertyDrawer(typeof(TilePlacement))]
public class TileEntryDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Dynamic height based on prefab, tileData, and coordinate
        var prefabProp = property.FindPropertyRelative("tilePrefab");
        var dataProp = property.FindPropertyRelative("tileData");

        float height = EditorGUI.GetPropertyHeight(prefabProp);
        height += EditorGUIUtility.singleLineHeight + 20; // coordinate
        height += EditorGUI.GetPropertyHeight(dataProp) + 5;

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var prefabProp = property.FindPropertyRelative("tilePrefab");
        var dataProp = property.FindPropertyRelative("tileData");
        var coordinateProp = property.FindPropertyRelative("coordinate");

        // Draw tilePrefab
        float yOffset = position.y;
        var prefabHeight = EditorGUI.GetPropertyHeight(prefabProp);
        var prefabRect = new Rect(position.x, yOffset, position.width, prefabHeight);
        EditorGUI.PropertyField(prefabRect, prefabProp);
        yOffset += prefabHeight + 5;

        // --- Draw Coordinate as Vector3Int ---
        object targetObject = property.serializedObject.targetObject;
        TilePlacement placement = GetTargetObjectOfProperty(property) as TilePlacement;

        if (placement != null && placement.Coordinate != null)
        {
            Vector3Int current = new Vector3Int(placement.Coordinate.X, placement.Coordinate.Y, placement.Coordinate.Z);
            var coordRect = new Rect(position.x, yOffset, position.width, EditorGUIUtility.singleLineHeight);
            Vector3Int edited = EditorGUI.Vector3IntField(coordRect, "Coordinate", current);
            yOffset += EditorGUIUtility.singleLineHeight + 20;

            // If changed, update the private readonly properties
            if (edited != current)
            {
                var coord = placement.Coordinate;
                SetReadonlyProperty(coord, "x", edited.x);
                SetReadonlyProperty(coord, "y", edited.y);
                SetReadonlyProperty(coord, "z", edited.z);
            }
        }

        // --- Draw TileData ---
        if (dataProp.managedReferenceValue != null)
        {
            var dataHeight = EditorGUI.GetPropertyHeight(dataProp);
            var dataRect = new Rect(position.x, yOffset, position.width, dataHeight);
            EditorGUI.PropertyField(dataRect, dataProp, new GUIContent("Tile Data"), true);
        }

        EditorGUI.EndProperty();
    }

    // Helper to get the actual object instance behind a SerializedProperty
    private static object GetTargetObjectOfProperty(SerializedProperty prop)
    {
        string path = prop.propertyPath.Replace(".Array.data[", "[");
        object obj = prop.serializedObject.targetObject;
        string[] elements = path.Split('.');
        foreach (var element in elements)
        {
            if (element.Contains("["))
            {
                string name = element.Substring(0, element.IndexOf("["));
                int index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue(obj, name, index);
            }
            else
            {
                obj = GetValue(obj, element);
            }
        }
        return obj;
    }

    private static object GetValue(object source, string name)
    {
        if (source == null)
            return null;

        var type = source.GetType();
        while (type != null)
        {
            var field = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
                return field.GetValue(source);
            var prop = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (prop != null)
                return prop.GetValue(source);
            type = type.BaseType;
        }
        return null;
    }

    private static object GetValue(object source, string name, int index)
    {
        var enumerable = GetValue(source, name) as System.Collections.IEnumerable;
        if (enumerable == null)
            return null;

        var enm = enumerable.GetEnumerator();
        for (int i = 0; i <= index; i++)
        {
            if (!enm.MoveNext()) return null;
        }
        return enm.Current;
    }

    // Helper to update readonly auto-properties using reflection
    private void SetReadonlyProperty(object target, string propertyName, int value)
    {
        var backingField = target.GetType().GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
        if (backingField != null)
        {
            backingField.SetValue(target, value);
        }
    }
}
