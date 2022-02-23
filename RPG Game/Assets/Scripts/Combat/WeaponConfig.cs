using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Attributes;
using System;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] public float weaponDamage = 5f;
        [SerializeField] public float weaponRange = 2f;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] Weapon equippedPrefab = null;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        [SerializeField] private float percentageBonus;
        const string weaponName = "Weapon";

        public Weapon SpawnWeapon(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);
            Weapon weapon = null;


            if (equippedPrefab!=null)
            {
                Transform handTransform = GetTransform(rightHandTransform, leftHandTransform);
                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }


            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;

            if (animatorOverride!=null) animator.runtimeAnimatorController = animatorOverride;

            else if (overrideController!=null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            return weapon;
        }

        public void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null) { oldWeapon = leftHand.Find(weaponName); }
            if (oldWeapon == null) return;

            oldWeapon.name = "old weapon";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHandTransform;
            else handTransform = leftHandTransform;
            return handTransform;
        }

        public bool HasProjectile() { return projectile != null; }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            Projectile projectileInstance =
               Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            //projectileInstance.SetTarget(target, instigator, weaponDamage);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }

        public float GetPercentageBonus()
        {
            return percentageBonus;
        }
    }
}
