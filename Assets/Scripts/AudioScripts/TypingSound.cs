using UnityEngine;
using TMPro;

public class TypingSound : MonoBehaviour
{
    public TMP_InputField _inputField;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _typingSound;

    private void Start()
    {
        _inputField.onValueChanged.AddListener(delegate { PlayTypingSound(); });
    }

    private void PlayTypingSound()
    {
        if (_typingSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(_typingSound);
        }
    }
}