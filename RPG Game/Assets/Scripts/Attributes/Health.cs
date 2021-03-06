using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;
using GameDevTV.Utils;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70f;
        [SerializeField] TakeDamageEvent takeDamage;

        LazyValue<float> healthPoints;
        bool isDead = false;

        [System.Serializable]
        public class TakeDamageEvent : UnityEvent<float>
        {
        }


        private void Awake()
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
            if (healthPoints.value < 0)
            {
                healthPoints.value = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += Heal;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= Heal;
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public bool GetIsDead()
        {
            return isDead;
        }

        public float GetHealth()
        {
            return healthPoints.value;
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health);
        }


        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value == 0)
            {
                StartCoroutine(Die());
            }
            if (gameObject.tag == "Player") FindObjectOfType<PlayerHealthBar>().UpdateBar();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            if (healthPoints.value == 0)
            {
                StartCoroutine(Die());
                AwardXP(instigator);
            }
            else takeDamage.Invoke(damage);
        }

        private void AwardXP(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.Experience));
        }

        public void Heal()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);

            if (gameObject.tag == "Player") FindObjectOfType<PlayerHealthBar>().UpdateBar();
        }

        private IEnumerator Die()
        {
            if (isDead) StopCoroutine(Die());

            isDead = true;
            yield return new WaitForSeconds(0.3f);
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<Animator>().SetTrigger("die");

            GetComponentInChildren<HealthBar>().UpdateBar();
            if (gameObject.tag == "Player") FindObjectOfType<PlayerHealthBar>().UpdateBar();
        }
    }
}