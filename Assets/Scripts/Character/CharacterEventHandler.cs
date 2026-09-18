using UnityEngine;

public class CharacterEventHandler : MonoBehaviour
{
    private CharacterController controller;
    private CharacterHealth health;
    
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if(controller == null )
        {
            Debug.LogError("cant find CharacterController on this GameObject!");
        }
        health = GetComponent<CharacterHealth>();
        if (health == null)
        {
            Debug.LogError("cant find CharacterHealth on this GameObject!");
        }
    }

    private void OnEnable()
    {
        GameEvents.CharacterTakeHit += OnTakeHit;
        GameEvents.CharacterDeath += OnDeath;
    }

    private void OnDisable()
    {
        GameEvents.CharacterTakeHit -= OnTakeHit;
        GameEvents.CharacterDeath -= OnDeath;
    }

    private void OnDeath(GameObject victim, GameObject killer)
    {
        if (controller == null) return;

        if (victim != this.gameObject) return;

        controller.Die();//after the last frame of die animation the object will be destroy
    }

    private void OnTakeHit(GameObject victim, GameObject attacker, int damage)
    {
        if (controller == null || health == null)
        {
            return;
        }

        if (victim != this.gameObject || !health.IsAlive()) return;
        
        health.ApplyDamage(damage);
        GameEvents.OnCharacterHealthChange(victim, health.GetHealth() , health.GetFullHealth());
        if(health.IsAlive())
        {
            controller.TakeHit();
        }
        else
        {
            GameEvents.OnCharacterDeath(victim, attacker);
        }
    }
}
