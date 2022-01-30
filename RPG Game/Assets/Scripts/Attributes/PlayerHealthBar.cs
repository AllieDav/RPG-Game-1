using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Attributes
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;
        Health health = null;


        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        public void UpdateBar()
        {
            foreground.localScale = new Vector3(health.GetFraction(), 1f, 1f);
        }
    }
}