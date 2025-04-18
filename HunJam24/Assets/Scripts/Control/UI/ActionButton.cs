using UnityEngine;
using UnityEngine.Events;

namespace View
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    [AddComponentMenu("UI/Action Button")]
    public class ActionButton : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The UnityEvent to be called when the button is clicked.")]  
        UnityEvent unityEventAction;

        [SerializeField]
        [Tooltip("The common action to be called when the button is clicked.")]
        CommonButtonAction scriptableObjectAction;

        [SerializeField]
        [Tooltip("Whether to use common actions instead of UnityEvent.")]
        bool useScriptableObject = false;

        private void Start()
        {
            if (useScriptableObject && scriptableObjectAction != null)
            {
                // If using ScriptableObject, use its Execute method
                GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => scriptableObjectAction.Execute());
            }
            else
            {
                // Otherwise, use the UnityEvent
                GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => unityEventAction.Invoke());
            }
        }

        private void OnDestroy() {
            // Unsubscribe from the button click event to prevent memory leaks
            if (useScriptableObject && scriptableObjectAction != null)
            {
                GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(() => scriptableObjectAction.Execute());
            }
            else
            {
                GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener(() => unityEventAction.Invoke());
            }
        }
    }
}
