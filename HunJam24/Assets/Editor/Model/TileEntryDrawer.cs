using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;
using Model.Tiles;
using Model.Tiles.Data;
using Model.Level.Data;

[CustomPropertyDrawer(typeof(TilePlacement))]
public class TileEntryDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var prefabProp = property.FindPropertyRelative("tilePrefab");
        var dataProp = property.FindPropertyRelative("tileData");

        float height = EditorGUI.GetPropertyHeight(prefabProp) + 20;
        height += EditorGUIUtility.singleLineHeight + 20; // for coordinate
        if (dataProp != null && dataProp.managedReferenceValue != null)
            height += EditorGUI.GetPropertyHeight(dataProp, true) + 20;

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var prefabProp = property.FindPropertyRelative("tilePrefab");
        var dataProp = property.FindPropertyRelative("tileData");
        var coordinateProp = property.FindPropertyRelative("coordinate");

        float yOffset = position.y;

        // --- Draw tilePrefab ---
        var prefabHeight = EditorGUI.GetPropertyHeight(prefabProp);
        var prefabRect = new Rect(position.x, yOffset, position.width, prefabHeight);

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(prefabRect, prefabProp);
        if (EditorGUI.EndChangeCheck())
        {
            property.serializedObject.ApplyModifiedProperties(); // Needed to get updated object reference
            UpdateTileDataFromPrefab(property, prefabProp, dataProp);
        }

        yOffset += prefabHeight + 10;

        // --- Draw coordinate as Vector3Int ---
        var xProp = coordinateProp.FindPropertyRelative("x");
        var yProp = coordinateProp.FindPropertyRelative("y");
        var zProp = coordinateProp.FindPropertyRelative("z");

        if (xProp != null && yProp != null && zProp != null)
        {
            var coordRect = new Rect(position.x, yOffset, position.width, EditorGUIUtility.singleLineHeight);
            Vector3Int current = new Vector3Int(xProp.intValue, yProp.intValue, zProp.intValue);
            Vector3Int edited = EditorGUI.Vector3IntField(coordRect, "Coordinate", current);
            if (edited != current)
            {
                xProp.intValue = edited.x;
                yProp.intValue = edited.y;
                zProp.intValue = edited.z;
            }
            yOffset += EditorGUIUtility.singleLineHeight + 20;
        }

        // --- Draw tileData ---
        GameObject prefab = prefabProp.objectReferenceValue as GameObject;
        if (prefab != null)
        {
            Tile tile = prefab.GetComponent<Tile>();
            if (tile != null)
            {
                var tileType = tile.GetType();
                var attr = tileType.GetCustomAttribute<TileDataTypeAttribute>();
                if (attr != null && typeof(TileData).IsAssignableFrom(attr.DataType))
                {
                    // Tile uses a valid TileDataType — show the field
                    if (dataProp.managedReferenceValue == null || dataProp.managedReferenceValue.GetType() != attr.DataType)
                    {
                        var instance = Activator.CreateInstance(attr.DataType);
                        dataProp.managedReferenceValue = instance;
                        property.serializedObject.ApplyModifiedProperties();
                    }

                    var dataHeight = EditorGUI.GetPropertyHeight(dataProp, true);
                    var dataRect = new Rect(position.x, yOffset, position.width, dataHeight);
                    EditorGUI.PropertyField(dataRect, dataProp, new GUIContent("Tile Data"), true);
                    yOffset += dataHeight + 5;
                }
                else
                {
                    // No TileDataType — clear tileData
                    if (dataProp.managedReferenceValue != null)
                    {
                        dataProp.managedReferenceValue = null;
                        property.serializedObject.ApplyModifiedProperties();
                    }
                }
            }
        }

        EditorGUI.EndProperty();
    }

    /// <summary>
    /// Creates or updates the tileData based on the selected prefab type.
    /// </summary>
    private void UpdateTileDataFromPrefab(SerializedProperty parentProperty, SerializedProperty prefabProp, SerializedProperty dataProp)
    {
        GameObject prefab = prefabProp.objectReferenceValue as GameObject;
        if (prefab == null) return;

        var tile = prefab.GetComponent<Tile>();
        if (tile == null) return;

        // Get custom TileDataTypeAttribute
        var tileType = tile.GetType();
        var attr = tileType.GetCustomAttribute<TileDataTypeAttribute>();
        if (attr == null || !typeof(TileData).IsAssignableFrom(attr.DataType)) return;

        // If current data type doesn't match, create a new one
        if (dataProp.managedReferenceValue == null || dataProp.managedReferenceValue.GetType() != attr.DataType)
        {
            var instance = Activator.CreateInstance(attr.DataType);
            dataProp.managedReferenceValue = instance;
            parentProperty.serializedObject.ApplyModifiedProperties();
        }
    }


    // ----- Reflection helpers -----
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
}
