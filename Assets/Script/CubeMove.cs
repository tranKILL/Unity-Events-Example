using NJ_Event;
using System.Collections;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float moveDistance = 1.0f; // The distance the cube will move up and down
    public float moveSpeed = 1.0f; // The speed of the movement
    public float waitingTime = 0.2f;

    private Vector3 startPosition;
    private Vector3 endPosition;
    private int nbCubeSwitch = 0;
    private bool isMoving = false;

    public static event System.Action<int> OnSwitchCube;

    void Start()
    {
        GameController.FreezeCube += Freeze;
        GameController.UnfreezeCube += Unfreeze;
        startPosition = transform.position; // Store the initial position of the cube
        endPosition = startPosition + Vector3.up * moveDistance; // Calculate the end position for the upward movement
        StartCoroutine(MoveUpAndDown()); // Start the movement coroutine
    }
    public void SetSpeed(float _speed = 1) //useless ?
    {
        moveSpeed = _speed;
    }
    public void Freeze()
    {
        isMoving = false;
    }
    public void Unfreeze()
    {
        isMoving = true;
    }

    IEnumerator MoveUpAndDown()
    {
        while (true)
        {
            if (isMoving)
            {
                // Move the cube from its current position to the end position
                float journeyLength = Vector3.Distance(startPosition, endPosition);
                float startTime = Time.time;
                float distanceCovered = 0f;

                while (distanceCovered < journeyLength)
                {
                    float distanceMoved = (Time.time - startTime) * moveSpeed;
                    float fractionOfJourney = distanceMoved / journeyLength;
                    transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                    distanceCovered = Vector3.Distance(startPosition, transform.position);
                    yield return null;
                }

                // Swap start and end positions to move in the opposite direction
                Vector3 tempPosition = startPosition;
                startPosition = endPosition;
                endPosition = tempPosition;
                nbCubeSwitch++;
                OnSwitchCube?.Invoke(nbCubeSwitch);

                // Wait for a moment at the top and bottom
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}