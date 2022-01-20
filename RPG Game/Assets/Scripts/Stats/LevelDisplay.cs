using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class    LevelDisplay : MonoBehaviour
    {
        Text text;
        BaseStats playerStats;

        private void Awake()
        {
            text = GetComponent<Text>();
            playerStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            playerStats.OnLevelUp += UpdateDisplay;
        }

        private void Start()
        {
            text.text = playerStats.GetLevel().ToString();
        }

        public void UpdateDisplay()
        {
            text.text = playerStats.GetLevel().ToString();
        }
    }
}