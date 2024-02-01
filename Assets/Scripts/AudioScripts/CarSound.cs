using UnityEngine;

public class CarSound : MonoBehaviour
{
    [SerializeField] private CarControls _carControls;
    
    [SerializeField] private AudioSource _audioSourceOneShot;
    [SerializeField] private AudioSource _audioSourceLoop;
    
    [SerializeField] private AudioClip _engineClip;
    [SerializeField] private AudioClip _startClip;
    [SerializeField] private AudioClip _moveClip;
    
    private bool _isEngineSoundPlaying = false;
    private bool _isMoveSoundPlaying = false;
    
    private void Start()
    {
        _audioSourceOneShot.clip = _startClip;
        
        _audioSourceOneShot.PlayOneShot(_startClip);
    }
    
    private void Update()
    {
        /*if (_carControls._currentSpeed > 0 && !_isMoveSoundPlaying)
        {
            if (_isEngineSoundPlaying)
            {
                _audioSourceLoop.Stop();
                _isEngineSoundPlaying = false;
            }
            
            _audioSourceLoop.PlayOneShot(_moveClip);
            _audioSourceLoop.Play();
            _isMoveSoundPlaying = true;
        }*/
        if (_carControls._currentSpeed == 0 && !_isEngineSoundPlaying)
        {
            if (_isMoveSoundPlaying)
            {
                _audioSourceLoop.Stop();
                _isMoveSoundPlaying = false;
            }
            
            _audioSourceLoop.PlayOneShot(_engineClip);
            _audioSourceLoop.Play();
            _isEngineSoundPlaying = true;
        }
    }
}
