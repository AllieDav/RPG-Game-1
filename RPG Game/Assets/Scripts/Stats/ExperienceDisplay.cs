using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Text text;
        Experience experience;

        private void Awake()
        {
            text = GetComponent<Text>();
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void OnEnable()
        {
            experience.OnExperienceGained += UpdateDisplay;
        }

        private void OnDisable()
        {
            experience.OnExperienceGained -= UpdateDisplay;
        }

        private void Start()
        {
            text.text = experience.GetPoints().ToString();
        }

        public void UpdateDisplay()
        {
            text.text = experience.GetPoints().ToString();
        }
    }
}