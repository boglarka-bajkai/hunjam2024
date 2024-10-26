using System.Linq;
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
    }
}
