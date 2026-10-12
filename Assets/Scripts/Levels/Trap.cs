using Unity.Cinemachine;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            LevelEvents.OnLevelFailed();
        }
    }
}
