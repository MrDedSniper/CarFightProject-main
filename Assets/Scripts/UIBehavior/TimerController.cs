using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public float matchTime = 120f; // Время матча в секундах (2 минуты)
    private float currentTime = 0f;
    private bool isTimerRunning = false;
    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        currentTime = matchTime;
        isTimerRunning = true;
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                isTimerRunning = false;
                TimeIsUp();
            }
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimeIsUp()
    {
        // В этом методе вы можете добавить логику подсчёта очков или другие действия, которые должны произойти после истечения времени
        Debug.Log("Time is up! Calculating scores...");
        CalculateScores();
    }

    private void CalculateScores()
    {
        // Логика подсчёта очков
        Debug.Log("Scores calculated!");
    }
}