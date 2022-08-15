using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _alarmClip;
    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changeRateVolume = 0.5f;
    private Coroutine _runCorutine;

    public void UpVolume()
    {
        _audioSource.Play();
        _audioSource.volume = _minVolume;

        if (_runCorutine != null)
        {
            StopCoroutine(_runCorutine);
        }

        _runCorutine = StartCoroutine(TurnUpVolume());
    }

    public void DownVolume()
    {
        if (_runCorutine != null)
        {
            StopCoroutine(_runCorutine);
        }

        _runCorutine = StartCoroutine(TurnDownVolume());

        if (_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }
    }

    private IEnumerator TurnUpVolume()
    {
        while (_audioSource.volume <= _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _changeRateVolume * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }        
    }

    private IEnumerator TurnDownVolume()
    {
        while(_audioSource.volume >= _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _changeRateVolume * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }        
    }
}
