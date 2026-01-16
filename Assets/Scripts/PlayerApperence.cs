using UnityEngine;
using Unity.Netcode;
public class PlayerApperence : NetworkBehaviour{
    
    private Renderer rend;

    public NetworkVariable<Color> playerColor = new NetworkVariable<Color>(
        Color.gray, NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

    public void Awake()
    {
        rend = GetComponent<Renderer>();
    }
    public override void OnNetworkSpawn(){
        if (IsServer)
        {
            if (OwnerClientId == 0)
            {
                playerColor.Value = Color.red;
            }
            else
            {
                playerColor.Value = Color.green;
            }
            rend.material.color = playerColor.Value;
            playerColor.OnValueChanged += OnColorChanged;
        }
    }

    void OnColorChanged(Color oldColor, Color newColor)
    {
        rend.material.color = newColor;
    }
}
