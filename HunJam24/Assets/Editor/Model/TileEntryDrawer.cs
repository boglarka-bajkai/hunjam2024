using UnityEditor;
using UnityEngine;
using System;
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
        height += EditorGUIUtility.singleLineHeight + 20; // Coordinate
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
            property.serializedObject.ApplyModifiedProperties();
            AssignNewTileDataInstance(property, prefabProp, dataProp);
        }

        yOffset += prefabHeight + 10;

        // --- Draw Coordinate ---
        var xProp = coordinateProp.FindPropertyRelative("x");
        var yPropCoord = coordinateProp.FindPropertyRelative("y");
        var zProp = coordinateProp.FindPropertyRelative("z");

        if (xProp != null && yPropCoord != null && zProp != null)
        {
            var coordRect = new Rect(position.x, yOffset, position.width, EditorGUIUtility.singleLineHeight);
            Vector3Int current = new Vector3Int(xProp.intValue, yPropCoord.intValue, zProp.intValue);
            Vector3Int edited = EditorGUI.Vector3IntField(coordRect, "Coordinate", current);
            if (edited != current)
            {
                xProp.intValue = edited.x;
                yPropCoord.intValue = edited.y;
                zProp.intValue = edited.z;
            }
            yOffset += EditorGUIUtility.singleLineHeight + 20;
        }

        // --- Draw tileData if applicable ---
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
    /// Creates a new instance of TileData when the prefab changes.
    /// </summary>
    private void AssignNewTileDataInstance(SerializedProperty parentProperty, SerializedProperty prefabProp, SerializedProperty dataProp)
    {
        GameObject prefab = prefabProp.objectReferenceValue as GameObject;
        if (prefab == null) return;

        var tile = prefab.GetComponent<Tile>();
        if (tile == null) return;

        var tileType = tile.GetType();
        var attr = tileType.GetCustomAttribute<TileDataTypeAttribute>();
        if (attr == null || !typeof(TileData).IsAssignableFrom(attr.DataType)) return;

        var instance = Activator.CreateInstance(attr.DataType);
        dataProp.managedReferenceValue = instance;
        parentProperty.serializedObject.ApplyModifiedProperties();
    }
}
