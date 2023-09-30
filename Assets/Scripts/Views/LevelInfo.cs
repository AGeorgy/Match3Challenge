using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tactile.TactileMatch3Challenge.ViewComponents
{
    public class LevelInfo : MonoBehaviour, ILevelInfo
    {
        [SerializeField] private Text levelInfoText;

        public Action RestartAction;

        public void SetLevelInfo(string info)
        {
            levelInfoText.text = info;
        }
    }
}