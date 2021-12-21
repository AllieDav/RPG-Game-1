using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Saving
{
    public class AutoSave : MonoBehaviour
    {
        [SerializeField] float saveTime = 3f;
        float timer = 0;
        bool autoSave = false;

        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= saveTime)
            {
                autoSave = true;
                timer = 0;
            }
            else autoSave = false;
        }

        public bool GetAutoSave() { return autoSave; }
    }
}