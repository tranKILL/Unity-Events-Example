namespace NJ_Event
{
    using UnityEngine;
    using TMPro;
    using System.Collections.Generic;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreManager : MonoBehaviour
    {
        private TMP_Text scoreText; // Reference to the TextMeshPro Text object
        private int score = 0;

        private void Awake()
        {
            // Initialize the score text
            scoreText = GetComponent<TMP_Text>();
            //scoreText.transform.position = new Vector3 (0, 0, 0);
            UpdateScoreText();
        }

        public void IncreaseScore(int points)
        {
            score += points;
            //UpdateScoreText();
            EventsHandler.TriggerEvent("scoreUpdate", new Dictionary<string, object> { { "scoreText", score } });
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