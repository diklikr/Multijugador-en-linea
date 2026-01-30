using Unity.Netcode;
using UnityEngine;

public class RequestSpawn : NetworkBehaviour
{
    [SerializeField] private GameObject _item;
    [SerializeField] Transform pos;
    [SerializeField] private Spawner spawner;

    private void Awake()
    {
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        pos = GameObject.Find("Spawner").transform;
    }
    private void Update()
    {
        if(!IsOwner) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            RequestSpawnServerRpc();
        }
    }

    [ServerRpc]
    void RequestSpawnServerRpc()
    {
        spawner.SpawnObject();
    }
}
