using NJ_Event;
using System.Collections;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public float moveDistance = 1.0f; // The distance the cube will move up and down
    private float distanceCovered = 0;
    private float distanceCoveredSave = 0;
    public float moveSpeed = 1.0f; // The speed of the movement
    public float waitingTime = 0.2f;

    private Vector3 startBasePosition;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float journeyLength;
    private Vector3 lastPosition;
    private float initialMoveDistance;
    private int nbCubeSwitch = 0;
    private bool isMoving = true;
    private Coroutine moveCoroutine;

    public static event System.Action<int> OnSwitchCube;

    void Start()
    {
        initialMoveDistance = moveDistance;
        startBasePosition = transform.position;
        startPosition = startBasePosition;
        endPosition = startBasePosition + Vector3.up * moveDistance;
        journeyLength = Vector3.Distance(startPosition, endPosition);
        moveCoroutine = StartCoroutine(MoveUpAndDown());
        GameController.FreezeCube += Freeze; GameController.UnfreezeCube += Unfreeze;
    }

    private void OnDestroy()
    {
        GameController.FreezeCube -= Freeze; GameController.UnfreezeCube -= Unfreeze;
    }
    public void SetSpeed(float _speed = 1) //useless ?
    {
        moveSpeed = _speed;
    }
    public void Freeze()
    {
        if (isMoving)
        {
            isMoving = false;
            distanceCoveredSave = distanceCovered;
            StopCoroutine(moveCoroutine);
            startPosition = lastPosition;
Debug.Log("Freeze:" + distanceCoveredSave + " - lastPosition:" + startPosition);
        }
    }

    public void Unfreeze()
    {
        if (!isMoving)
        {
            isMoving = true;
            moveDistance = initialMoveDistance;
            distanceCovered = distanceCoveredSave;
Debug.Log("Unfreeze:" + distanceCovered + " - lastPosition:" + startPosition);
            moveCoroutine = StartCoroutine(MoveUpAndDown());
        }
    }

    IEnumerator MoveUpAndDown()
    {
Debug.Log("MoveUpAndDown");
        Vector3 tempPosition;
        float startTime, distanceMoved, fractionOfJourney;

        while (isMoving)
        {
            startTime = Time.time;
            while (isMoving && distanceCovered < journeyLength)
            {
                distanceMoved = (Time.time - startTime) * moveSpeed;
                fractionOfJourney = distanceMoved / journeyLength;
                transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                lastPosition = transform.position;
                distanceCovered = Vector3.Distance(startPosition, transform.position);
                //distanceCoveredSave = distanceCovered;
Debug.Log(distanceCovered + " < " + journeyLength);
                yield return null;
            }

            if (isMoving) {
                tempPosition = startPosition;
                startPosition = endPosition;
                endPosition = tempPosition;
                distanceCoveredSave = distanceCovered = 0;
                //distanceCoveredSave = distanceCovered = Vector3.Distance(startPosition, transform.position);
                nbCubeSwitch++;
                OnSwitchCube?.Invoke(nbCubeSwitch);
Debug.Log("OnSwitchCube");
                yield return new WaitForSeconds(waitingTime);
            }
        }
    }
}