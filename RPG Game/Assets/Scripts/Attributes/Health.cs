using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;
        bool isDead = false;


        private void Awake()
        {
            healthPoints = GetComponent<BaseStats>().GetLevelHealth();
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
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
            {
                StartCoroutine(Die());
            }
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