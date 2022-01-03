using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

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
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void SpawnWeapon(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);

            if (equippedPrefab!=null)
            {
                Transform handTransform = GetTransform(rightHandTransform, leftHandTransform);
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;
            }
            if (animatorOverride!=null) animator.runtimeAnimatorController = animatorOverride;
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

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance =
               Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }
    }
}
