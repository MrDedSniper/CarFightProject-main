using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool _isPaused = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 0f; // Ставим игру на паузу при запуске сцены
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isPaused)
            {
                Time.timeScale = 1f; // Возобновляем игру
                _isPaused = false;
            }
            else
            {
                Time.timeScale = 0f; // Ставим игру на паузу
                _isPaused = true;
            }
        }
    }
}