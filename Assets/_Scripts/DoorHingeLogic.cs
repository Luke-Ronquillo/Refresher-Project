using System.Collections;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [Header("Door Properties")]
    [SerializeField] float openAngle;
    [SerializeField] float duration;
    [SerializeField] float timeTillDoorCloses;

    bool isOpen = false;
    public void DoorEntered()
    {
        if (!isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(OpenDoor());
        }
    }
    IEnumerator OpenDoor()
    {
        isOpen = true;

        float startAngle = transform.localEulerAngles.y;
        float endAngle = startAngle + openAngle;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            float currentAngle = Mathf.LerpAngle(startAngle, endAngle, t);

            transform.localEulerAngles = new Vector3(0, currentAngle, 0);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localEulerAngles = new Vector3(0, endAngle, 0);
    }
    public void DoorExited()
    {
        if (isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(CloseDoor());
        }
    }
    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(timeTillDoorCloses);

        float startAngle = transform.localEulerAngles.y;
        float endAngle = 0f;
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            float currentAngle = Mathf.LerpAngle(startAngle, endAngle, t);

            transform.localEulerAngles = new Vector3(0, currentAngle, 0);

            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localEulerAngles = new Vector3(0, endAngle, 0);
        isOpen = false;
    }
}
