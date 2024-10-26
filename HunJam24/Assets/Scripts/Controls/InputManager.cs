using System.Linq;
using Logic;
using Logic.Tiles;
using Unity.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Camera cam;
    private void Awake() {
        cam = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context){
        if (!context.started) return;

        var ray = Physics2D.GetRayIntersectionAll(cam.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (ray.Length <= 0) return;
        var rayFirst = ray.OrderByDescending(x=> x.collider.GetComponent<SpriteRenderer>().sortingOrder).First();
        Debug.Log($"{rayFirst.collider.gameObject.name} hit");
        var tile = rayFirst.collider.GetComponent<TileBase>();
        if (!MapManager.Instance.Player.MoveOnto(tile)) {
            Debug.Log($"failed to move to {tile.name}");   
        }
        CloneManager.Instance.UpdateHistory(clone => clone.MoveOnto(tile));

    }
}
