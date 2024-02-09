using UnityEngine;

public class UISound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void PlaySingleSound(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}
