using Model;
using Model.Level;
using Model.Level.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    [RequireComponent(typeof(Button))]
    public class LevelButton : MonoBehaviour
    {
        public LevelData Level { get; private set; }
        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI starCount;
        Button button;
        void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }
        public void Initialize(LevelData level)
        {
            Level = level;
            nameText.text = level.LevelName;
            starCount.text = $"{level.CollectedStars}/3";
        }

        void OnButtonClick()
        {
            if (!Level.IsUnlocked)
            {
                Debug.Log($"Level '{Level.LevelName}' is locked. Complete previous levels");
                return;
            }
            LevelManager.Instance.SelectLevel(Level);
            GameManager.Instance.StartGame();
        }
    }
}