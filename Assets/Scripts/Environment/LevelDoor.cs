using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openedSprite;

    private bool isOpen;

    private void Awake()
    {
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        isOpen = false;
        spriteRenderer.sprite = closedSprite;
    }

    private void CanOpenFinalGate(StateSO state)
    {
        if (state.CanOpenFinalGate)
        {
            spriteRenderer.sprite = openedSprite;
            isOpen = true;
        }
        else
        {
            spriteRenderer.sprite = closedSprite;
            isOpen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isOpen)
        {
            LevelEvents.OnFinishLineTriggered();
        }
    }

    private void OnEnable()
    {
        GameStateEvents.StateUpdated += CanOpenFinalGate;
    }

    private void OnDisable()
    {
        GameStateEvents.StateUpdated -= CanOpenFinalGate;
    }
}
