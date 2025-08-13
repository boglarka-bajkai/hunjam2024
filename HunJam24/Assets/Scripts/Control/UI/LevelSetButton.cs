using Model;
using Model.Level;
using Model.Level.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    [RequireComponent(typeof(Button))]
    public class LevelSetButton : MonoBehaviour
    {
        public LevelSet LevelSet { get; private set; }
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI starCount;
        Button button;
        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }
        public void Initialize(LevelSet levelSet)
        {
            LevelSet = levelSet;
            nameText.text = levelSet.LevelSetName;
            starCount.text = $"{levelSet.TotalStars}/{levelSet.EarnableStars}";
        }

        void OnButtonClick()
        {
            if (LevelSet.IsUnlocked)
            {
                LevelManager.Instance.SelectLevelSet(LevelSet);
                GameManager.Instance.LevelSelect();
            }
            else
            {
                Debug.Log($"Level Set '{LevelSet.LevelSetName}' is locked. Stars required: {LevelSet.StarsRequired}");
            }
        }
    }
}