using System;
using TMPro;
using UnityEngine;
using static Unity.Cinemachine.IInputAxisOwner.AxisDescriptor;

public class TimerHUD : MonoBehaviour
{
    [SerializeField] private TimerObjective timerObjective;
    private TextMeshProUGUI textMeshPro;
    private Color currentColor;
    [SerializeField] private Color expireColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if(textMeshPro == null)
        {
            Debug.LogError("cant find any TextMeshProUGUI on this gameObject");
        }
        else
        {
            currentColor = textMeshPro.color;
        }
    }

    void OnEnable()
    {
        if (timerObjective != null)
        {
            timerObjective.TickEvent += Tick;
        }
    }

    private void OnDisable()
    {
        if (timerObjective != null)
        {
            timerObjective.TickEvent -= Tick;
        }
    }

    private void Tick(TimerObjective tickedTimerObjective, float secondsLeft)
    {
        if(tickedTimerObjective == timerObjective)
        {
            if(secondsLeft > 0f)
            {
                UpdateTimerText(secondsLeft);
            }
            else
            {
                UpdateTimerTextToExpire();
            }
        }
    }


    private void UpdateTimerText(float secondsLeft)
    {
        if (textMeshPro == null) return;
        int minuts = (int)(secondsLeft / 60);
        int seconds = (int)(secondsLeft % 60);
        textMeshPro.text = $"Timer: {minuts}:{seconds}";
    }
    private void UpdateTimerTextToExpire()
    {
        if (textMeshPro == null) return;
        textMeshPro.color = expireColor;
        textMeshPro.text = "Timer: END";
    }

}
