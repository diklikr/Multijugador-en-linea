using Unity.Netcode;
using UnityEngine;

public class Key : Item
{
   [SerializeField] private PlayerInventory _key;
    [Rpc(SendTo.Server)]
    public void CollectKeyServerRpc()
    {
        if (isCollected.Value) return;
        isCollected.Value = true;

        ulong playerId = this.GetComponent<NetworkObject>().OwnerClientId;
        ShowCollectedKeyMessageClientRpc(playerId);

        this.GetComponent<NetworkObject>().Despawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;
        if (isCollected.Value) return;
        if (other.CompareTag("Player"))
        {
            CollectServerRpc();
        }
    }

    [ClientRpc]
    private void ShowCollectedKeyMessageClientRpc(ulong playerId)
    {
        if (UImanager.instance != null)
        {
            UImanager.instance.ShowMessage($"Key Collected by {playerId}!");
        }
    }

}
