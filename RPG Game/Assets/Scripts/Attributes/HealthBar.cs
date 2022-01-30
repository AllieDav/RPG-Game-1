using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas canvas = null;


        private void Start()
        {
            canvas.enabled = false;
        }

        public void UpdateBar()
        {
            if ((Mathf.Approximately(health.GetFraction(), 0)) || Mathf.Approximately(health.GetFraction(), 1))
            {
                canvas.enabled = false;
                return;
            }

            canvas.enabled = true;
            foreground.localScale = new Vector3(health.GetFraction(), 1f, 1f);
        }
    }
}