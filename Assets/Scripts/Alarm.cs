using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _alarmClip;
    [SerializeField] private SecuredTrigger _eventTriggerObject;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changeRateVolume = 0.5f;
    private Coroutine _volumeChange;

    private void OnEnable()
    {
        _eventTriggerObject.EventEnteredTrigger += UpVolume;
        _eventTriggerObject.EventExitedTrigger += DownVolume;
    }

    private void OnDisable()
    {
        _eventTriggerObject.EventEnteredTrigger -= UpVolume;
        _eventTriggerObject.EventExitedTrigger -= DownVolume;
    }

    public void UpVolume()
    {
        if (_volumeChange != null)
        {
            StopCoroutine(_volumeChange);
        }
        
        _audioSource.Play();
        _audioSource.volume = _minVolume;

        _volumeChange = StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void DownVolume()
    {
        if (_volumeChange != null)
        {
            StopCoroutine(_volumeChange);
        }

        _volumeChange = StartCoroutine(ChangeVolume(_minVolume));  
    }

    private IEnumerator ChangeVolume(float volume)
    {
        while (_audioSource.volume != volume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volume, _changeRateVolume * Time.deltaTime);
            yield return null;
        }

        if (_audioSource.volume == _minVolume)
        {
            StopCoroutine(_volumeChange);
        }
    }
}
