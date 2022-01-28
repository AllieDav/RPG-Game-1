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
            GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().OnLevelUp -= RegenerateHealth;
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

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value == 0)
            {
                StartCoroutine(Die());
            }
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took " + damage + " damage.");

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

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        private IEnumerator Die()
        {
            if (isDead) StopCoroutine(Die());

            isDead = true;
            yield return new WaitForSeconds(0.3f);
            GetComponent<ActionScheduler>().CancelCurrentAction();
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}