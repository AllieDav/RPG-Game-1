using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Control;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinitionIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] float timeToFadeOut = 3f;
        [SerializeField] float timeToFadeIn = 2f;
        [SerializeField] float waitWhileFaded = 1f;

        [SerializeField] int SceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinitionIdentifier destinition;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (SceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(timeToFadeOut);

            FindObjectOfType<SavingWrapper>().Save();

            DontDestroyOnLoad(gameObject);
            yield return SceneManager.LoadSceneAsync(SceneToLoad);

            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;

            FindObjectOfType<SavingWrapper>().Load();
            FindObjectOfType<SavingWrapper>().Save();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(waitWhileFaded);
            yield return fader.FadeIn(timeToFadeIn);

            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.transform.position;
            player.transform.rotation = otherPortal.spawnPoint.transform.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) { continue; }

                if (portal.destinition != destinition) { continue; }


                return portal;
            }

            return null;
        }
    }

}