using RPG.Stats;
using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        //[SerializeField] GameObject greenFill = null;
        //[SerializeField] GameObject redBackround = null;
        //Health enemy = null;


        //private void Update()
        //{
        //    if (enemy != null)
        //    {
        //        StartCoroutine(UpdateDisplay(enemy.GetComponent<Health>().GetHealth()));
        //
        //        if (!enemy.GetIsDead())
        //        {
        //            transform.position = Camera.main.WorldToScreenPoint(enemy.transform.position + Vector3.up * 3);
        //        }
        //    }
        //    else
        //    {
        //        ToggleDisplay(false);
        //    }
        //}

        //private void ToggleDisplay(bool shouldShow)
        //{
        //    transform.GetChild(0).gameObject.SetActive(shouldShow);
        //}

        //public void StartDisplay(Health target)
        //{
        //    enemy = target;
        //
        //    GetComponent<Slider>().maxValue = enemy.GetComponent<BaseStats>().GetStat(Stat.Health);
        //
        //    ToggleDisplay(true);
        //
        //    StartCoroutine(UpdateDisplay(enemy.GetComponent<Health>().GetHealth()));
        //}
        //
        //private IEnumerator UpdateDisplay(float health)
        //{
        //    GetComponent<Slider>().value = health;
        //
        //    if (health <= 0)
        //    {
        //        greenFill.SetActive(false);
        //        redBackround.SetActive(true);
        //
        //        yield return new WaitForSeconds(2f);
        //        ToggleDisplay(false);
        //        enemy = null;
        //    }

        //    if (GetComponent<Slider>().maxValue == health)
        //    {
        //        greenFill.SetActive(true);
        //        redBackround.SetActive(false);
        //    }

        //    else redBackround.SetActive(true);
        //}
    }
}