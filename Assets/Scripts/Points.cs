using Unity.Netcode;
using UnityEngine;

public class Points : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;
        if (other.TryGetComponent<PlayerStats>(out PlayerStats player))
        {
            player.Score.Value += 10;
        }
    }
}
