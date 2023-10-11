namespace NJ_Event
{
    using UnityEngine;
    using TMPro;

#if UNITY_EDITOR
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
#endif

    public class LifeManager : MonoBehaviour
    {
        private TMP_Text lifeText;
        private int life;

        private void OnEnable()
        {
            lifeText = GetComponent<TMP_Text>();
            //UpdateLifeText();
        }

        public void IncreaseLife(int _life)
        {
            life += _life;
            UpdateLifeText();
        }

        public void DecreaseLife(int _life)
        {
            life -= _life;
            UpdateLifeText();
         }

        public void UpdateLifeText()
        {
Debug.Log("UpdateLifeText:" + life);
            if (lifeText != null)
            {
                lifeText.text = life.ToString();
            }
        }
    }
}