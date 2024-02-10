using UnityEngine;

public class UISoundsManager : MonoBehaviour
{
    public AudioSource buttonSound;
    public AudioSource inputFieldSound;

    public void PlayButtonSound()
    {
        buttonSound.Play();
    }

    public void PlayInputFieldSound()
    {
        inputFieldSound.Play();
    }
}
