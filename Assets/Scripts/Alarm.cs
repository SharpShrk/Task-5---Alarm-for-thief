using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _alarmClip;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changeRateVolume = 0.5f;
    private bool _isClipPlaying = false;
    private bool _isClipStop = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _audioSource.Play();
            _audioSource.volume = _minVolume;
            _isClipPlaying = true;
            var upVolumeJob = StartCoroutine(TurnUpVolume());
            
            if (_audioSource.volume == _maxVolume)
            {
                _isClipPlaying = false;
                StopCoroutine(upVolumeJob);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _isClipStop = true;
            var downVolumeJob = StartCoroutine(TurnDownVolume());
            
            if (_audioSource.volume == _minVolume)
            {
                StopCoroutine(downVolumeJob);
                _audioSource.Stop();
                _isClipStop = false;
            }
        }
    }

    private IEnumerator TurnUpVolume()
    {
        while (_isClipPlaying)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _changeRateVolume * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }        
    }

    private IEnumerator TurnDownVolume()
    {
        while(_isClipStop)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _changeRateVolume * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }        
    }
}
