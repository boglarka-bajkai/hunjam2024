using System.Collections;
using Model;
using Model.Characters;
using Model.Data;
using Model.Level;
using Model.Tiles;
using UnityEngine;

namespace View.Animated
{
    public class CharacterAnimation : MonoBehaviour
    {

        [SerializeField] string prefix = "alterego_";
        [SerializeField] float WAITBEFORESTART = .1f;
        [SerializeField] float MOVE_MULTIPLIER = 1.1f;
        Animator animator;
        void Awake()
        {
            animator = GetComponent<Animator>();
            GetComponent<Character>().OnMove += Move;   
        }

        void Move(Coordinate from, Coordinate to, bool skipAnimation)
        {
            if (skipAnimation)
            {
                transform.position = to.AsUnityVector;
                GetComponent<SpriteRenderer>().sortingOrder = to.RenderOrder + 1;
                return;
            }
            Coordinate direction = to - from;
            if (direction == new Coordinate(1, 0, 0))
            {
                //Debug.Log("R");
                animator.SetInteger("dir", 0);
            }
            if (direction == new Coordinate(0, -1, 0))
            {
                //Debug.Log("U");
                animator.SetInteger("dir", 1);
            }
            if (direction == new Coordinate(-1, 0, 0))
            {
                //Debug.Log("L");
                animator.SetInteger("dir", 2);
            }
            if (direction == new Coordinate(0, 1, 0))
            {
                //Debug.Log("D");
                animator.SetInteger("dir", 3);
            }
            StartCoroutine(moveSoftlyTo(from, to, !LevelManager.Instance.GetTilesAt(to).TrueForAll(x=> x is not Box)));
        }

        protected virtual IEnumerator moveSoftlyTo(Coordinate from, Coordinate to, bool pushing)
        {
            float t = 0f;
            //Wait for jump anim
            yield return new WaitForEndOfFrame();
            Animator animator = GetComponent<Animator>();
            string suffix = "";

            switch (animator.GetInteger("dir"))
            {
                case 0:
                    suffix = pushing ? "push_UR" : "jump_UR";
                    break;
                case 1:
                    suffix = pushing ? "push_UL" : "jump_UL";
                    break;
                case 2:
                    suffix = pushing ? "push_DL" : "jump_DL";
                    break;
                case 3:
                    suffix = pushing ? "push_DR" : "jump_DR";
                    break;
                default: break;
            }
            string animName = prefix + suffix;
            if (animName != "")
            {
                animator.Play(animName);
            }

            
            while (t <= WAITBEFORESTART)
            {
                t += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            //Move smoothly
            t = 0f;
            // Vector3 startPos = transform.position;
            if (to.RenderOrder > from.RenderOrder)
                GetComponent<SpriteRenderer>().sortingOrder = to.RenderOrder+1;
            while (t <= 1f)
            {
                t += Time.deltaTime * MOVE_MULTIPLIER;
                transform.position = Vector3.Lerp(from.AsUnityVector, to.AsUnityVector, t);
                yield return new WaitForEndOfFrame();
            }
            transform.position = to.AsUnityVector;
            GetComponent<SpriteRenderer>().sortingOrder = to.RenderOrder+1;
        }
    }
}