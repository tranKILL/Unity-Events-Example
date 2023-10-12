namespace NJ_Event
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Cube2Producer : CubeEventProducer
    {
        public static event System.Action<Vector3, GameObject> OnLeftCube2Click;
        public override void OnEnable()
        {
        }
        public override void OnTriggerEnter2D(Collider2D other)
        {
            EventsHandler.TriggerEvent("subCoins", new Dictionary<string, object> { { "amount", -1 } });
        }
        public override void OnMouseDown()
        {
            OnLeftCube2Click?.Invoke(cubeSize * 2, gameObject);
            //OnMouseDown = true;
            //Debug.Log("1 Cube OnMouseLeftDown");
            //EventsHandler.TriggerEvent("leftClicCubeDown", new Dictionary<string, object> { { "clic", 1 } });
        }
        public override void OnMouseUp()
        {
//Debug.Log("1 Cube OnMouseLeftUp");
            //EventsHandler.TriggerEvent("OnLeftCube2Click", new Dictionary<string, object> { { "clic", 20 } });
        }
    }
}