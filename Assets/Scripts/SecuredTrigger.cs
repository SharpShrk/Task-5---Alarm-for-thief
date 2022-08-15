using UnityEngine;

[RequireComponent(typeof(Alarm))]

public class SecuredTrigger : MonoBehaviour
{
    [SerializeField] private GameObject _signaling;
    private Alarm _alarm;

    private void Start()
    {
        _alarm = _signaling.GetComponent<Alarm>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _alarm.UpVolume();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _alarm.DownVolume();
        }
    }
}
