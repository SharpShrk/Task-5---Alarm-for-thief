using UnityEngine;
using System;

[RequireComponent(typeof(Player))]

public class SecuredTrigger : MonoBehaviour
{

    public event Action EventEnteredTrigger;
    public event Action EventExitedTrigger;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            EventEnteredTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            EventExitedTrigger?.Invoke();
        }
    }
}
