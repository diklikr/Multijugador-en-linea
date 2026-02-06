using Unity.Netcode;
using UnityEngine;

public class PlayerInventory : NetworkBehaviour
{
    [SerializeField] private NetworkDoor door;
    public bool hasKey = false;
    private void OnTriggerEnter(Collider other)
        {
          if (!IsOwner) return;
            if(other.CompareTag("Door") && hasKey)
            {
                door.OpenDoorServerRpc();
            }
    }
}
