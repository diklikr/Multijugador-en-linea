using UnityEngine;
using Unity.Netcode;

public class Item : NetworkBehaviour
{
    private Renderer rend;

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
    }
}
