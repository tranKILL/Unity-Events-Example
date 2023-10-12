namespace NJ_Event
{
    using System;
    using UnityEngine;

    public class CubeMove : MonoBehaviour
    {
        public float moveDistance = 4.0f;
        public float moveSpeed = 1.0f;
        public float moveTime = 2.0f;
        public bool isMoving = true;
        public bool isFrozen = false;

        private Vector3 startPosition;
        private Vector3 endPosition;
        private float timeElapsed = 0.0f;
        private int nbCubeSwitch = 0;

        public static event System.Action<int> OnSwitchCube;

        void Start()
        {
            startPosition = transform.position;
            endPosition = startPosition + Vector3.up * moveDistance;
            GameController.FreezeCube += Freeze;
            GameController.UnfreezeCube += Unfreeze;
        }

        void Update()
        {
            if (isMoving && !isFrozen)
            {
                if (timeElapsed < moveTime)
                {
                    timeElapsed += Time.deltaTime;
                    transform.position += moveSpeed * Time.deltaTime * (endPosition - startPosition).normalized;
                }
                else
                {
                    timeElapsed = 0;
                    endPosition = startPosition;
                    startPosition = transform.position;
                    nbCubeSwitch++;
                    OnSwitchCube?.Invoke(nbCubeSwitch);
                }
            }
        }
        private void OnDestroy()
        {
            GameController.FreezeCube -= Freeze;
            GameController.UnfreezeCube -= Unfreeze;
        }

        private void Unfreeze(GameObject cube)
        {
            if (cube == gameObject)
            {
                isFrozen = false;
            }
        }

        public void Freeze()
        {
            isFrozen = true;
        }

        public void Unfreeze()
        {
            isFrozen = false;
        }
    }
}