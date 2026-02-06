using UnityEngine;
using Unity.Netcode;

public class Item : NetworkBehaviour
{
    private Renderer rend;

    private static Spawner spawner => Object.FindFirstObjectByType<Spawner>();

    public NetworkVariable<bool> isCollected = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public override void OnNetworkSpawn()
    {
        UpdateVisual(isCollected.Value);

        isCollected.OnValueChanged += OnCollectedChanged;
    }

    void OnCollectedChanged(bool old, bool newValue)
    {
        UpdateVisual(newValue);
    }

    public void UpdateVisual(bool collected)
    {
        rend.enabled = !collected;
    }

    [Rpc(SendTo.Server)]
    public void CollectServerRpc()
    {
        if (isCollected.Value) return;
            isCollected.Value = true;
        
        ulong playerId = this.GetComponent<NetworkObject>().OwnerClientId;
        ShowCollectedMessageClientRpc(playerId);

        spawner.SpawnObject();

        this.GetComponent<NetworkObject>().Despawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;
        if (isCollected.Value) return;
        if(other.CompareTag("Player"))
        {
            CollectServerRpc();
        }
    }

    [ClientRpc]
    private void ShowCollectedMessageClientRpc(ulong playerId)
    {
        if(UImanager.instance != null)
        {
            UImanager.instance.ShowMessage($"Item Collected by {playerId}!");
        }
    }
}
