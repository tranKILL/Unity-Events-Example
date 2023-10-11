namespace NJ_Event
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Consumer : MonoBehaviour
    {
        private int coins = 0;
        private int totalClic = 0;

        void OnEnable()
        {
            EventManager.StartListening("addCoins", OnAddCoins);
            EventManager.StartListening("clic", OnClic);
        }

        void OnDisable()
        {
            EventManager.StopListening("addCoins", OnAddCoins);
            EventManager.StopListening("clic", OnClic);
        }

        void OnAddCoins(Dictionary<string, object> message)
        {
            var amount = (int)message["amount"];
            coins += amount;
        }
        void OnClic(Dictionary<string, object> message)
        {
            var clic = (int)message["clic"];
            totalClic += clic;
        }
    }
}