using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] Text text = null;

        private void Start()
        {
            GetComponentInChildren<Animation>().playAutomatically = true;
            GetComponentInChildren<Animation>().Play();

            if (GetComponentInChildren<Animation>().isPlaying == true) print("playing fade animation");
        }

        private void DestroyText()
        {
            Destroy(gameObject);
        }

        public void SetText(float damageAmount)
        {
            text.text = damageAmount.ToString();
        }
    }
}
