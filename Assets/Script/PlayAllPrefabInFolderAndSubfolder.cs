using System.Collections;
using TMPro;
using UnityEngine;

namespace NJ_Event
{
    public class PlayAllPrefabInFolderAndSubfolder : MonoBehaviour
    {
        public string resourcesPrefabPath = "Mirza Beig/Particle Systems/Ultimate VFX/Prefabs/";
        public Vector3 decalPrefab = new Vector3(0, 0, -2);
        public float loopTime = 2.2f;
        public float oneshotTime = 2.0f;

        private GameObject[] prefabs;
        public TMP_Text vfxName;

        private void Awake()
        {
            // Load all prefabs in the prefabPath folder.
            prefabs = Resources.LoadAll<GameObject>(resourcesPrefabPath);
            ShuffleArray(prefabs);
        }
        private void Start()
        {
            StartCoroutine(PlayPrefabs());
        }

        private IEnumerator PlayPrefabs()
        {
            foreach (GameObject prefab in prefabs)
            {
                // Create a new instance of the prefab.
                GameObject vfx = Instantiate(prefab, transform.position + decalPrefab, transform.rotation);
                vfxName.text = vfx.name.Replace("pf_vfx-ult_demo_psys_", "").Replace("(Clone)", "");

                float animationDuration = loopTime;
                var particleSystem = vfx.GetComponent<ParticleSystem>();
                Animation animation = null;
                if (particleSystem != null)
                {
                    var mainModule = particleSystem.main;
                    animationDuration = mainModule.duration;
                }
                else
                {
                    animation = vfx.GetComponent<Animation>();
                    if (animation != null)
                    {
                        animationDuration = animation.clip.length;
                    }
                }

                // Play the animation multiple times if it's shorter than loopTime.
                int repetitions = Mathf.CeilToInt(loopTime / animationDuration);
                //if (repetitions == 1) repetitions++;
//Debug.Log("animationDuration:" + animationDuration + " - loopTime:" + loopTime + " - repetitions:" + repetitions);
                for (int i = 0; i < repetitions; i++)
                {
                    if (particleSystem != null)
                    {
                        particleSystem.Play();
                    }
                    else if (animation != null)
                    {
                        animation.Play();
                    }

                    // Wait for the animation to complete.
                    yield return new WaitForSeconds(animationDuration);
                }

                Destroy(vfx);
            }
        }

        private void ShuffleArray(GameObject[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                // Generate a random index between i and the end of the array.
                int randomIndex = UnityEngine.Random.Range(i, n);

                // Swap elements at i and randomIndex.
                GameObject temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }
    }
}
