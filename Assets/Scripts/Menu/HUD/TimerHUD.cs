using TMPro;
using UnityEngine;

public class TimerHUD : MonoBehaviour
{
    [SerializeField] private TimerObjective timerObjective;
    private TextMeshProUGUI textMeshPro;
    private Color currentColor;
    [SerializeField] private Color expireColor;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro == null)
        {
            Debug.LogError("Can't find any TextMeshProUGUI on this GameObject");
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
        textMeshPro.color = currentColor;
        int totalSeconds = Mathf.CeilToInt(secondsLeft);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        textMeshPro.text = $"Timer: {minutes:D2}:{seconds:D2}";
    }
    private void UpdateTimerTextToExpire()
    {
        if (textMeshPro == null) return;
        textMeshPro.color = expireColor;
        textMeshPro.text = "Timer: END";
    }

}
