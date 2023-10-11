namespace NJ_Event
{
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    public class CubeEventProducer : MonoBehaviour
    {
        public static event System.Action<Vector3> OnLeftCubeClick;

        private Vector3 cubeSize;
        private void OnEnable()
        {
            cubeSize = transform.localScale;
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            EventsHandler.TriggerEvent("addCoins", new Dictionary<string, object> { { "amount", 3 } });
        }
        void OnMouseDown()
        {
            OnLeftCubeClick?.Invoke(cubeSize);
            //OnMouseDown = true;
            //Debug.Log("1 Cube OnMouseLeftDown");
            //EventsHandler.TriggerEvent("leftClicCubeDown", new Dictionary<string, object> { { "clic", 1 } });
        }
        void OnMouseUp()
        {
Debug.Log("1 Cube OnMouseLeftUp");
            EventsHandler.TriggerEvent("leftClicCubeUp", new Dictionary<string, object> { { "clic", 2 } });
        }
    }
}