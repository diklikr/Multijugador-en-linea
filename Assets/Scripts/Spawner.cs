using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject _item;
    private Vector3 randPos;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;
        SpawnObject();
    }

    public void RandomSpawn()
    {
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        randPos = new Vector3(x, 1.5f, z);
    }

   public void SpawnObject()
    {
        RandomSpawn();
        GameObject obj = Instantiate(_item,randPos,Quaternion.identity);
        obj.GetComponent<NetworkObject>().Spawn();
    }
}
