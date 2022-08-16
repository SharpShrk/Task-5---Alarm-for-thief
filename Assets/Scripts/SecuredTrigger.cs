using UnityEngine;
using System;

[RequireComponent(typeof(Player))]

public class SecuredTrigger : MonoBehaviour
{

    public static Action EnterTrigger;
    public static Action ExitTrigger;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            EnterTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            ExitTrigger?.Invoke();
        }
    }
}
