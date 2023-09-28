using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tactile.TactileMatch3Challenge.ViewComponents
{
    public class LevelInfo : MonoBehaviour
    {
        private const string LOST_TEXT = "You Lost!";
        private const string WIN_TEXT = "You Win!";

        [SerializeField] private Text levelInfoText;
        [SerializeField] private Text gameOverText;
        [SerializeField] private Button restartButton;

        public Action RestartAction;

        void Awake()
        {
            restartButton.onClick.AddListener(() => RestartAction?.Invoke());
        }

        void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
        }

        public void ResetView(string levelInfo)
        {
            gameOverText.text = "";
            gameOverText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            SetLevelInfo(levelInfo);
        }

        public void SetLevelInfo(string info)
        {
            levelInfoText.text = info;
        }

        public void SetWinState(bool isAchieved)
        {
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);

            if (isAchieved)
            {
                gameOverText.text = WIN_TEXT;
            }
            else
            {
                gameOverText.text = LOST_TEXT;
            }
        }
    }
}