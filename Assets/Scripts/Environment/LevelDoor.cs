using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private string nextSceneName;

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

    private void OpenDoor()
    {
        if (isOpen)
            return;

        isOpen = true;
        spriteRenderer.sprite = openedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !isOpen)
            return;

        SceneManager.LoadScene(nextSceneName);
    }
}
