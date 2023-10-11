namespace NJ_Event
{
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    public class CubeEventProducer : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            EventsHandler.TriggerEvent("addCoins", new Dictionary<string, object> { { "amount", 3 } });
        }
        void OnMouseDown()
        {
            //OnMouseDown = true;
Debug.Log("1 Cube OnMouseLeftDown");
            EventsHandler.TriggerEvent("leftClicCubeDown", new Dictionary<string, object> { { "clic", 1 } });
        }
        void OnMouseUp()
        {
Debug.Log("1 Cube OnMouseLeftUp");
            EventsHandler.TriggerEvent("leftClicCubeUp", new Dictionary<string, object> { { "clic", 2 } });
        }
    }
}