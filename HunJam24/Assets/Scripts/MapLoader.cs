using System.Collections.Generic;
using Logic;
using UnityEngine;
using Logic.Tiles;
using System;
using Logic.Characters;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor.Profiling.Memory.Experimental;

public enum MapState {
    Playing,
    InEditor,
    EditorConnecting,
    EditorTesting,
    InMenu
}
public class MapLoader : MonoBehaviour
{
    [SerializeField] TilePlacer builder;
    private static MapLoader _instance;
    public MapState state = MapState.InMenu;
    void Awake() {
        if (_instance != null) Destroy(this);
        _instance = this;
        builder.gameObject.SetActive(false);
    }
    public static MapLoader Instance => _instance;
    
    int currentMap = 0;
    [SerializeField] List<Map> maps;
    public void StartGame() {
        state = MapState.Playing;
        InvertColor.Instance.ToggleColorInversion();
        TryLoadNextMap();
    }
    //First coords is what to connect (spike, inverse spike)
    //Second coords is where to connect (pressure plate)
    List<Tuple<Vector, Vector>> ConnsTest = new(){
        new(new Vector(2, 0, 1), new Vector(0,2,1)),
    };
    List<List<Tuple<Vector, Vector>>> Conns = new();
    public void TryLoadNextMap() {
        InvertColor.Instance.ResetColor();
        if (state == MapState.Playing)
        {
            if (currentMap < maps.Count){
                MapManager.Instance.SetMap(maps[currentMap]);
                
                currentMap++;
            }
            else {
                OverlayManager.Instance.ShowWinScreen();
            }
        }
        else if (state == MapState.EditorTesting) {
            Debug.Log("Successful test");
            TilePlacer.Instance.TestSuccessful();
            state = MapState.InEditor;
        }
    }

    public void RestartMap() {
        InvertColor.Instance.ResetColor();
        if (state == MapState.Playing) {
            MapManager.Instance.SetMap(maps[currentMap-1]);
        }
        else if (state == MapState.EditorTesting) {
            MapManager.Instance.DestroyMap();
            state = MapState.InEditor;
            TilePlacer.Instance.TestFailed();
        }
    }

    public void StartMapEditor()
    {
        builder.gameObject.SetActive(true);
        state = MapState.InEditor;
        builder.StartEditing();
    }

    public void StartTesting(List<TileEntry> tiles, List<TileConnection> connections) {
        state = MapState.EditorTesting;
        Map map = ScriptableObject.CreateInstance<Map>();
        map.Tiles = tiles;
        map.Connections = connections;
        MapManager.Instance.SetMap(map);
    }
}
