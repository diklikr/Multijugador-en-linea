using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class LanClient : MonoBehaviour
{
    UdpClient udpClient;
    public int port = 8888;

    public bool isConnected = false;
    string serverIp = null;

    public UnityTransport transport;

    public void StartListening()
    {
        udpClient = new UdpClient(port);
        udpClient.BeginReceive(OnReceive,null);

        Debug.Log("Started listening for Host...");
    }

    void OnReceive(IAsyncResult result)
    {
        if(isConnected) return;
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] data = udpClient.EndReceive(result, ref endPoint);

        string message = Encoding.UTF8.GetString(data);

        if(message == "UNITY_NETCODE_HOST")
        {
            serverIp = endPoint.Address.ToString();
            Debug.Log($"Found Host at {serverIp}");
        }
        udpClient.BeginReceive(OnReceive,null);
    }

    private void Update()
    {
        if(isConnected)return;

        if (serverIp != null)
        {
            isConnected = true;
            ConnectToHost(serverIp);
        }
    }

    void ConnectToHost(string ip)
    {
        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        var connectionData = transport.ConnectionData;
        connectionData.Address = ip;
        connectionData.Port = 7777;

        transport.ConnectionData = connectionData;

        Debug.Log($"Connecting to {ip}:{connectionData.Port}");

        NetworkManager.Singleton.StartClient();
    }
}
