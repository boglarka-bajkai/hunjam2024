using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Logic;
using Logic.Tiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector = Logic.Vector;
public class TilePlacer : MonoBehaviour {
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] Transform placerTransform;
    private static TilePlacer _instance;
    public static TilePlacer Instance => _instance;
    [SerializeField] Material selectMaterial, baseMaterial;
    void Awake() {
        if (_instance != null) Destroy(this);
        _instance = this;
    }
    GameObject currentTilePrefab;
    Tile currentTile;
    Vector currentPosition = new Vector(0, 0, 0);
    List<TileConnection> connections = new();

    public void SetTile(Tile tile) {
        if (currentTile != null && currentTile.isHeightened && !tile.isHeightened) {
            tiles.FindAll(x=> x.Item1.Position == (currentPosition-new Vector(0,0,1)).AsVInt3).ForEach(x=> x.Item2.GetComponentInChildren<SpriteRenderer>().material = baseMaterial);
            Move(new Vector(0,0,-1));
        }
        else if ((currentTile == null || !currentTile.isHeightened) && tile.isHeightened) {
            tiles.FindAll(x=> x.Item1.Position == currentPosition.AsVInt3).ForEach(x=> x.Item2.GetComponentInChildren<SpriteRenderer>().material = selectMaterial);
            Move(new Vector(0,0,1));
        }
        currentTile = tile;
        currentTilePrefab = tile.Prefab;
        
    }

    public void DropdownChanged(int index) {
        string name = dropdown.options[index].text;
        Tile t = MapManager.Instance.getTileByName(name);
        SetTile(t);
    }
    
    public void StartEditing() {
        Tile baseTile = MapManager.Instance.getTileByName("Base");
        var tile = Instantiate(baseTile.Prefab, new Vector(0,0,0).UnityVector, UnityEngine.Quaternion.identity, placerTransform);
        tile.GetComponentInChildren<SpriteRenderer>().sortingOrder = new Vector(0,0,0).Order;
        tiles.Add(new Tuple<TileEntry, GameObject>(new TileEntry(new Vector(0,0,0).AsVInt3, "Base"), tile));
        Tile startTile = MapManager.Instance.getTileByName("Start");
        var tile2 = Instantiate(startTile.Prefab, new Vector(0,0,1).UnityVector, UnityEngine.Quaternion.identity, placerTransform);
        tile2.GetComponentInChildren<SpriteRenderer>().sortingOrder = new Vector(0,0,1).Order;
        tile2.GetComponentInChildren<SpriteRenderer>().enabled = true;
        tiles.Add(new Tuple<TileEntry, GameObject>(new TileEntry(new Vector(0,0,1).AsVInt3, "Start"), tile2));
        SetTile(baseTile);
        dropdown.ClearOptions();
        dropdown.AddOptions(MapManager.Tiles.FindAll(x=> x.Name != "Start").ConvertAll(x => x.Name));
    }

    List<Tuple<TileEntry, GameObject>> tiles = new();

    public void PlaceTile() {
        if (currentTilePrefab == null) return;
        if (MapLoader.Instance.state != MapState.InEditor) return;
        if (currentPosition == new Vector(0,0,0) || currentPosition == new Vector(0,0,1)) return;
        if (tiles.Exists(t => t.Item1.Position == currentPosition.AsVInt3 && t.Item1.TileName != "PressurePlate")) return;
        if (currentPosition.Z == 1 && !tiles.Exists(t => t.Item1.Position == (currentPosition-new Vector(0,0,1)).AsVInt3 && t.Item1.TileName == "Base")) return;
        if (currentPosition.Z == 0 && tiles.Exists(t => t.Item1.Position == currentPosition.AsVInt3 && t.Item1.TileName == "Base")) return;
        GameObject tile = Instantiate(currentTilePrefab, currentPosition.UnityVector, UnityEngine.Quaternion.identity, placerTransform);
        tile.GetComponentInChildren<SpriteRenderer>().sortingOrder = currentPosition.Order - (currentTile.Name == "PressurePlate" ? 1 : 0);
        tile.GetComponentInChildren<TileBase>().Position = currentPosition;
        TileEntry tileEntry = new TileEntry(currentPosition.AsVInt3, currentTile.Name);
        tiles.Add(new Tuple<TileEntry, GameObject>(tileEntry, tile));
        if (tile.GetComponent<ActivationListener>() != null) {
            StartConnect(tileEntry);
        }
    }
    TileEntry connectTile;
    void StartConnect(TileEntry tile) {
        connectTile = tile;
        MapLoader.Instance.state = MapState.EditorConnecting;
    }
    public void FinishConnect(TileBase tile) {
        if (tile == null) {
            Destroy(tiles.Find(t => t.Item1 == connectTile).Item2);
            tiles.Remove(tiles.Find(t => t.Item1 == connectTile));
        }
        else {
            var c = connections.FirstOrDefault(c=> c.PressurePlatePosition == tile.Position.AsVInt3);// ?? new TileConnection(tile.Position.AsVInt3);
            if (c == null) {
                c = new TileConnection(tile.Position.AsVInt3);
                connections.Add(c);
            }
            c.ConnectedTiles.Add(new Vector(connectTile.Position.x, connectTile.Position.y, connectTile.Position.z).AsVInt3);
        }
        MapLoader.Instance.state = MapState.InEditor;
    }
    public void RemoveTile() {
        if (MapLoader.Instance.state != MapState.InEditor) return;
        if (currentPosition.Equals(new Vector(0,0,0)) || currentPosition.Equals(new Vector(0,0,1))) return;
        List<Tuple<TileEntry, GameObject>> toRemove = tiles.FindAll(t => t.Item1.Position == currentPosition.AsVInt3 
        || t.Item1.Position == (currentPosition + new Vector(0,0,1)).AsVInt3 
        || t.Item1.Position == (currentPosition - new Vector(0,0,1)).AsVInt3);
        foreach (var tuple in toRemove) {
            Destroy(tuple.Item2);
            tiles.Remove(tuple);
        }
    }

    public void Move(Vector direction) {
        if (MapLoader.Instance.state != MapState.InEditor) return;
        if (currentTile.isHeightened) {
            tiles.FindAll(x=> x.Item1.Position == (currentPosition-new Vector(0,0,1)).AsVInt3).ForEach(x=> x.Item2.GetComponentInChildren<SpriteRenderer>().material = baseMaterial);
        }
        currentPosition += direction;
        transform.position = currentPosition.UnityVector;
        if (currentTile.isHeightened) {
            tiles.FindAll(x=> x.Item1.Position == (currentPosition-new Vector(0,0,1)).AsVInt3).ForEach(x=> x.Item2.GetComponentInChildren<SpriteRenderer>().material = selectMaterial);
        }
    }

    public void TestMap(){
        placerTransform.gameObject.SetActive(false);
        gameObject.SetActive(false);
        MapLoader.Instance.state = MapState.EditorTesting;
        MapLoader.Instance.StartTesting(tiles.ConvertAll(x=> x.Item1), connections);
        Debug.Log("connections: " + connections.Count);
        foreach (var item in connections)
        {
            Debug.Log(item.PressurePlatePosition);
            foreach (var item2 in item.ConnectedVectors)
            {
                Debug.Log(item2);
            }
        }
    }
    public void TestFailed() {
        placerTransform.gameObject.SetActive(true);
        gameObject.SetActive(true);
        OverlayManager.Instance.BackToEditor();
    }

    public void TestSuccessful() {
        foreach (var item in tiles) {
            Destroy(item.Item2);
        }
    }

    public void UploadMap() {
        Map map = new Map();
        map.Tiles = tiles.ConvertAll(x=> x.Item1);
        map.Connections = connections;
        Debug.Log("Map uploaded");
    }

}