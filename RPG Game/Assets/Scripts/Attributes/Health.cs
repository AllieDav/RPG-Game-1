using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Core;
using RPG.Stats;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = -1f;
        [SerializeField] float regenerationPercentage = 70f;
        bool isDead = false;


        private void Awake()
        {
            if (healthPoints < 0)
            {
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }

            GetComponent<BaseStats>().OnLevelUp += RegenerateHealth;
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public bool GetIsDead()
        {
            return isDead;
        }

        public float GetHealth()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
            {
                StartCoroutine(Die());
            }
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took " + damage + " damage.");

            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
            {
                StartCoroutine(Die());
                AwardXP(instigator);
            }
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
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
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