using UnityEngine;

public class NormalDoorSwitch : MonoBehaviour
{
    [SerializeField] private GameObject openLocation;
    [SerializeField] private GameObject closeLocation;
    [SerializeField] private float MinimumDistanceToTarget = 0.01f;
    private Rigidbody2D rb2d;
    private Vector3 moveToLocation;
    [SerializeField] private float movementSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (openLocation == null || closeLocation == null)
        {
            Debug.LogError("openLocation or closeLocation are not set!");
        }

        rb2d = GetComponent<Rigidbody2D>();
        if (rb2d == null)
        {
            Debug.LogError("not found any Rigidbody2D on this GameObject!");
        }
        
        //stop the door from move on the start
        moveToLocation = this.gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        MoveDoor(Time.fixedDeltaTime);
    }

    public void OpenDoor()
    {
        if (openLocation != null)
        {
            moveToLocation = openLocation.transform.position;
        }
    }

    public void CloseDoor()
    {
        if(closeLocation != null)
        {
            moveToLocation = closeLocation.transform.position;
        }
    }

    private void MoveDoor(float deltaTime)
    {
        if (rb2d == null || closeLocation == null || openLocation == null) return;
        if(Vector3.Distance(this.gameObject.transform.position, moveToLocation) >= MinimumDistanceToTarget)//need to move
        {
            Vector2 newPos = Vector2.MoveTowards(rb2d.position, moveToLocation, movementSpeed * deltaTime);
            rb2d.MovePosition(newPos);
        }
    }

}
