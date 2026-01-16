using UnityEngine;
using Unity.Netcode;
public class NetworkUI : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10,10,300,200));
        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        { if(GUILayout.Button("Iniciar Host"))
            {
                NetworkManager.Singleton.StartHost();
            }
            if (GUILayout.Button("Iniciar Client"))
            {
                NetworkManager.Singleton.StartClient();
            }
        }
        GUILayout.EndArea();
    }
}
