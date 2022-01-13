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
            GetComponent<Slider>().maxValue = player.GetComponent<BaseStats>().GetLevelHealth();
        }

        private void Start()
        {
            UpdateDisplay(GetComponent<Slider>().maxValue);
        }

        private void Update()
        {
            UpdateDisplay(player.GetComponent<Health>().GetHealth());
        }

        private void UpdateDisplay(float health)
        {
            GetComponent<Slider>().value = health;

            if (player.GetComponent<Health>().GetHealth() <= 0)
            {
                greenFill.SetActive(false);
                redBackround.SetActive(true);
            }

            if (GetComponent<Slider>().maxValue == GetComponent<Slider>().value)
            {
                greenFill.SetActive(true);
                redBackround.SetActive(false);
            }

            else redBackround.SetActive(true);
        }
    }
}