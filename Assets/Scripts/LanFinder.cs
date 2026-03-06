using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class LanFinder : MonoBehaviour
{
    UdpClient udpClient;
    IPEndPoint endPoint;

    public int port = 8888;
    void Start()
    {
        udpClient = new UdpClient(port);
        udpClient.EnableBroadcast = true;

        endPoint = new IPEndPoint(IPAddress.Broadcast, port);

        InvokeRepeating(nameof(BroadcastServer), 1f, 1f);
    }

    void BroadcastServer()
    {
        string message = "UNITY_NETCODE_HOST";
        byte[] data = Encoding.UTF8.GetBytes(message);
        udpClient.Send(data, data.Length, endPoint);
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}
