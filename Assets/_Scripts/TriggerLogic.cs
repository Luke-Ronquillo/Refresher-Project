using UnityEngine;
using UnityEngine.Events;

public class TriggerLogic : MonoBehaviour
{
    [SerializeField] UnityEvent OnTriggerEntered;
    [SerializeField] UnityEvent OnTriggerExited;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Entered");
            OnTriggerEntered.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Exited");
            OnTriggerExited.Invoke();
        }
    }
}