namespace NJ_Event
{
    using System.Collections;
    using TMPro;
    using UnityEngine;
    
    public class PlayAllPrefabInFolderAndSubfolder : MonoBehaviour
    {
        public string resourcesPrefabPath = "Mirza Beig/Particle Systems/Ultimate VFX/Prefabs/";
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
            float time;
            foreach (GameObject prefab in prefabs)
            {
                // Create a new instance of the prefab.
                GameObject vfx = Instantiate(prefab, transform.position, transform.rotation);
                vfxName.text = vfx.name.Replace("pf_vfx-ult_demo_psys_", "").Replace("(Clone)", "");
                // Start playing the vfx.
                vfx.SetActive(true);
                time = loopTime;
                yield return new WaitForSeconds(time);
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