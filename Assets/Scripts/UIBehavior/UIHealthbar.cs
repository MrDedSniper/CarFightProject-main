using TMPro;
using UnityEngine;

public class UIHealthbar : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private PlayerHealth _playerHealth;
    
    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Update()
    {
        _healthText.text = _playerHealth._currentHealth.ToString();
    }
}
