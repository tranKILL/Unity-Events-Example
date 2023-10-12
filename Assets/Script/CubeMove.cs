using NJ_Event;
using System.Collections;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float moveDistance = 1.0f; // The distance the cube will move up and down
    public float moveSpeed = 1.0f; // The speed of the movement
    public float waitingTime = 0.2f;

    private Vector3 startBasePosition;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 lastPosition;
    private float journeyLength;
    private float startTime;
    private int nbCubeSwitch = 0;
    private bool isMoving = true;
    private Coroutine moveCoroutine;

    public static event System.Action<int> OnSwitchCube;

    void Start()
    {
        startBasePosition = transform.position;
        startPosition = startBasePosition;
        endPosition = startBasePosition + Vector3.up * moveDistance;
        journeyLength = Vector3.Distance(startPosition, endPosition);
        GameController.FreezeCube += Freeze;
        GameController.UnfreezeCube += Unfreeze;
        ResumeMovement(); // Start the movement coroutine
    }

    private void OnDestroy()
    {
        GameController.FreezeCube -= Freeze;
        GameController.UnfreezeCube -= Unfreeze;
    }

    public void Freeze()
    {
        if (isMoving)
        {
            isMoving = false;
            StopCoroutine(moveCoroutine);
        }
    }

    public void Unfreeze()
    {
        if (!isMoving)
        {
            isMoving = true;
            ResumeMovement(); // Restart the movement
        }
    }

    private void ResumeMovement()
    {
        moveCoroutine = StartCoroutine(MoveUpAndDown());
    }

    IEnumerator MoveUpAndDown()
    {
        Vector3 tempPosition;
        float distanceMoved, fractionOfJourney;
        float distanceCovered = 0;

        while (isMoving)
        {
            startTime = Time.time;
            while (distanceCovered < journeyLength)
            {
                distanceMoved = (Time.time - startTime) * moveSpeed;
                fractionOfJourney = distanceMoved / journeyLength;
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                lastPosition = transform.position;
                distanceCovered = Vector3.Distance(startPosition, transform.position);
                yield return null;
            }

            //if (isMoving)
            {
                tempPosition = startPosition;
                startPosition = endPosition;
                endPosition = tempPosition;
                distanceCovered = 0;
                nbCubeSwitch++;
                OnSwitchCube?.Invoke(nbCubeSwitch);
                yield return new WaitForSeconds(waitingTime);
            }
        }
    }
}
