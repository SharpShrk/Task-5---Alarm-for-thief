using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _alarmClip;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _changeRateVolume = 0.5f;

    public void UpVolume()
    {
        _audioSource.Play();
        _audioSource.volume = _minVolume;

        StartCoroutine(ChangeVolume(_maxVolume));
    }

    public void DownVolume()
    {
        StartCoroutine(ChangeVolume(_minVolume));

        if (_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }
    }

    private IEnumerator ChangeVolume(float volume)
    {
        while (_audioSource.volume != volume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volume, _changeRateVolume * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }        
    }
}
