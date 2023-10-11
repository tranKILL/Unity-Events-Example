namespace NJ_Event
{
    using UnityEngine;
    using TMPro;
    using System.Collections.Generic;

    [RequireComponent(typeof(TMP_Text))]
    [DisallowMultipleComponent]
    public class ScoreManager : MonoBehaviour
    {
        private TMP_Text scoreText; // Reference to the TextMeshPro Text object
        private int score = 0;

        private void Awake()
        {
            // Initialize the score text
            scoreText = GetComponent<TMP_Text>();
            UpdateScoreText();
        }
        // Call this method whenever you want to update the score
        public void IncreaseScore(int points)
        {
            score += points;
            UpdateScoreText();
            EventManager.TriggerEvent("scoreUpdate", new Dictionary<string, object> { { "scoreText", scoreText.text } });
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