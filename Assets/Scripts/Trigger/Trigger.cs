using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private GameObject triggerBy;
    private bool isTrigger = false;
    [SerializeField] private UnityEvent triggerEnterEvent;
    [SerializeField] private UnityEvent triggerExitEvent;
    [SerializeField] private bool invokeExitEventOnStart = false;

    private void Awake()
    {
        Collider2D colliderTrigger = GetComponent<Collider2D>();
        if (colliderTrigger == null)
        {
            Debug.LogError("there is no Collider2D on this GameObject!");
        }

        if(triggerBy == null)
        {
            Debug.LogWarning("there is no triggerBy gameobject to be trigger by!");
        }
    }

    private void Start()
    {
        if (invokeExitEventOnStart && triggerExitEvent != null)
        {
            triggerExitEvent.Invoke();
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        if(other.gameObject == triggerBy && !isTrigger)
        {
            if(triggerEnterEvent != null)triggerEnterEvent.Invoke();
            isTrigger = true;
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D other)
    {
        if (other.gameObject == triggerBy && isTrigger)
        {
            if (triggerExitEvent != null) triggerExitEvent.Invoke();
            isTrigger = false;
        }
    }
}
