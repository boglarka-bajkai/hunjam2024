using System.Collections;
using Model.Data;
using Model.Tiles;
using UnityEngine;

namespace View.Animated
{
    public class MovableTileAnimation : MonoBehaviour
    {
        [SerializeField] float WAITBEFORESTART = .1f;
        [SerializeField] float MOVE_MULTIPLIER = 1.1f;
        Animator animator;
        void Awake()
        {
            animator = GetComponent<Animator>();
            GetComponent<MovableTile>().OnMove += Move;   
        }

        void Move(Coordinate from, Coordinate to, bool skipAnimation)
        {
            if (skipAnimation)
            {
                transform.position = to.AsUnityVector;
                GetComponent<SpriteRenderer>().sortingOrder = to.RenderOrder;
                return;
            }
            StartCoroutine(moveSoftlyTo(from, to));
        }
         IEnumerator moveSoftlyTo(Coordinate from, Coordinate to) {
            float t = 0f;
            //Wait for jump anim
            yield return new WaitForEndOfFrame();
            
            while (t <= WAITBEFORESTART) {
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            //Move smoothly
            t = 0f;
            // Vector3 startPos = transform.position;
            if (to.RenderOrder > from.RenderOrder) 
                GetComponent<SpriteRenderer>().sortingOrder = to.RenderOrder;
            while (t <= 1f) {
                t += Time.deltaTime * MOVE_MULTIPLIER;
                transform.position = Vector3.Lerp(from.AsUnityVector, to.AsUnityVector, t);
                yield return new WaitForEndOfFrame();
            }
            GetComponent<SpriteRenderer>().sortingOrder = to.RenderOrder;
            
        }
    }
}