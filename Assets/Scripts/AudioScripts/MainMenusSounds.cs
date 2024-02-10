using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenusSounds : MonoBehaviour
{
    [SerializeField] private Button _optionsButton;
    [SerializeField] private GameObject _optionsCanvas;
    
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private GameObject _musicSource;
    
    [SerializeField] private Slider _soundsSlider;
    [SerializeField] private GameObject _soundsSource;

    private bool isOptionsOpen = false;

    void Awake()
    {
        _musicSlider.onValueChanged.AddListener(delegate { AdjustMusicVolume(); });
        _soundsSlider.onValueChanged.AddListener(delegate { AdjustSoundsVolume(); });

        SignForSoundsSource();
    }

    public void SignForSoundsSource()
    {
        _soundsSource = GameObject.FindWithTag("SoundsSource");
        if (_soundsSource == null)
        {
            Debug.LogError("Object with tag 'SoundSource' not found!");
        }
    }
    
    public void OptionCanvasOnAndOff()
    {
        if (!isOptionsOpen)
        {
            _optionsCanvas.gameObject.SetActive(true);
            isOptionsOpen = true;
            
        }
        else if (isOptionsOpen)
        {
            _optionsCanvas.gameObject.SetActive(false);
            isOptionsOpen = false;
        }
    }
    
    public void AdjustMusicVolume()
    {
        _musicSource.GetComponent<AudioSource>().volume = _musicSlider.value;
    }

    public void AdjustSoundsVolume()
    {
        AudioSource[] audioSources = _soundsSource.GetComponents<AudioSource>();

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = _soundsSlider.value;
        }
    }
}
