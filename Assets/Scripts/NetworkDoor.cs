using Unity.Netcode;
using UnityEngine;

public class NetworkDoor : NetworkBehaviour
{
    [SerializeReference] private Transform doorTransform;
    [SerializeField] private Vector3 openDoor = new Vector3 (0,90,0);
    [SerializeField] private Vector3 closeDoor = Vector3.zero;

    private NetworkVariable<bool> isOpen = new NetworkVariable<bool>(false);
    public static AudioManager audioManager;

    public override void OnNetworkSpawn()
    {
       isOpen.OnValueChanged += OnDoorStateChanged;
    }

    void OnDoorStateChanged(bool old, bool newest)
    {
        if(newest)
        {
            doorTransform.localRotation = Quaternion.Euler(openDoor);
        }
        else
        {
            doorTransform.localRotation = Quaternion.Euler(closeDoor);
        }
    }

    [ServerRpc]
    public void OpenDoorServerRpc()
    {
        isOpen.Value = true;
        PlaySoundClientRpc();
    }
    [ServerRpc]
    public void CloseDoorServerRpc()
    {
        isOpen.Value = false;
        PlaySoundClientRpc();
    }

    [ClientRpc]
    private void PlaySoundClientRpc()
    {
        audioManager.instance.PlayDoorSFX();
    }

}
