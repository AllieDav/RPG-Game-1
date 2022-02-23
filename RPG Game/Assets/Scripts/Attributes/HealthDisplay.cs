using RPG.Stats;
using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] GameObject greenFill = null;
        [SerializeField] GameObject redBackround = null;
        GameObject player = null;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            redBackround.SetActive(false);

            SetMaxValue();
            UpdateDisplay(GetComponent<Slider>().maxValue);
        }

        private void OnEnable()
        {
            player.GetComponent<BaseStats>().onLevelUp += SetMaxValue;
        }

        private void OnDisable()
        {
            player.GetComponent<BaseStats>().onLevelUp -= SetMaxValue;
        }

        private void SetMaxValue()
        {
            GetComponent<Slider>().maxValue = player.GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Update()
        {
            UpdateDisplay(player.GetComponent<Health>().GetHealth());
        }

        private void UpdateDisplay(float health)
        {
            GetComponent<Slider>().value = health;

            if (health == 0)
            {
                greenFill.SetActive(false);
                redBackround.SetActive(true);
            }

            if (GetComponent<Slider>().maxValue == health)
            {
                greenFill.SetActive(true);
                redBackround.SetActive(false);
            }

            else redBackround.SetActive(true);
        }
    }
}