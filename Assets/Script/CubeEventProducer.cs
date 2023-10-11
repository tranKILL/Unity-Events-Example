namespace NJ_Event
{
    using System.Collections.Generic;
    using UnityEngine;

    public class CubeEventProducer : MonoBehaviour
    {
        public static event System.Action<Vector3> OnLeftCubeClick;

        protected Vector3 cubeSize;
        public virtual void OnEnable()
        {
            cubeSize = transform.localScale;
        }
        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            EventsHandler.TriggerEvent("addCoins", new Dictionary<string, object> { { "amount", 3 } });
        }
        public virtual void OnMouseDown()
        {
            OnLeftCubeClick?.Invoke(cubeSize);
            //OnMouseDown = true;
            //Debug.Log("1 Cube OnMouseLeftDown");
            //EventsHandler.TriggerEvent("leftClicCubeDown", new Dictionary<string, object> { { "clic", 1 } });
        }
        public virtual void OnMouseUp()
        {
//Debug.Log("1 Cube OnMouseLeftUp");
            EventsHandler.TriggerEvent("leftClicCubeUp", new Dictionary<string, object> { { "clic", 2 } });
        }
    }
}