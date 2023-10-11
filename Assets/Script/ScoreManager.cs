namespace NJ_Event
{
    using UnityEngine;
    using TMPro;
#if UNITY_EDITOR
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
#endif
    public class ScoreManager : MonoBehaviour
    {
        private TMP_Text scoreText;
        private int score = 0;

        private void Awake()
        {
            scoreText = GetComponent<TMP_Text>();
            UpdateScoreText();
        }

        public void IncreaseScore(int points)
        {
            score += points;
            UpdateScoreText();
            //EventsHandler.TriggerEvent("scoreUpdate", new Dictionary<string, object> { { "scoreText", score } });
        }

        public void DecreaseScore(int points)
        {
            score -= points;
            UpdateScoreText();
            //EventsHandler.TriggerEvent("scoreUpdate", new Dictionary<string, object> { { "scoreText", score } });
        }

        public void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = score.ToString();
            }
        }
    }
}