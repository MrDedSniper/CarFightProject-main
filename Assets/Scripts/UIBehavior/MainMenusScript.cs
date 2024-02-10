using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenusScript : MonoBehaviour
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
        _soundsSource.GetComponent<AudioSource>().volume = _soundsSlider.value;
    }

}
