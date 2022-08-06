using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _alarmClip;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changeRateVolume = 0.3f;
    private bool _isClipPlaying = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _audioSource.Play();
            _audioSource.volume = _minVolume;
            _isClipPlaying = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isClipPlaying = false;

            if (_audioSource.volume == _minVolume)
            {
                _audioSource.Stop();
            }            
        }
    }

    private void Update()
    {
        if (_isClipPlaying)
        {
            _audioSource.volume = Mathf.MoveTowards (_audioSource.volume, _maxVolume, _changeRateVolume * Time.deltaTime);
        }
        else
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _changeRateVolume * Time.deltaTime);
        }
    }
}
