using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tactile.TactileMatch3Challenge.ViewComponents
{
    public class GameOver : MonoBehaviour, IGameOver
    {
        private const string LOST_TEXT = "You Lost!";
        private const string WIN_TEXT = "You Win!";

        [SerializeField] private Text gameOverText;
        [SerializeField] private Button restartButton;

        public event Action RestartAction;

        void Awake()
        {
            gameObject.SetActive(false);
            restartButton.onClick.AddListener(() => RestartAction?.Invoke());
        }

        void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
        }

        public void Disable()
        {
            gameOverText.text = "";
            gameObject.SetActive(false);
        }

        public void SetWinState(bool isAchieved)
        {
            gameObject.SetActive(true);

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