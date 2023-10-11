namespace NJ_Event {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(ScoreManager))]

    public class GameController : MonoBehaviour {
        private ScoreManager m_scoreManager;

        void OnEnable() {
            EventManager.StartListening("scoreUpdate", OnScoreUpdate);
            EventManager.StartListening("addCoins", OnAddCoins);
            EventManager.StartListening("clic", OnClic);
        }

        void OnScoreUpdate(Dictionary<string, object> dictionary)
        {
            m_scoreManager.UpdateScoreText();
        }

        void OnDisable() {
            EventManager.StopListening("addCoins", OnAddCoins);
            EventManager.StopListening("clic", OnClic);
        }
  
        void OnAddCoins(Dictionary<string, object> message) {
            var amount = (int) message["amount"];
            coins += amount;
        }
        void OnClic(Dictionary<string, object> message)
        {
            var clic = (int)message["clic"];
            totalClic += clic;
        }
    }
}