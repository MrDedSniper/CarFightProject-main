using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _musicClip;

    private void Start()
    {
        if (_musicClip != null)
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = _musicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
