using Unity.Netcode;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    public NetworkVariable<int> Score = new NetworkVariable<int>(
        0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server
    );

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            Score.Value = 0;
        }
    }

    [ServerRpc]

    public void AddScoreServerRpc(int amount)
    {
        Score.Value += amount;
    }
}
