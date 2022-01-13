using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression")]
    public class Progression : ScriptableObject
    {
        [SerializeField] public ProgressionCharacterClass[] characterClasses = null;


        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if(progressionClass.characterClass == characterClass)
                {
                    return progressionClass.health[level - 1];
                }
            }

            return 0;
        }

        [System.Serializable]
        public class ProgressionCharacterClass
        {
            [SerializeField] public CharacterClass characterClass;
            [SerializeField] public float[] health;
            [SerializeField] public float[] experiencePoints;
        }
    }
}