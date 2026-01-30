using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject _item;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        SpawnObject();
    }

   public void SpawnObject()
    {
        GameObject obj = Instantiate(_item,transform.position,Quaternion.identity);
        obj.GetComponent<NetworkObject>().Spawn();
    }
}
