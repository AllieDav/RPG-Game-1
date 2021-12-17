using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] public float weaponDamage = 5f;
        [SerializeField] public float weaponRange = 2f;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] bool isRightHanded = true;

        public void SpawnWeapon(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            if (equippedPrefab!=null)
            {
                Transform handTransform;
                if (isRightHanded) handTransform = rightHandTransform;
                else handTransform = leftHandTransform;
                Instantiate(equippedPrefab, handTransform);
            }
            if (animatorOverride!=null) animator.runtimeAnimatorController = animatorOverride;
        }
    }
}
